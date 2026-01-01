---
type: "THEORY"
title: "C Interop (cinterop)"
---


### What is cinterop?

Kotlin/Native can call C libraries directly using **cinterop** (C interoperability). This is powerful for:
- Using system libraries (OpenSSL, SQLite, etc.)
- Integrating legacy C/C++ code
- Accessing platform APIs not available in Kotlin

### Setting Up cinterop

**Step 1: Create a .def file**

```
# src/nativeInterop/cinterop/openssl.def
package = openssl
headers = openssl/ssl.h openssl/crypto.h
compilerOpts = -I/usr/local/include
linkerOpts = -L/usr/local/lib -lssl -lcrypto
```

**Step 2: Configure in build.gradle.kts**

```kotlin
kotlin {
    linuxX64 {
        compilations.getByName("main") {
            cinterops {
                val openssl by creating {
                    defFile(project.file("src/nativeInterop/cinterop/openssl.def"))
                    packageName("openssl")
                }
            }
        }
    }
}
```

**Step 3: Use C functions in Kotlin**

```kotlin
import openssl.*

fun generateRandomBytes(count: Int): ByteArray {
    val buffer = ByteArray(count)
    buffer.usePinned { pinned ->
        RAND_bytes(pinned.addressOf(0).reinterpret(), count)
    }
    return buffer
}
```

### Memory Management with C

```kotlin
import kotlinx.cinterop.*

fun workWithCMemory() {
    // Allocate C memory
    memScoped {
        val buffer = allocArray<ByteVar>(1024)
        
        // Use the buffer
        some_c_function(buffer, 1024)
        
        // Convert to Kotlin string
        val result = buffer.toKString()
        
        // Memory automatically freed when memScoped block ends
    }
}
```

### Platform-Specific cinterop

```kotlin
// iOS-specific with CoreFoundation
kotlin {
    iosArm64 {
        compilations.getByName("main") {
            cinterops {
                val corefoundation by creating {
                    defFile("src/nativeInterop/cinterop/corefoundation.def")
                }
            }
        }
    }
}
```

---

