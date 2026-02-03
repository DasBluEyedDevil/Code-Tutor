---
type: "THEORY"
title: "Making a GET Request"
---

import java.net.URI;
import java.net.http.HttpClient;
import java.net.http.HttpRequest;
import java.net.http.HttpResponse;

public class APIExample {
    public static void main(String[] args) throws Exception {
        // 1. Create HttpClient
        HttpClient client = HttpClient.newHttpClient();
        
        // 2. Build request
        HttpRequest request = HttpRequest.newBuilder()
            .uri(URI.create("https://api.example.com/users/123"))
            .GET()
            .build();
        
        // 3. Send request and get response
        HttpResponse<String> response = client.send(
            request,
            HttpResponse.BodyHandlers.ofString()
        );
        
        // 4. Process response
        IO.println("Status: " + response.statusCode());
        IO.println("Body: " + response.body());
    }
}