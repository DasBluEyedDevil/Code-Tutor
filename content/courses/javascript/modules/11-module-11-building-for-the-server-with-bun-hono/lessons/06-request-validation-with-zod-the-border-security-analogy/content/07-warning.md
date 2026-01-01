---
type: "WARNING"
title: "Common Pitfalls"
---

Common Zod validation mistakes:

1. **Forgetting async for refine with promises**:
   ```typescript
   // Wrong - async refinement but using parse()
   const schema = z.string().refine(async (val) => {
     return await checkDatabase(val);
   });
   schema.parse(data);  // Won't work with async!
   
   // Correct - use parseAsync for async refinements
   await schema.parseAsync(data);
   await schema.safeParseAsync(data);
   ```

2. **Not using coerce for query parameters**:
   ```typescript
   // Wrong - query params are always strings!
   const query = z.object({ page: z.number() });
   // ?page=1 will fail because '1' is a string
   
   // Correct - coerce string to number
   const query = z.object({ page: z.coerce.number() });
   ```

3. **Confusing optional() and nullable()**:
   ```typescript
   z.string().optional()   // string | undefined
   z.string().nullable()   // string | null
   z.string().nullish()    // string | null | undefined
   ```

4. **Not handling ZodError properly**:
   ```typescript
   // Wrong - silent failure
   const result = schema.safeParse(data);
   doSomething(result.data);  // data might be undefined!
   
   // Correct - check success first
   if (result.success) {
     doSomething(result.data);
   } else {
     handleErrors(result.error);
   }
   ```

5. **Transform vs Refine order matters**:
   ```typescript
   // Wrong order - refine runs before transform
   z.string().refine(s => s.length > 0).transform(s => s.trim());
   // '   ' passes refine but becomes '' after transform
   
   // Correct - transform first, then validate
   z.string().transform(s => s.trim()).pipe(z.string().min(1));
   ```

6. **Forgetting to export schema types**:
   ```typescript
   // Define schema once, export both schema and type
   export const userSchema = z.object({...});
   export type User = z.infer<typeof userSchema>;
   ```