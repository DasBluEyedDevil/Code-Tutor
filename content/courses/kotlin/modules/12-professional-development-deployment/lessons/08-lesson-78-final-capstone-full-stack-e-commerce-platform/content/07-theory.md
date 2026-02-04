---
type: "THEORY"
title: "Server Implementation: Routes and DAOs"
---

### User DAO

```kotlin
// server/src/main/kotlin/com/taskflow/server/db/dao/UserDao.kt
package com.taskflow.server.db.dao

import at.favre.lib.crypto.bcrypt.BCrypt
import com.auth0.jwt.JWT
import com.auth0.jwt.algorithms.Algorithm
import com.taskflow.server.db.tables.Users
import com.taskflow.shared.dto.AuthResponse
import com.taskflow.shared.dto.LoginRequest
import com.taskflow.shared.dto.RegisterRequest
import com.taskflow.shared.model.User
import kotlinx.datetime.Clock
import org.jetbrains.exposed.sql.insert
import org.jetbrains.exposed.sql.selectAll
import org.jetbrains.exposed.sql.transactions.transaction
import java.util.Date
import java.util.UUID

class UserDao(
    private val jwtSecret: String,
    private val jwtIssuer: String,
    private val jwtAudience: String
) {
    fun register(request: RegisterRequest): AuthResponse {
        val userId = UUID.randomUUID().toString()
        val hashedPassword = BCrypt.withDefaults().hashToString(12, request.password.toCharArray())

        transaction {
            Users.insert {
                it[id] = userId
                it[email] = request.email
                it[passwordHash] = hashedPassword
                it[displayName] = request.displayName
                it[createdAt] = Clock.System.now()
            }
        }

        val user = User(id = userId, email = request.email, displayName = request.displayName)
        val token = generateToken(userId)
        return AuthResponse(token = token, user = user)
    }

    fun login(request: LoginRequest): AuthResponse? {
        val row = transaction {
            Users.selectAll().where { Users.email eq request.email }.singleOrNull()
        } ?: return null

        val verified = BCrypt.verifyer()
            .verify(request.password.toCharArray(), row[Users.passwordHash])
            .verified

        if (!verified) return null

        val user = User(
            id = row[Users.id],
            email = row[Users.email],
            displayName = row[Users.displayName]
        )
        val token = generateToken(user.id)
        return AuthResponse(token = token, user = user)
    }

    private fun generateToken(userId: String): String {
        return JWT.create()
            .withIssuer(jwtIssuer)
            .withAudience(jwtAudience)
            .withClaim("userId", userId)
            .withExpiresAt(Date(System.currentTimeMillis() + 86_400_000)) // 24 hours
            .sign(Algorithm.HMAC256(jwtSecret))
    }
}
```

### Task DAO

```kotlin
// server/src/main/kotlin/com/taskflow/server/db/dao/TaskDao.kt
package com.taskflow.server.db.dao

import com.taskflow.server.db.tables.Tasks
import com.taskflow.shared.dto.CreateTaskRequest
import com.taskflow.shared.dto.UpdateTaskRequest
import com.taskflow.shared.model.Task
import kotlinx.datetime.Clock
import org.jetbrains.exposed.sql.SqlExpressionBuilder.eq
import org.jetbrains.exposed.sql.and
import org.jetbrains.exposed.sql.deleteWhere
import org.jetbrains.exposed.sql.insert
import org.jetbrains.exposed.sql.selectAll
import org.jetbrains.exposed.sql.transactions.transaction
import org.jetbrains.exposed.sql.update
import java.util.UUID

class TaskDao {
    fun create(userId: String, request: CreateTaskRequest): Task {
        val taskId = UUID.randomUUID().toString()
        val now = Clock.System.now()

        transaction {
            Tasks.insert {
                it[id] = taskId
                it[title] = request.title
                it[description] = request.description
                it[priority] = request.priority.name
                it[status] = "TODO"
                it[category] = request.category
                it[dueDate] = request.dueDate
                it[Tasks.userId] = userId
                it[createdAt] = now
                it[updatedAt] = now
            }
        }

        return getById(userId, taskId)!!
    }

    fun getAll(userId: String): List<Task> = transaction {
        Tasks.selectAll().where { Tasks.userId eq userId }
            .map { row ->
                Task(
                    id = row[Tasks.id],
                    title = row[Tasks.title],
                    description = row[Tasks.description],
                    priority = enumValueOf(row[Tasks.priority]),
                    status = enumValueOf(row[Tasks.status]),
                    category = row[Tasks.category],
                    dueDate = row[Tasks.dueDate],
                    userId = row[Tasks.userId],
                    createdAt = row[Tasks.createdAt].toString(),
                    updatedAt = row[Tasks.updatedAt].toString()
                )
            }
    }

    fun getById(userId: String, taskId: String): Task? = transaction {
        Tasks.selectAll().where { (Tasks.id eq taskId) and (Tasks.userId eq userId) }
            .singleOrNull()?.let { row ->
                Task(
                    id = row[Tasks.id],
                    title = row[Tasks.title],
                    description = row[Tasks.description],
                    priority = enumValueOf(row[Tasks.priority]),
                    status = enumValueOf(row[Tasks.status]),
                    category = row[Tasks.category],
                    dueDate = row[Tasks.dueDate],
                    userId = row[Tasks.userId],
                    createdAt = row[Tasks.createdAt].toString(),
                    updatedAt = row[Tasks.updatedAt].toString()
                )
            }
    }

    fun update(userId: String, taskId: String, request: UpdateTaskRequest): Task? {
        transaction {
            Tasks.update({ (Tasks.id eq taskId) and (Tasks.userId eq userId) }) {
                request.title?.let { value -> it[title] = value }
                request.description?.let { value -> it[description] = value }
                request.priority?.let { value -> it[priority] = value.name }
                request.status?.let { value -> it[status] = value.name }
                request.category?.let { value -> it[category] = value }
                request.dueDate?.let { value -> it[dueDate] = value }
                it[updatedAt] = Clock.System.now()
            }
        }
        return getById(userId, taskId)
    }

    fun delete(userId: String, taskId: String): Boolean = transaction {
        Tasks.deleteWhere { (Tasks.id eq taskId) and (Tasks.userId eq userId) } > 0
    }
}
```

### Auth Routes

```kotlin
// server/src/main/kotlin/com/taskflow/server/routes/AuthRoutes.kt
package com.taskflow.server.routes

import com.taskflow.server.db.dao.UserDao
import com.taskflow.shared.dto.ApiResponse
import com.taskflow.shared.dto.LoginRequest
import com.taskflow.shared.dto.RegisterRequest
import io.ktor.http.HttpStatusCode
import io.ktor.server.request.receive
import io.ktor.server.response.respond
import io.ktor.server.routing.Route
import io.ktor.server.routing.post
import io.ktor.server.routing.route

fun Route.authRoutes(userDao: UserDao) {
    route("/api/auth") {
        post("/register") {
            val request = call.receive<RegisterRequest>()
            try {
                val response = userDao.register(request)
                call.respond(HttpStatusCode.Created, ApiResponse(success = true, data = response))
            } catch (e: Exception) {
                call.respond(
                    HttpStatusCode.Conflict,
                    ApiResponse<Unit>(success = false, message = "Email already registered")
                )
            }
        }

        post("/login") {
            val request = call.receive<LoginRequest>()
            val response = userDao.login(request)
            if (response != null) {
                call.respond(ApiResponse(success = true, data = response))
            } else {
                call.respond(
                    HttpStatusCode.Unauthorized,
                    ApiResponse<Unit>(success = false, message = "Invalid credentials")
                )
            }
        }
    }
}
```

### Task Routes

```kotlin
// server/src/main/kotlin/com/taskflow/server/routes/TaskRoutes.kt
package com.taskflow.server.routes

import com.taskflow.server.db.dao.TaskDao
import com.taskflow.shared.dto.ApiResponse
import com.taskflow.shared.dto.CreateTaskRequest
import com.taskflow.shared.dto.UpdateTaskRequest
import io.ktor.http.HttpStatusCode
import io.ktor.server.auth.authenticate
import io.ktor.server.auth.jwt.JWTPrincipal
import io.ktor.server.auth.principal
import io.ktor.server.request.receive
import io.ktor.server.response.respond
import io.ktor.server.routing.Route
import io.ktor.server.routing.delete
import io.ktor.server.routing.get
import io.ktor.server.routing.post
import io.ktor.server.routing.put
import io.ktor.server.routing.route

fun Route.taskRoutes(taskDao: TaskDao) {
    authenticate("auth-jwt") {
        route("/api/tasks") {
            get {
                val userId = call.principal<JWTPrincipal>()!!
                    .payload.getClaim("userId").asString()
                val tasks = taskDao.getAll(userId)
                call.respond(ApiResponse(success = true, data = tasks))
            }

            post {
                val userId = call.principal<JWTPrincipal>()!!
                    .payload.getClaim("userId").asString()
                val request = call.receive<CreateTaskRequest>()
                val task = taskDao.create(userId, request)
                call.respond(HttpStatusCode.Created, ApiResponse(success = true, data = task))
            }

            get("/{id}") {
                val userId = call.principal<JWTPrincipal>()!!
                    .payload.getClaim("userId").asString()
                val taskId = call.parameters["id"]!!
                val task = taskDao.getById(userId, taskId)
                if (task != null) {
                    call.respond(ApiResponse(success = true, data = task))
                } else {
                    call.respond(
                        HttpStatusCode.NotFound,
                        ApiResponse<Unit>(success = false, message = "Task not found")
                    )
                }
            }

            put("/{id}") {
                val userId = call.principal<JWTPrincipal>()!!
                    .payload.getClaim("userId").asString()
                val taskId = call.parameters["id"]!!
                val request = call.receive<UpdateTaskRequest>()
                val task = taskDao.update(userId, taskId, request)
                if (task != null) {
                    call.respond(ApiResponse(success = true, data = task))
                } else {
                    call.respond(
                        HttpStatusCode.NotFound,
                        ApiResponse<Unit>(success = false, message = "Task not found")
                    )
                }
            }

            delete("/{id}") {
                val userId = call.principal<JWTPrincipal>()!!
                    .payload.getClaim("userId").asString()
                val taskId = call.parameters["id"]!!
                val deleted = taskDao.delete(userId, taskId)
                if (deleted) {
                    call.respond(ApiResponse(success = true, message = "Task deleted"))
                } else {
                    call.respond(
                        HttpStatusCode.NotFound,
                        ApiResponse<Unit>(success = false, message = "Task not found")
                    )
                }
            }
        }
    }
}
```

---

