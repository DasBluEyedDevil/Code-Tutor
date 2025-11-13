from flask import Flask, request, jsonify
from flask_cors import CORS
import sys
import io
import traceback
import signal
import resource
from contextlib import redirect_stdout, redirect_stderr

app = Flask(__name__)
CORS(app)

# Security: Limit resource usage
MAX_EXECUTION_TIME = 5  # seconds
MAX_MEMORY_MB = 256

class TimeoutError(Exception):
    pass

def timeout_handler(signum, frame):
    raise TimeoutError("Code execution timed out")

def execute_python_code(code, test_cases=None):
    """
    Execute Python code in a restricted environment
    """
    # Set resource limits
    try:
        resource.setrlimit(resource.RLIMIT_AS, (MAX_MEMORY_MB * 1024 * 1024, MAX_MEMORY_MB * 1024 * 1024))
    except:
        pass  # Resource limits might not work on all systems

    # Set timeout
    signal.signal(signal.SIGALRM, timeout_handler)
    signal.alarm(MAX_EXECUTION_TIME)

    # Capture output
    stdout_buffer = io.StringIO()
    stderr_buffer = io.StringIO()

    try:
        # Create restricted globals
        restricted_globals = {
            '__builtins__': {
                'print': print,
                'len': len,
                'range': range,
                'str': str,
                'int': int,
                'float': float,
                'bool': bool,
                'list': list,
                'dict': dict,
                'tuple': tuple,
                'set': set,
                'abs': abs,
                'min': min,
                'max': max,
                'sum': sum,
                'sorted': sorted,
                'enumerate': enumerate,
                'zip': zip,
                'map': map,
                'filter': filter,
                'all': all,
                'any': any,
                'isinstance': isinstance,
                'type': type,
                'Exception': Exception,
                'ValueError': ValueError,
                'TypeError': TypeError,
                'KeyError': KeyError,
                'IndexError': IndexError,
            }
        }

        # Execute code
        with redirect_stdout(stdout_buffer), redirect_stderr(stderr_buffer):
            exec(code, restricted_globals)

        signal.alarm(0)  # Cancel the alarm

        output = stdout_buffer.getvalue()
        errors = stderr_buffer.getvalue()

        # Run test cases if provided
        test_results = None
        if test_cases:
            test_results = run_test_cases(code, test_cases, restricted_globals)

        return {
            'success': True,
            'output': output if output else '(No output)',
            'error': errors if errors else None,
            'testResults': test_results
        }

    except TimeoutError as e:
        signal.alarm(0)
        return {
            'success': False,
            'output': stdout_buffer.getvalue(),
            'error': str(e)
        }
    except Exception as e:
        signal.alarm(0)
        return {
            'success': False,
            'output': stdout_buffer.getvalue(),
            'error': f"{type(e).__name__}: {str(e)}\n{traceback.format_exc()}"
        }
    finally:
        stdout_buffer.close()
        stderr_buffer.close()

def run_test_cases(code, test_cases, globals_dict):
    """
    Run test cases against the code
    """
    passed = 0
    failed = 0
    details = []

    for test in test_cases:
        try:
            # Execute code in fresh environment
            test_globals = globals_dict.copy()
            stdout_buffer = io.StringIO()

            with redirect_stdout(stdout_buffer):
                exec(code, test_globals)

            actual_output = stdout_buffer.getvalue().strip()
            expected_output = test.get('expectedOutput', '').strip()

            if actual_output == expected_output:
                passed += 1
                details.append({
                    'testId': test.get('id', 'unknown'),
                    'passed': True,
                    'expected': expected_output,
                    'actual': actual_output
                })
            else:
                failed += 1
                details.append({
                    'testId': test.get('id', 'unknown'),
                    'passed': False,
                    'expected': expected_output,
                    'actual': actual_output,
                    'message': f"Expected '{expected_output}' but got '{actual_output}'"
                })

            stdout_buffer.close()

        except Exception as e:
            failed += 1
            details.append({
                'testId': test.get('id', 'unknown'),
                'passed': False,
                'expected': test.get('expectedOutput', ''),
                'actual': '',
                'message': f"Error: {str(e)}"
            })

    return {
        'passed': passed,
        'failed': failed,
        'details': details
    }

@app.route('/health', methods=['GET'])
def health():
    return jsonify({'status': 'ok', 'service': 'python-executor'})

@app.route('/execute', methods=['POST'])
def execute():
    try:
        data = request.json
        code = data.get('code', '')
        test_cases = data.get('testCases', None)

        if not code:
            return jsonify({'success': False, 'error': 'No code provided'}), 400

        result = execute_python_code(code, test_cases)
        return jsonify(result)

    except Exception as e:
        return jsonify({
            'success': False,
            'error': f"Server error: {str(e)}"
        }), 500

if __name__ == '__main__':
    print("🐍 Python executor service starting on port 4000...")
    app.run(host='0.0.0.0', port=4000, debug=False)
