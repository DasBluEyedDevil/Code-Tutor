---
type: "THEORY"
title: "Thymeleaf Path: Spring MVC Form Handling and CSRF"
---

THYMELEAF PATH

Spring MVC and Thymeleaf handle form submission through the standard HTTP POST/redirect/GET pattern. Spring Security adds automatic CSRF protection, and the framework provides elegant validation error display.

CSRF Protection:
Spring Security automatically includes a CSRF token in Thymeleaf forms. You do not need to do anything special -- Thymeleaf's th:action automatically adds the hidden CSRF field:

```html
<form th:action="@{/tasks}" method="post">
    <!-- Spring Security auto-inserts: -->
    <!-- <input type="hidden" name="_csrf" value="abc123..."> -->
    <input type="text" name="title" />
    <button type="submit">Create</button>
</form>
```

This protects against cross-site request forgery attacks. Every POST, PUT, and DELETE request must include the CSRF token, and Spring Security verifies it automatically.

Validation Error Display:
When form validation fails, the controller returns the form view again with errors attached to the BindingResult. Thymeleaf can display these inline:

```html
<form th:action="@{/tasks}" th:object="${taskForm}" method="post">
    <div>
        <label>Title *</label>
        <input type="text" th:field="*{title}"
               th:classappend="${#fields.hasErrors('title')} ? 'border-red-500' : 'border-gray-300'"
               class="w-full border rounded px-3 py-2" />
        <p th:if="${#fields.hasErrors('title')}"
           th:errors="*{title}" class="text-red-500 text-sm mt-1">Error message</p>
    </div>
    <!-- more fields... -->
</form>
```

Flash Attributes and Redirect Messages:
After a successful create/update/delete, redirect the user back to the task list with a success message:

```java
@PostMapping
public String createTask(@AuthenticationPrincipal User user,
                         @Valid @ModelAttribute("taskForm") TaskRequest request,
                         BindingResult bindingResult,
                         RedirectAttributes redirectAttributes) {
    if (bindingResult.hasErrors()) {
        return "tasks/form";  // Re-render form with errors
    }
    taskService.createTask(request, user);
    redirectAttributes.addFlashAttribute("successMessage", "Task created!");
    return "redirect:/tasks";  // Redirect to list (Post-Redirect-Get)
}
```

Flash attributes survive one redirect and are then discarded. This prevents the success message from appearing again on page refresh.

Session-Based Authentication for Thymeleaf:
For the Thymeleaf path, you can use Spring Security's built-in form login instead of JWT:

```java
@Bean
public SecurityFilterChain securityFilterChain(HttpSecurity http) throws Exception {
    http
        .authorizeHttpRequests(auth -> auth
            .requestMatchers("/login", "/register", "/css/**", "/js/**").permitAll()
            .anyRequest().authenticated()
        )
        .formLogin(form -> form
            .loginPage("/login")
            .defaultSuccessUrl("/tasks")
            .permitAll()
        )
        .logout(logout -> logout
            .logoutSuccessUrl("/login?logout")
            .permitAll()
        );
    return http.build();
}
```

This is simpler than JWT -- Spring Security handles session creation, CSRF tokens, and logout automatically.
