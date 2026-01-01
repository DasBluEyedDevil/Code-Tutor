---
type: "THEORY"
title: "Form Login with Spring Security"
---

Spring Security's form login handles:
- Login page generation (or custom)
- Credential validation
- Session creation
- Remember-me (optional)
- Login success/failure handling

@Bean
public SecurityFilterChain filterChain(HttpSecurity http) throws Exception {
    return http
        .formLogin(form -> form
            .loginPage("/login")              // Custom login page
            .loginProcessingUrl("/perform_login")  // Form action URL
            .usernameParameter("email")       // Custom field name
            .passwordParameter("pwd")         // Custom field name
            .defaultSuccessUrl("/dashboard")  // After login
            .failureUrl("/login?error=true")  // On failure
            .successHandler(customSuccessHandler)  // Custom logic
            .failureHandler(customFailureHandler)
            .permitAll()
        )
        .build();
}

Custom login page (Thymeleaf):
<form th:action="@{/perform_login}" method="post">
    <input type="text" name="email" placeholder="Email">
    <input type="password" name="pwd" placeholder="Password">
    <button type="submit">Login</button>
</form>