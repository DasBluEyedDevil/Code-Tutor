---
type: "WARNING"
title: "Common Pitfalls"
---

## Watch Out For These Mistakes!

**Empty catch blocks**: Writing `catch { }` swallows errors silently! ALWAYS log or handle errors. Silent failures are impossible to debug.

**Catching Exception first**: If you put `catch (Exception ex)` FIRST, it catches everything! More specific catch blocks after it will never run.

**Forgetting to use exception filters**: Instead of complex if-statements inside catch blocks, use 'when' filters: `catch (HttpException ex) when (ex.StatusCode == 404)`. Cleaner AND preserves stack trace!

**Not re-throwing properly**: Use `throw;` (not `throw ex;`) to re-throw while preserving the original stack trace.