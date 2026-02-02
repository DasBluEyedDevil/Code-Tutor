function diagnoseError(error) {
  // YOUR CODE HERE
  // Return an object with: type, severity, suggestion
  // Severity:
  //   - 'critical' for ReferenceError and SyntaxError
  //   - 'warning' for everything else
  // Suggestions:
  //   - TypeError: 'Check that the value is the correct type'
  //   - ReferenceError: 'Check that the variable is declared'
  //   - SyntaxError: 'Check the code syntax'
  //   - RangeError: 'Check that values are within valid ranges'
  //   - Default: 'Review the error message for details'
}

// Test cases
try { null.toString(); } catch (e) { console.log(diagnoseError(e)); }
try { undefinedVar; } catch (e) { console.log(diagnoseError(e)); }
try { new Array(-1); } catch (e) { console.log(diagnoseError(e)); }