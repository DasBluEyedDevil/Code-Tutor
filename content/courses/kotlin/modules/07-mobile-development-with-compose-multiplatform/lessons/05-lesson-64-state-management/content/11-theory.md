---
type: "THEORY"
title: "Solution 1"
---



---



```kotlin
import androidx.compose.foundation.layout.*
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Email
import androidx.compose.material.icons.filled.Lock
import androidx.compose.material3.*
import androidx.compose.runtime.*
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.text.input.PasswordVisualTransformation
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp

data class LoginState(
    val email: String = "",
    val password: String = "",
    val rememberMe: Boolean = false
) {
    val isValid: Boolean
        get() = email.contains("@") && password.length >= 6
}

@Composable
fun LoginScreen() {
    var loginState by rememberSaveable(stateSaver = LoginStateSaver) {
        mutableStateOf(LoginState())
    }

    LoginForm(
        loginState = loginState,
        onEmailChange = { loginState = loginState.copy(email = it) },
        onPasswordChange = { loginState = loginState.copy(password = it) },
        onRememberMeChange = { loginState = loginState.copy(rememberMe = it) },
        onLoginClick = {
            // Handle login
            println("Login: ${loginState.email}")
        }
    )
}

@Composable
fun LoginForm(
    loginState: LoginState,
    onEmailChange: (String) -> Unit,
    onPasswordChange: (String) -> Unit,
    onRememberMeChange: (Boolean) -> Unit,
    onLoginClick: () -> Unit,
    modifier: Modifier = Modifier
) {
    Column(
        modifier = modifier
            .fillMaxSize()
            .padding(24.dp),
        horizontalAlignment = Alignment.CenterHorizontally,
        verticalArrangement = Arrangement.Center
    ) {
        Text(
            "Login",
            style = MaterialTheme.typography.headlineLarge
        )

        Spacer(modifier = Modifier.height(32.dp))

        // Email field
        OutlinedTextField(
            value = loginState.email,
            onValueChange = onEmailChange,
            label = { Text("Email") },
            leadingIcon = {
                Icon(Icons.Default.Email, contentDescription = null)
            },
            isError = loginState.email.isNotEmpty() && !loginState.email.contains("@"),
            supportingText = {
                if (loginState.email.isNotEmpty() && !loginState.email.contains("@")) {
                    Text("Invalid email")
                }
            },
            modifier = Modifier.fillMaxWidth()
        )

        Spacer(modifier = Modifier.height(16.dp))

        // Password field
        OutlinedTextField(
            value = loginState.password,
            onValueChange = onPasswordChange,
            label = { Text("Password") },
            leadingIcon = {
                Icon(Icons.Default.Lock, contentDescription = null)
            },
            visualTransformation = PasswordVisualTransformation(),
            isError = loginState.password.isNotEmpty() && loginState.password.length < 6,
            supportingText = {
                if (loginState.password.isNotEmpty() && loginState.password.length < 6) {
                    Text("Password must be at least 6 characters")
                }
            },
            modifier = Modifier.fillMaxWidth()
        )

        Spacer(modifier = Modifier.height(8.dp))

        // Remember me
        Row(
            modifier = Modifier.fillMaxWidth(),
            verticalAlignment = Alignment.CenterVertically
        ) {
            Checkbox(
                checked = loginState.rememberMe,
                onCheckedChange = onRememberMeChange
            )
            Text("Remember me")
        }

        Spacer(modifier = Modifier.height(24.dp))

        // Login button
        Button(
            onClick = onLoginClick,
            enabled = loginState.isValid,
            modifier = Modifier.fillMaxWidth()
        ) {
            Text("Login")
        }
    }
}

// Custom saver for LoginState
val LoginStateSaver = Saver<LoginState, List<Any>>(
    save = { listOf(it.email, it.password, it.rememberMe) },
    restore = {
        LoginState(
            email = it[0] as String,
            password = it[1] as String,
            rememberMe = it[2] as Boolean
        )
    }
)

@Preview(showBackground = true)
@Composable
fun LoginScreenPreview() {
    MaterialTheme {
        LoginScreen()
    }
}
```
