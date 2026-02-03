---
type: "THEORY"
title: "Integration Tests for Controllers with @WebMvcTest"
---

Integration tests verify that components work together correctly. For controllers, we use @WebMvcTest which loads only the web layer (controllers, filters, security) without the full application context.

BOTH PATHS use @WebMvcTest, but the assertions differ. REST controllers verify JSON responses; Thymeleaf view controllers verify view names and model attributes.

REACT PATH -- Testing REST API Controllers:

```java
// src/test/java/com/taskmanager/controller/TaskControllerTest.java
package com.taskmanager.controller;

import com.fasterxml.jackson.databind.ObjectMapper;
import com.taskmanager.dto.TaskRequest;
import com.taskmanager.dto.TaskResponse;
import com.taskmanager.entity.TaskStatus;
import com.taskmanager.entity.TaskPriority;
import com.taskmanager.security.JwtTokenProvider;
import com.taskmanager.service.TaskService;
import com.taskmanager.service.CustomUserDetailsService;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.web.servlet.WebMvcTest;
import org.springframework.test.context.bean.override.mockito.MockitoBean;
import org.springframework.data.domain.PageImpl;
import org.springframework.data.domain.Pageable;
import org.springframework.http.MediaType;
import org.springframework.security.test.context.support.WithMockUser;
import org.springframework.test.web.servlet.MockMvc;

import java.time.LocalDate;
import java.util.List;

import static org.mockito.ArgumentMatchers.any;
import static org.mockito.Mockito.when;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.*;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.*;
import static org.springframework.security.test.web.servlet.request.SecurityMockMvcRequestPostProcessors.csrf;

@WebMvcTest(TaskController.class)
class TaskControllerTest {

    @Autowired
    private MockMvc mockMvc;

    @Autowired
    private ObjectMapper objectMapper;

    @MockitoBean
    private TaskService taskService;

    @MockitoBean
    private JwtTokenProvider jwtTokenProvider;

    @MockitoBean
    private CustomUserDetailsService userDetailsService;

    @Test
    @WithMockUser(username = "test@example.com")
    void getTasks_ReturnsPageOfTasks() throws Exception {
        // Arrange
        TaskResponse task = new TaskResponse();
        task.setId(1L);
        task.setTitle("Test Task");
        task.setStatus(TaskStatus.PENDING);

        when(taskService.getTasks(any(), any(Pageable.class)))
            .thenReturn(new PageImpl<>(List.of(task)));

        // Act & Assert
        mockMvc.perform(get("/api/tasks")
                .contentType(MediaType.APPLICATION_JSON))
            .andExpect(status().isOk())
            .andExpect(jsonPath("$.content[0].id").value(1))
            .andExpect(jsonPath("$.content[0].title").value("Test Task"));
    }

    @Test
    @WithMockUser(username = "test@example.com")
    void createTask_ValidRequest_Returns201() throws Exception {
        // Arrange
        TaskRequest request = new TaskRequest();
        request.setTitle("New Task");
        request.setStatus(TaskStatus.PENDING);
        request.setPriority(TaskPriority.HIGH);

        TaskResponse response = new TaskResponse();
        response.setId(1L);
        response.setTitle("New Task");

        when(taskService.createTask(any(TaskRequest.class), any()))
            .thenReturn(response);

        // Act & Assert
        mockMvc.perform(post("/api/tasks")
                .with(csrf())
                .contentType(MediaType.APPLICATION_JSON)
                .content(objectMapper.writeValueAsString(request)))
            .andExpect(status().isCreated())
            .andExpect(jsonPath("$.id").value(1))
            .andExpect(jsonPath("$.title").value("New Task"));
    }

    @Test
    @WithMockUser(username = "test@example.com")
    void createTask_InvalidRequest_Returns400() throws Exception {
        // Arrange - empty title
        TaskRequest request = new TaskRequest();
        request.setTitle("");

        // Act & Assert
        mockMvc.perform(post("/api/tasks")
                .with(csrf())
                .contentType(MediaType.APPLICATION_JSON)
                .content(objectMapper.writeValueAsString(request)))
            .andExpect(status().isBadRequest());
    }

    @Test
    void getTasks_Unauthenticated_Returns401() throws Exception {
        mockMvc.perform(get("/api/tasks"))
            .andExpect(status().isUnauthorized());
    }

    @Test
    @WithMockUser(username = "test@example.com")
    void deleteTask_Returns204() throws Exception {
        mockMvc.perform(delete("/api/tasks/1")
                .with(csrf()))
            .andExpect(status().isNoContent());
    }
}
```

Key @WebMvcTest features:
- Loads only web layer, not full context (fast)
- @MockitoBean creates Spring-managed mocks
- @WithMockUser simulates authenticated user
- MockMvc makes HTTP requests and verifies responses
- csrf() adds CSRF token for POST/PUT/DELETE

THYMELEAF PATH -- Testing View Controllers:

If you chose the Thymeleaf frontend, your controllers return view names instead of JSON. Test them with MockMvc by asserting on the view name and model attributes:

```java
// src/test/java/com/taskmanager/controller/TaskViewControllerTest.java
package com.taskmanager.controller;

import com.taskmanager.entity.Task;
import com.taskmanager.entity.TaskStatus;
import com.taskmanager.service.TaskService;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.web.servlet.WebMvcTest;
import org.springframework.test.context.bean.override.mockito.MockitoBean;
import org.springframework.security.test.context.support.WithMockUser;
import org.springframework.test.web.servlet.MockMvc;

import java.util.List;

import static org.mockito.ArgumentMatchers.any;
import static org.mockito.Mockito.when;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.*;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.*;
import static org.springframework.security.test.web.servlet.request.SecurityMockMvcRequestPostProcessors.csrf;

@WebMvcTest(TaskViewController.class)
class TaskViewControllerTest {

    @Autowired
    private MockMvc mockMvc;

    @MockitoBean
    private TaskService taskService;

    @Test
    @WithMockUser(username = "test@example.com")
    void taskList_ReturnsViewWithTasks() throws Exception {
        when(taskService.getTasks(any(), any()))
            .thenReturn(List.of(new Task()));

        mockMvc.perform(get("/tasks"))
            .andExpect(status().isOk())
            .andExpect(view().name("tasks"))
            .andExpect(model().attributeExists("tasks"));
    }

    @Test
    @WithMockUser(username = "test@example.com")
    void newTaskForm_ReturnsFormView() throws Exception {
        mockMvc.perform(get("/tasks/new"))
            .andExpect(status().isOk())
            .andExpect(view().name("task-form"))
            .andExpect(model().attributeExists("taskForm"));
    }

    @Test
    @WithMockUser(username = "test@example.com")
    void createTask_ValidForm_RedirectsToTasks() throws Exception {
        mockMvc.perform(post("/tasks")
                .with(csrf())
                .param("title", "Test Task")
                .param("priority", "HIGH"))
            .andExpect(status().is3xxRedirection())
            .andExpect(redirectedUrl("/tasks"));
    }

    @Test
    @WithMockUser(username = "test@example.com")
    void createTask_InvalidForm_ReturnsFormWithErrors() throws Exception {
        mockMvc.perform(post("/tasks")
                .with(csrf())
                .param("title", ""))  // empty title fails validation
            .andExpect(status().isOk())
            .andExpect(view().name("task-form"))
            .andExpect(model().hasErrors());
    }
}
```

Key differences from REST controller tests:
- `view().name("tasks")` verifies the returned Thymeleaf template name
- `model().attributeExists("tasks")` confirms the controller populated the model
- `model().hasErrors()` checks for validation errors in form submissions
- Form tests use `.param()` instead of JSON body (HTML forms send parameters)
- Successful POST returns 3xx redirect, not 201 Created