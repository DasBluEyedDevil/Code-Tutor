---
type: "THEORY"
title: "💻 Real Test Class Example"
---

```java
import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;

class StringUtilsTest {
    
    @Test
    void testReverseWithNormalString() {
        String result = StringUtils.reverse("hello");
        assertEquals("olleh", result);
    }
    
    @Test
    void testReverseWithEmptyString() {
        String result = StringUtils.reverse("");
        assertEquals("", result);
    }
    
    @Test
    void testReverseWithNullThrowsException() {
        assertThrows(IllegalArgumentException.class, () -> {
            StringUtils.reverse(null);
        });
    }
}

Note: Test different scenarios (normal case, edge cases, error cases)
```