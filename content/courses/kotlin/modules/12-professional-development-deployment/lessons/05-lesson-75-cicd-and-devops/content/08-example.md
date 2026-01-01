---
type: "EXAMPLE"
title: "Code Quality Tools"
---

### ktlint Configuration

ktlint enforces Kotlin style conventions. Configure it with `.editorconfig`:

```ini
# .editorconfig
root = true

[*]
charset = utf-8
end_of_line = lf
insert_final_newline = true
trim_trailing_whitespace = true
indent_style = space
indent_size = 4

[*.{kt,kts}]
ktlint_code_style = ktlint_official
max_line_length = 120
ktlint_standard_no-wildcard-imports = disabled
ktlint_standard_filename = disabled
```

```kotlin
// build.gradle.kts - Add ktlint plugin
plugins {
    id("org.jlleitschuh.gradle.ktlint") version "12.1.0"
}

ktlint {
    version.set("1.1.1")
    android.set(true)
    outputColorName.set("RED")
    reporters {
        reporter(ReporterType.CHECKSTYLE)
        reporter(ReporterType.HTML)
    }
}
```

Run: `./gradlew ktlintCheck` or `./gradlew ktlintFormat`

### detekt Configuration

detekt performs static code analysis for code smells:

```yaml
# detekt.yml - Place in project root
build:
  maxIssues: 10
  excludeCorrectable: false

complexity:
  LongMethod:
    threshold: 60
  ComplexCondition:
    threshold: 4
  TooManyFunctions:
    thresholdInFiles: 15
    thresholdInClasses: 15

naming:
  FunctionNaming:
    functionPattern: '[a-z][a-zA-Z0-9]*'
  VariableNaming:
    variablePattern: '[a-z][a-zA-Z0-9]*'

style:
  MagicNumber:
    ignoreNumbers:
      - '-1'
      - '0'
      - '1'
      - '2'
    ignoreHashCodeFunction: true
  MaxLineLength:
    maxLineLength: 120
  WildcardImport:
    active: false

# Run with: ./gradlew detekt
# Reports: build/reports/detekt/detekt.html
```
