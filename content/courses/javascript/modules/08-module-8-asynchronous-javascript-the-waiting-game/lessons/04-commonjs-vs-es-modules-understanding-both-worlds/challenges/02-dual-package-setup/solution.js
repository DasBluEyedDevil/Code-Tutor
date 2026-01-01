const packageJson = {
  "name": "my-utils",
  "version": "1.0.0",
  "type": "module",
  "exports": {
    ".": {
      "types": "./dist/index.d.ts",
      "import": "./dist/index.mjs",
      "require": "./dist/index.cjs"
    },
    "./helpers": {
      "types": "./dist/helpers.d.ts",
      "import": "./dist/helpers.mjs",
      "require": "./dist/helpers.cjs"
    }
  }
};

console.log(JSON.stringify(packageJson, null, 2));