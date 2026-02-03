---
type: "THEORY"
title: "The Problem: Java Needs to Call External APIs"
---

Modern applications don't exist in isolation:

- Weather app → calls weather API
- E-commerce → calls payment API
- Social media → calls authentication API

Your Java code needs to make HTTP requests to external services.

Java provides: HttpClient (modern, built-in since Java 11)
Before Java 11: HttpURLConnection (old, clunky)
Popular library: Apache HttpClient

We'll use Java's modern HttpClient!