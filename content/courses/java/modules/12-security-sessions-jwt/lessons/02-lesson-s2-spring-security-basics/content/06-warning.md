---
type: "WARNING"
title: "Spring Security 6 Migration Notes"
---

If you see old tutorials, note these Spring Security 6 changes:

OLD (Spring Security 5.x):
@EnableWebSecurity
public class SecurityConfig extends WebSecurityConfigurerAdapter {
    @Override
    protected void configure(HttpSecurity http) {
        http.authorizeRequests()
            .antMatchers("/admin/**").hasRole("ADMIN")
    }
}

NEW (Spring Security 6.x):
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
- No more extending WebSecurityConfigurerAdapter (deprecated)
- authorizeRequests() -> authorizeHttpRequests()
- antMatchers() -> requestMatchers()
- Method returns SecurityFilterChain bean
- Lambda DSL is the standard