---
type: "CONCEPT"
title: "The Two Module Systems"
---

JavaScript has a unique challenge in the programming world: it evolved with TWO competing module systems, each with its own syntax, behavior, and ecosystem. Understanding both is essential for any modern JavaScript developer.

**CommonJS (CJS)** - Born in 2009 for Node.js
When Node.js was created, JavaScript had no native module system. Ryan Dahl needed a way to organize server-side code, so CommonJS was invented. It uses `require()` to import and `module.exports` to export. CommonJS modules load SYNCHRONOUSLY - the entire file is read and executed before the next line runs. This works great on servers where files are local, but would freeze browsers waiting for network requests.

**ES Modules (ESM)** - The Official Standard (ES2015/ES6)
In 2015, JavaScript finally got an official module system as part of the ES6 specification. It uses `import` and `export` keywords. ESM modules are STATIC - imports are analyzed at parse time before code runs. This enables tree-shaking (removing unused code) and better optimization. ESM also supports asynchronous loading, making it perfect for browsers.

**The Current State (2024-2025)**
Today, ESM is the clear winner and the future of JavaScript. Node.js has full ESM support since v12. All modern browsers support ESM natively. New projects should use ESM by default. However, millions of npm packages still use CommonJS, so understanding interoperability is crucial. Bun, Deno, and modern bundlers all prefer ESM but maintain CJS compatibility.