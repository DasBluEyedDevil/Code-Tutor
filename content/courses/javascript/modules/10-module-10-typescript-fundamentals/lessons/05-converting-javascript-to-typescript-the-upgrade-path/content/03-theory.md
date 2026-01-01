---
type: "THEORY"
title: "Incremental Migration"
---

The most common mistake when moving to TypeScript is trying to do it all at once. Here is the professional strategy for a smooth transition.

### 1. The "Loose" Configuration
Start by creating a `tsconfig.json` that allows JavaScript and isn't too strict yet:
```json
{
  "compilerOptions": {
    "allowJs": true,        // Treat .js files as part of the project
    "checkJs": false,       // Don't report errors in .js files yet
    "noImplicitAny": false, // Allow 'any' for now
    "strict": false         // Turn off strict mode temporarily
  }
}
```

### 2. Renaming Files
Change your file extensions from `.js` to `.ts`. 
*   **The Error Explosion:** As soon as you do this, you will see dozens of red squiggly lines. **Don't panic!**
*   **The Band-Aid:** Use the `any` type to make the errors go away so you can keep the app running. 

### 3. JSDoc: The Secret Weapon
If you're not ready to rename files yet, you can use JSDoc comments. TypeScript can read these comments and provide autocomplete and error checking inside plain `.js` files.

### 4. Tightening the Screws
Once your files are renamed and you have basic types, start turning on strict features one by one:
1.  Set `checkJs: true` to find bugs in your remaining JS files.
2.  Change `any` to specific interfaces or types.
3.  Set `strictNullChecks: true` to find potential `undefined` crashes.
4.  Finally, set `strict: true`.
