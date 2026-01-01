---
type: "THEORY"
title: "Testing with Testcontainers for Database Integration"
---

For true integration testing with a real database, Testcontainers spins up Docker containers on demand. This ensures tests run against the same database as production.

Add Testcontainers Dependency:
```gradle
testImplementation 'org.springframework.boot:spring-boot-testcontainers'
testImplementation 'org.testcontainers:junit-jupiter'
testImplementation 'org.testcontainers:postgresql'
```

Repository Integration Tests:

```java
// src/test/java/com/taskmanager/repository/TaskRepositoryIntegrationTest.java
package com.taskmanager.repository;

import com.taskmanager.entity.*;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.jdbc.AutoConfigureTestDatabase;
import org.springframework.boot.test.autoconfigure.orm.jpa.DataJpaTest;
import org.springframework.boot.testcontainers.service.connection.ServiceConnection;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.PageRequest;
import org.testcontainers.containers.PostgreSQLContainer;
import org.testcontainers.junit.jupiter.Container;
import org.testcontainers.junit.jupiter.Testcontainers;

import java.time.LocalDate;
import java.util.List;

import static org.assertj.core.api.Assertions.assertThat;

@DataJpaTest
@Testcontainers
@AutoConfigureTestDatabase(replace = AutoConfigureTestDatabase.Replace.NONE)
class TaskRepositoryIntegrationTest {

    @Container
    @ServiceConnection
    static PostgreSQLContainer<?> postgres = new PostgreSQLContainer<>("postgres:15")
        .withDatabaseName("taskmanager_test")
        .withUsername("test")
        .withPassword("test");

    @Autowired
    private TaskRepository taskRepository;

    @Autowired
    private UserRepository userRepository;

    @Autowired
    private CategoryRepository categoryRepository;

    private User testUser;
    private Category workCategory;

    @BeforeEach
    void setUp() {
        taskRepository.deleteAll();
        categoryRepository.deleteAll();
        userRepository.deleteAll();

        testUser = new User();
        testUser.setEmail("test@example.com");
        testUser.setPassword("password");
        testUser.setName("Test User");
        testUser = userRepository.save(testUser);

        workCategory = new Category();
        workCategory.setName("Work");
        workCategory.setColor("#FF5733");
        workCategory.setUser(testUser);
        workCategory = categoryRepository.save(workCategory);
    }

    @Test
    void findByUser_ReturnsOnlyUserTasks() {
        // Arrange
        Task task1 = createTask("Task 1", TaskStatus.PENDING);
        Task task2 = createTask("Task 2", TaskStatus.COMPLETED);
        taskRepository.saveAll(List.of(task1, task2));

        User otherUser = new User();
        otherUser.setEmail("other@example.com");
        otherUser.setPassword("password");
        otherUser.setName("Other User");
        otherUser = userRepository.save(otherUser);

        Task otherTask = new Task();
        otherTask.setTitle("Other Task");
        otherTask.setStatus(TaskStatus.PENDING);
        otherTask.setPriority(TaskPriority.LOW);
        otherTask.setUser(otherUser);
        taskRepository.save(otherTask);

        // Act
        Page<Task> result = taskRepository.findByUser(testUser, PageRequest.of(0, 10));

        // Assert
        assertThat(result.getContent()).hasSize(2);
        assertThat(result.getContent()).allMatch(t -> t.getUser().getId().equals(testUser.getId()));
    }

    @Test
    void findByUserAndStatus_FiltersCorrectly() {
        // Arrange
        taskRepository.save(createTask("Pending 1", TaskStatus.PENDING));
        taskRepository.save(createTask("Pending 2", TaskStatus.PENDING));
        taskRepository.save(createTask("Completed", TaskStatus.COMPLETED));

        // Act
        Page<Task> pending = taskRepository.findByUserAndStatus(
            testUser, TaskStatus.PENDING, PageRequest.of(0, 10));

        // Assert
        assertThat(pending.getContent()).hasSize(2);
        assertThat(pending.getContent()).allMatch(t -> t.getStatus() == TaskStatus.PENDING);
    }

    @Test
    void findByUserAndCategory_FiltersCorrectly() {
        // Arrange
        Task workTask = createTask("Work Task", TaskStatus.PENDING);
        workTask.setCategory(workCategory);
        taskRepository.save(workTask);

        taskRepository.save(createTask("No Category", TaskStatus.PENDING));

        // Act
        Page<Task> result = taskRepository.findByUserAndCategory(
            testUser, workCategory, PageRequest.of(0, 10));

        // Assert
        assertThat(result.getContent()).hasSize(1);
        assertThat(result.getContent().get(0).getTitle()).isEqualTo("Work Task");
    }

    @Test
    void findOverdueTasks_ReturnsOnlyOverdue() {
        // Arrange
        Task overdue = createTask("Overdue", TaskStatus.PENDING);
        overdue.setDueDate(LocalDate.now().minusDays(1));
        taskRepository.save(overdue);

        Task future = createTask("Future", TaskStatus.PENDING);
        future.setDueDate(LocalDate.now().plusDays(1));
        taskRepository.save(future);

        Task completed = createTask("Done", TaskStatus.COMPLETED);
        completed.setDueDate(LocalDate.now().minusDays(1));
        taskRepository.save(completed);

        // Act
        List<Task> overdueTasks = taskRepository.findOverdueTasks(testUser, LocalDate.now());

        // Assert
        assertThat(overdueTasks).hasSize(1);
        assertThat(overdueTasks.get(0).getTitle()).isEqualTo("Overdue");
    }

    private Task createTask(String title, TaskStatus status) {
        Task task = new Task();
        task.setTitle(title);
        task.setStatus(status);
        task.setPriority(TaskPriority.MEDIUM);
        task.setUser(testUser);
        return task;
    }
}
```

Testcontainers benefits:
- Real database behavior (not H2 approximations)
- Automatic container lifecycle management
- @ServiceConnection auto-configures Spring datasource
- Works with CI/CD pipelines that support Docker