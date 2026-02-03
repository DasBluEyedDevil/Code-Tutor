---
type: "THEORY"
title: "POST Request with JSON Body"
---

import com.google.gson.Gson;

// Create data object
class User {
    String name;
    int age;
    
    User(String name, int age) {
        this.name = name;
        this.age = age;
    }
}

// Convert to JSON
Gson gson = new Gson();
User newUser = new User("Alice", 20);
String json = gson.toJson(newUser);  // {"name":"Alice","age":20}

// Build POST request
HttpRequest request = HttpRequest.newBuilder()
    .uri(URI.create("https://api.example.com/users"))
    .header("Content-Type", "application/json")
    .POST(HttpRequest.BodyPublishers.ofString(json))
    .build();

// Send request
HttpResponse<String> response = client.send(
    request,
    HttpResponse.BodyHandlers.ofString()
);

// Parse response
if (response.statusCode() == 201) {
    User created = gson.fromJson(response.body(), User.class);
    IO.println("Created user: " + created.name);
}