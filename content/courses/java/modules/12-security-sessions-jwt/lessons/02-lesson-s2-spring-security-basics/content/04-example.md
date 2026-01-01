---
type: "EXAMPLE"
title: "Basic Security Configuration"
---

Customize security with SecurityFilterChain:

```java
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.security.config.annotation.web.builders.HttpSecurity;
import org.springframework.security.config.annotation.web.configuration.EnableWebSecurity;
import org.springframework.security.core.userdetails.User;
import org.springframework.security.core.userdetails.UserDetailsService;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.security.provisioning.InMemoryUserDetailsManager;
import org.springframework.security.web.SecurityFilterChain;

@Configuration
@EnableWebSecurity
public class SecurityConfig {

    @Bean
    public SecurityFilterChain securityFilterChain(HttpSecurity http) throws Exception {
        return http
            // Define URL authorization rules
            .authorizeHttpRequests(auth -> auth
                // Public endpoints - no auth required
                .requestMatchers("/", "/home", "/public/**").permitAll()
                .requestMatchers("/css/**", "/js/**", "/images/**").permitAll()
                // Admin endpoints - ADMIN role required
                .requestMatchers("/admin/**").hasRole("ADMIN")
                // API endpoints - authenticated users only
                .requestMatchers("/api/**").authenticated()
                // Everything else - authenticated
                .anyRequest().authenticated()
            )
            // Enable form-based login
            .formLogin(form -> form
                .loginPage("/login")        // Custom login page
                .defaultSuccessUrl("/dashboard", true)
                .permitAll()
            )
            // Enable logout
            .logout(logout -> logout
                .logoutSuccessUrl("/login?logout")
                .permitAll()
            )
            .build();
    }

    @Bean
    public UserDetailsService userDetailsService() {
        // In-memory users for demo (use database in production!)
        var user = User.builder()
            .username("user")
            .password(passwordEncoder().encode("password"))
            .roles("USER")
            .build();

        var admin = User.builder()
            .username("admin")
            .password(passwordEncoder().encode("admin123"))
            .roles("USER", "ADMIN")
            .build();

        return new InMemoryUserDetailsManager(user, admin);
    }

    @Bean
    public PasswordEncoder passwordEncoder() {
        return new BCryptPasswordEncoder();
    }
}
```
