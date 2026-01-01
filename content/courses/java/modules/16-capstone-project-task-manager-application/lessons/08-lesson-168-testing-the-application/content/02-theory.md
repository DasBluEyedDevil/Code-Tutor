---
type: "THEORY"
title: "Integration Tests for Controllers with @WebMvcTest"
---

Integration tests verify that components work together correctly. For REST controllers, we use @WebMvcTest which loads only the web layer (controllers, filters, security) without the full application context.

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
import org.springframework.boot.test.mock.mockito.MockBean;
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

    @MockBean
    private TaskService taskService;

    @MockBean
    private JwtTokenProvider jwtTokenProvider;

    @MockBean
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
- @MockBean creates Spring-managed mocks
- @WithMockUser simulates authenticated user
- MockMvc makes HTTP requests and verifies responses
- csrf() adds CSRF token for POST/PUT/DELETE