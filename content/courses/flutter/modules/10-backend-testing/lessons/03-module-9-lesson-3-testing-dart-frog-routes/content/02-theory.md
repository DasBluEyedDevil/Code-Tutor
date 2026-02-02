---
type: "THEORY"
title: "Why Test Your Backend Routes?"
---

Backend testing is fundamentally different from frontend testing. Your API is the contract between your server and clients - if it breaks, every app depending on it fails.

Key reasons to test Dart Frog routes:

1. **Contract Verification**: Ensure your API returns exactly what clients expect
2. **Regression Prevention**: Catch breaking changes before deployment
3. **Edge Case Coverage**: Handle malformed requests, missing data, unauthorized access
4. **Documentation**: Tests serve as executable documentation of API behavior
5. **Refactoring Safety**: Change implementation without breaking functionality

Dart Frog provides excellent testing utilities that make writing route tests straightforward and expressive.