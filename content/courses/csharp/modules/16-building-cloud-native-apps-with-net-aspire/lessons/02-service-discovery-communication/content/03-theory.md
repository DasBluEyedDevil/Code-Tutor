---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`.WithReference(catalogApi)`**: In AppHost, this tells Aspire that webapp needs to call catalog-api. Aspire injects environment variables with the service URL.

**`new Uri("http://catalog-api")`**: Use the SERVICE NAME as the hostname! Aspire's service discovery intercepts this and resolves to the actual running instance URL.

**`AddHttpClient<TClient>()`**: Registers a typed HttpClient. The client class receives a pre-configured HttpClient via constructor injection. Cleaner than using IHttpClientFactory directly.

**`builder.AddServiceDefaults()`**: Enables service discovery (among other things). Without this, http://service-name won't resolve!

**`GetFromJsonAsync<T>()`**: Convenience method that GETs JSON and deserializes to T. Part of System.Net.Http.Json. Reduces boilerplate.

**`Refit [Get], [Post] attributes`**: Declare HTTP operations as interface methods. Refit generates the implementation. Cleaner than manual HttpClient code.