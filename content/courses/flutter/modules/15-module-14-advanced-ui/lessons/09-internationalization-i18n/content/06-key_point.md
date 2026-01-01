---
type: "KEY_POINT"
title: "ICU Message Syntax"
---


**Pluralization Rules**

Different languages have different plural forms. ICU (International Components for Unicode) syntax handles this:

```
{count, plural, =0{none} =1{one} other{many}}
```

**Plural Categories:**

| Category | Languages | Example |
|----------|-----------|--------|
| `zero` | Arabic, Latvian | 0 items |
| `one` | English, Spanish | 1 item |
| `two` | Arabic, Welsh | 2 items |
| `few` | Russian, Polish | 2-4 items (varies) |
| `many` | Russian, Arabic | 5+ items (varies) |
| `other` | All languages | Default fallback |

**Arabic Example (complex pluralization):**
```
{count, plural,
  =0{لا كتب}
  =1{كتاب واحد}
  =2{كتابان}
  few{{count} كتب}
  many{{count} كتابا}
  other{{count} كتاب}
}
```

**Select for Gender/Categories:**
```
{gender, select, male{his} female{her} other{their}}
```

**Nested Messages:**
```
{gender, select,
  male{{count, plural, =1{He has 1 item} other{He has {count} items}}}
  female{{count, plural, =1{She has 1 item} other{She has {count} items}}}
  other{{count, plural, =1{They have 1 item} other{They have {count} items}}}
}
```

