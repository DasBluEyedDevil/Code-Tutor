---
type: "WARNING"
title: "Spring Security Migration Notes"
---

If you see old tutorials, note these changes in modern Spring Security (7.x):

OLD (Spring Security 5.x and earlier):
@EnableWebSecurity
public class SecurityConfig extends WebSecurityConfigurerAdapter {
    @Override
    protected void configure(HttpSecurity http) {
        http.authorizeRequests()
            .antMatchers("/admin/**").hasRole("ADMIN")
    }
}

CURRENT (Spring Security 7.x / Spring Boot 4.0):
@Configuration
@EnableWebSecurity
public class SecurityConfig {
    @Bean
    public SecurityFilterChain filterChain(HttpSecurity http) {
        return http
            .authorizeHttpRequests(auth -> auth
                .requestMatchers("/admin/**").hasRole("ADMIN")
            )
            .build();
    }
}

KEY CHANGES:
- WebSecurityConfigurerAdapter was removed (use SecurityFilterChain beans)
- authorizeRequests() -> authorizeHttpRequests()
- antMatchers() -> requestMatchers()
- Method returns SecurityFilterChain bean
- Lambda DSL is the standard