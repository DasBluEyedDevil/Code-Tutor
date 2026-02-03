---
type: "THEORY"
title: "Thymeleaf Path: Fundamentals and Setup"
---

THYMELEAF PATH -- If you chose React, skip to the React Path sections.

Thymeleaf is a modern server-side Java template engine for web and standalone environments. It processes HTML templates on the server, replacing placeholders with actual data before sending the complete page to the browser.

Adding Thymeleaf to Your Project:
Add the Spring Boot Thymeleaf starter to your pom.xml or build.gradle:

```xml
<dependency>
    <groupId>org.springframework.boot</groupId>
    <artifactId>spring-boot-starter-thymeleaf</artifactId>
</dependency>
```

Spring Boot auto-configures Thymeleaf to look for templates in src/main/resources/templates/ with the .html extension.

How Thymeleaf Works:
1. A Spring MVC controller handles a request and adds data to the Model
2. The controller returns a view name (like "tasks/list")
3. Thymeleaf finds the template at templates/tasks/list.html
4. Thymeleaf processes the template, replacing th: attributes with real data
5. The server sends the fully rendered HTML to the browser

Core Thymeleaf Syntax:

th:text -- Replace element text with a value:
```html
<span th:text="${task.title}">Default Title</span>
```
This renders: <span>Buy groceries</span>

th:each -- Loop over a collection:
```html
<div th:each="task : ${tasks}">
    <h3 th:text="${task.title}">Task Title</h3>
    <span th:text="${task.status}">PENDING</span>
</div>
```
This repeats the div once for each task in the list.

th:if and th:unless -- Conditional rendering:
```html
<span th:if="${task.status == 'COMPLETED'}" class="text-green-600">Done</span>
<span th:unless="${task.status == 'COMPLETED'}" class="text-yellow-600">In Progress</span>
```

th:href -- Dynamic links:
```html
<a th:href="@{/tasks/{id}(id=${task.id})}">View Task</a>
```
This renders: <a href="/tasks/42">View Task</a>

th:src -- Dynamic image/resource paths:
```html
<link th:href="@{/css/style.css}" rel="stylesheet" />
```

The key insight: Thymeleaf templates are valid HTML. The th: attributes are processed on the server and removed before sending to the browser. You can open templates directly in a browser for prototyping -- the default text inside elements serves as placeholder content.
