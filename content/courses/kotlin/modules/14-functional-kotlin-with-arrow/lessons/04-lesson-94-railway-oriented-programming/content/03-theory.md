---
type: "THEORY"
title: "The Railway Metaphor"
---


### Two-Track Functions

Imagine a railway with two parallel tracks:

```
Success track: ======================================>
                         \                    
Failure track:            ================================>
```

**Normal functions** keep data on the same track:
```
input --[validate]--> validInput --[transform]--> output
```

**Two-track functions** can switch to the failure track:
```
input --[validate]--\
                     \--> error
```

Once on the failure track, you stay there (short-circuit):
```
--[step1]-- error ==[step2]=======[step3]===========> error
              (skipped)    (skipped)
```

---

