---
type: "THEORY"
title: "Unit Tests for Services with JUnit 5 and Mockito"
---

Testing is essential for maintaining code quality and preventing regressions. We will start with unit tests for our service layer, where the business logic lives.

Unit tests verify individual components in isolation. For services, we mock their dependencies (repositories, other services) to focus on testing the service logic itself.

Test Dependencies (already in build.gradle):
```gradle
testImplementation 'org.springframework.boot:spring-boot-starter-test'
// Includes: JUnit 5, Mockito, AssertJ, Hamcrest
```

Testing TaskService:

```java
// src/test/java/com/taskmanager/service/TaskServiceTest.java
package com.taskmanager.service;

import com.taskmanager.dto.TaskRequest;
import com.taskmanager.dto.TaskResponse;
import com.taskmanager.entity.Task;
import com.taskmanager.entity.User;
import com.taskmanager.entity.Category;
import com.taskmanager.entity.TaskStatus;
import com.taskmanager.entity.TaskPriority;
import com.taskmanager.exception.ResourceNotFoundException;
import com.taskmanager.exception.UnauthorizedException;
import com.taskmanager.repository.TaskRepository;
import com.taskmanager.repository.CategoryRepository;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.extension.ExtendWith;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.junit.jupiter.MockitoExtension;

import java.time.LocalDate;
import java.util.Optional;

import static org.assertj.core.api.Assertions.*;
import static org.mockito.ArgumentMatchers.any;
import static org.mockito.Mockito.*;

@ExtendWith(MockitoExtension.class)
class TaskServiceTest {

    @Mock
    private TaskRepository taskRepository;

    @Mock
    private CategoryRepository categoryRepository;

    @InjectMocks
    private TaskService taskService;

    private User testUser;
    private Task testTask;
    private TaskRequest validRequest;

    @BeforeEach
    void setUp() {
        testUser = new User();
        testUser.setId(1L);
        testUser.setEmail("test@example.com");
        testUser.setName("Test User");

        testTask = new Task();
        testTask.setId(1L);
        testTask.setTitle("Test Task");
        testTask.setStatus(TaskStatus.PENDING);
        testTask.setPriority(TaskPriority.MEDIUM);
        testTask.setUser(testUser);

        validRequest = new TaskRequest();
        validRequest.setTitle("New Task");
        validRequest.setDescription("Task description");
        validRequest.setStatus(TaskStatus.PENDING);
        validRequest.setPriority(TaskPriority.HIGH);
        validRequest.setDueDate(LocalDate.now().plusDays(7));
    }

    @Test
    @DisplayName("createTask should save and return task")
    void createTask_Success() {
        // Arrange
        when(taskRepository.save(any(Task.class))).thenAnswer(invocation -> {
            Task saved = invocation.getArgument(0);
            saved.setId(1L);
            return saved;
        });

        // Act
        TaskResponse response = taskService.createTask(validRequest, testUser);

        // Assert
        assertThat(response).isNotNull();
        assertThat(response.getTitle()).isEqualTo("New Task");
        assertThat(response.getPriority()).isEqualTo(TaskPriority.HIGH);
        verify(taskRepository, times(1)).save(any(Task.class));
    }

    @Test
    @DisplayName("getTask should throw exception when task not found")
    void getTask_NotFound_ThrowsException() {
        // Arrange
        when(taskRepository.findById(999L)).thenReturn(Optional.empty());

        // Act & Assert
        assertThatThrownBy(() -> taskService.getTask(999L, testUser))
            .isInstanceOf(ResourceNotFoundException.class)
            .hasMessageContaining("Task not found");
    }

    @Test
    @DisplayName("getTask should throw exception when user is not owner")
    void getTask_WrongUser_ThrowsUnauthorized() {
        // Arrange
        User otherUser = new User();
        otherUser.setId(2L);
        when(taskRepository.findById(1L)).thenReturn(Optional.of(testTask));

        // Act & Assert
        assertThatThrownBy(() -> taskService.getTask(1L, otherUser))
            .isInstanceOf(UnauthorizedException.class);
    }

    @Test
    @DisplayName("updateTask should update all fields")
    void updateTask_Success() {
        // Arrange
        when(taskRepository.findById(1L)).thenReturn(Optional.of(testTask));
        when(taskRepository.save(any(Task.class))).thenAnswer(i -> i.getArgument(0));

        TaskRequest updateRequest = new TaskRequest();
        updateRequest.setTitle("Updated Title");
        updateRequest.setStatus(TaskStatus.COMPLETED);
        updateRequest.setPriority(TaskPriority.LOW);

        // Act
        TaskResponse response = taskService.updateTask(1L, updateRequest, testUser);

        // Assert
        assertThat(response.getTitle()).isEqualTo("Updated Title");
        assertThat(response.getStatus()).isEqualTo(TaskStatus.COMPLETED);
    }

    @Test
    @DisplayName("deleteTask should remove task")
    void deleteTask_Success() {
        // Arrange
        when(taskRepository.findById(1L)).thenReturn(Optional.of(testTask));
        doNothing().when(taskRepository).delete(testTask);

        // Act
        taskService.deleteTask(1L, testUser);

        // Assert
        verify(taskRepository, times(1)).delete(testTask);
    }
}
```

Run tests with: ./gradlew test or mvn test

Key testing patterns:
- @ExtendWith(MockitoExtension.class) enables Mockito annotations
- @Mock creates mock objects
- @InjectMocks injects mocks into the service
- AssertJ provides fluent assertions
- verify() confirms mock interactions