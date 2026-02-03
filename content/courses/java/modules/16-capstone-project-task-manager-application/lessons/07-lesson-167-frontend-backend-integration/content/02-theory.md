---
type: "THEORY"
title: "Thymeleaf Path: Login and Registration Pages"
---

THYMELEAF PATH (continued)

Build the login and registration pages using Thymeleaf templates and Spring Security's form login support.

Login Page (templates/login.html):
```html
<!DOCTYPE html>
<html xmlns:th="http://www.thymeleaf.org">
<head>
    <meta charset="UTF-8" />
    <title>Login - Task Manager</title>
    <link th:href="@{/css/style.css}" rel="stylesheet" />
</head>
<body class="bg-gray-100 min-h-screen flex items-center justify-center">
    <div class="bg-white p-8 rounded-lg shadow-md w-full max-w-md">
        <h1 class="text-2xl font-bold text-center mb-6">Login</h1>

        <div th:if="${param.error}" class="bg-red-100 text-red-700 p-3 rounded mb-4">
            Invalid email or password.
        </div>
        <div th:if="${param.logout}" class="bg-green-100 text-green-700 p-3 rounded mb-4">
            You have been logged out.
        </div>

        <form th:action="@{/login}" method="post" class="space-y-4">
            <div>
                <label class="block text-sm font-medium mb-1">Email</label>
                <input type="email" name="username" required
                       class="w-full border rounded px-3 py-2" placeholder="you@example.com" />
            </div>
            <div>
                <label class="block text-sm font-medium mb-1">Password</label>
                <input type="password" name="password" required
                       class="w-full border rounded px-3 py-2" />
            </div>
            <button type="submit" class="w-full bg-blue-600 text-white py-2 rounded hover:bg-blue-700">
                Sign In
            </button>
        </form>
        <p class="text-center mt-4 text-gray-600">
            No account? <a th:href="@{/register}" class="text-blue-600 hover:underline">Register</a>
        </p>
    </div>
</body>
</html>
```

Notice: Spring Security uses "username" as the default parameter name for the login field. Since we use email as the username, the input name is "username" even though the label says "Email".

Login Controller:
```java
@Controller
public class AuthViewController {

    @GetMapping("/login")
    public String showLoginForm() {
        return "login";
    }

    @GetMapping("/register")
    public String showRegisterForm(Model model) {
        model.addAttribute("registerForm", new RegisterRequest());
        return "register";
    }

    @PostMapping("/register")
    public String register(@Valid @ModelAttribute("registerForm") RegisterRequest request,
                           BindingResult bindingResult,
                           RedirectAttributes redirectAttributes) {
        if (bindingResult.hasErrors()) {
            return "register";
        }
        try {
            authService.register(request);
            redirectAttributes.addFlashAttribute("successMessage",
                "Account created! Please log in.");
            return "redirect:/login";
        } catch (DuplicateResourceException e) {
            bindingResult.rejectValue("email", "duplicate",
                "This email is already registered.");
            return "register";
        }
    }
}
```

The login POST is handled entirely by Spring Security -- you do not need a controller method for it. Spring Security intercepts POST /login, validates credentials, creates a session, and redirects to the configured success URL.
