function diagnoseError(error) {
  const suggestions = {
    'TypeError': 'Check that the value is the correct type',
    'ReferenceError': 'Check that the variable is declared',
    'SyntaxError': 'Check the code syntax',
    'RangeError': 'Check that values are within valid ranges',
    'URIError': 'Check the URI encoding/decoding',
    'EvalError': 'Avoid using eval()'
  };
  
  const criticalErrors = ['ReferenceError', 'SyntaxError'];
  
  return {
    type: error.name,
    severity: criticalErrors.includes(error.name) ? 'critical' : 'warning',
    suggestion: suggestions[error.name] || 'Review the error message for details'
  };
}

// Test cases
try { null.toString(); } catch (e) { console.log(diagnoseError(e)); }
// { type: 'TypeError', severity: 'warning', suggestion: 'Check that the value is the correct type' }

try { undefinedVar; } catch (e) { console.log(diagnoseError(e)); }
// { type: 'ReferenceError', severity: 'critical', suggestion: 'Check that the variable is declared' }

try { new Array(-1); } catch (e) { console.log(diagnoseError(e)); }
// { type: 'RangeError', severity: 'warning', suggestion: 'Check that values are within valid ranges' }