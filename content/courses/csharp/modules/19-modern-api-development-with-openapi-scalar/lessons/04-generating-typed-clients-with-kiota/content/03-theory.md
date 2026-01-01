---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`kiota generate`**: CLI command to generate a client. Creates models and client classes from OpenAPI spec.

**`-l CSharp`**: Target language. Options: CSharp, TypeScript, Python, Go, Java, Ruby, PHP, Swift.

**`-o ./Client`**: Output directory. Generated files go here. Usually add to .gitignore or commit for easier builds.

**`-d <spec>`**: OpenAPI document. Can be local file path or URL to live spec.

**`-c ApiClient`**: Client class name. The main class you'll instantiate to make API calls.

**`-n MyApp.Client`**: Namespace for generated code. Choose something that fits your project structure.

**`HttpClientRequestAdapter`**: Kiota's HTTP implementation using HttpClient. Handles serialization, headers, etc.

**`AnonymousAuthenticationProvider`**: For APIs without auth. Use other providers for OAuth, API keys, etc.

**`client.Products.GetAsync()`**: Fluent API matches your endpoints. Products endpoint becomes Products property.

**`client.Products[id].GetAsync()`**: Path parameters use indexer syntax. Clean and intuitive.

**`config.QueryParameters`**: Typed query parameters. IntelliSense shows what's available. Compile-time checking.