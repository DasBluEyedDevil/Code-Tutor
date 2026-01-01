---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`[JsonSerializable(typeof(T))]`**: Register type T for source generation. The generator creates Serialize/Deserialize methods specifically for T. No reflection!

**`[JsonSourceGenerationOptions(...)]`**: Configure all generated serializers. WriteIndented, PropertyNamingPolicy, etc. apply to all types in this context.

**`[GeneratedRegex(pattern)]`**: Marks a partial method to receive generated regex. The generator compiles the pattern at build time into optimized IL.

**`public static partial Regex MethodName()`**: The signature for generated regex. Must be static, partial, return Regex, take no parameters. Generator fills in the body.

**`[LoggerMessage(Level, Message)]`**: Generates high-performance logging method. Message uses {placeholders} that map to method parameters.

**`public static partial void LogXxx(ILogger, params...)`**: Logger is first param. Additional params match {placeholders} in order. Generator creates the implementation.

**Why partial?**: Source generators ADD code to partial classes/methods. You declare the signature, generator provides implementation.