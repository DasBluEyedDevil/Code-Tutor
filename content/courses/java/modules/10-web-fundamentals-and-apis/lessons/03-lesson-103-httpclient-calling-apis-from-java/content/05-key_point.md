---
type: "KEY_POINT"
title: "Error Handling for HTTP Requests"
---

ALWAYS handle errors:

try {
    HttpResponse<String> response = client.send(request, ...);
    
    // Check status code
    if (response.statusCode() == 200) {
        // Success!
        String body = response.body();
    } else if (response.statusCode() == 404) {
        IO.println("Resource not found");
    } else if (response.statusCode() >= 500) {
        IO.println("Server error: " + response.statusCode());
    } else {
        IO.println("Error: " + response.statusCode());
    }
    
} catch (IOException e) {
    // Network error (can't reach server)
    IO.println("Network error: " + e.getMessage());
} catch (InterruptedException e) {
    // Request was interrupted
    IO.println("Request interrupted");
}

COMMON ERRORS:
- IOException: Network issues, timeout
- 4xx codes: Client problems (bad request, not found)
- 5xx codes: Server problems (crash, overload)