# Lesson Content Quality Review

## Context

You are reviewing a programming lesson for a tutorial platform. Your task is to evaluate the lesson content for quality and identify improvements.

## Course Information

- **Course:** Python Backend Development (python)
- **Module:** Exception Handling
- **Lesson:** Mini-Project: Robust Calculator with Complete Error Handling (ID: 08_06)
- **Difficulty:** intermediate
- **Estimated Time:** 45 minutes

## Current Lesson Content

{
    "id":  "08_06",
    "contentSections":  [
                            {
                                "type":  "ANALOGY",
                                "title":  "Project Overview: The Unbreakable Calculator",
                                "content":  "**The Challenge:** Build a calculator that NEVER crashes, no matter what the user does.\n\n**Real-world scenario:** You\u0027re building a calculator for a critical system (medical device, aircraft control, financial trading). It CANNOT crash. Users might:\n- Type letters instead of numbers\n- Divide by zero\n- Enter empty input\n- Use invalid operations\n- Provide huge numbers that cause overflow\n- Enter malformed expressions\n\nYour calculator must handle ALL of these gracefully, showing helpful error messages and letting users try again.\n\n**What you\u0027ll build:**\n\n1. **Basic Calculator Functions:**\n   - Add, subtract, multiply, divide\n   - Power, square root, modulo\n\n2. **Advanced Features:**\n   - Expression evaluation (\"2 + 3 * 4\")\n   - Memory storage (store results)\n   - Calculation history\n\n3. **Error Handling:**\n   - Custom exceptions for calculator-specific errors\n   - Input validation for all operations\n   - Safe expression evaluation\n   - Graceful error recovery\n\n4. **Defensive Programming:**\n   - Type checking\n   - Range validation\n   - Clear error messages\n   - Finally blocks for cleanup\n\n**Project structure:**\n- Custom exception classes\n- Calculator class with validated methods\n- Interactive REPL (Read-Eval-Print Loop)\n- Comprehensive error handling throughout\n\nThis project demonstrates production-level code that\u0027s robust, maintainable, and user-friendly."
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "Complete Implementation: Robust Calculator",
                                "content":  "This production-ready calculator demonstrates:\n\n**1. Custom Exception Hierarchy:**\n- Base `CalculatorError` for all calculator errors\n- Specific exceptions for each error type\n- Clear, descriptive error messages\n\n**2. Comprehensive Validation:**\n- Type checking with `isinstance()`\n- Range validation (no infinity, NaN)\n- Defensive programming throughout\n\n**3. Error Handling Patterns:**\n- Try/except for risky operations\n- Finally for cleanup (logging)\n- EAFP approach where appropriate\n\n**4. User-Friendly Features:**\n- Clear error messages\n- Calculation history\n- Memory storage\n- Interactive REPL\n\n**5. Production-Level Code:**\n- Type hints for documentation\n- Docstrings for all methods\n- Proper exception hierarchy\n- Edge case handling",
                                "code":  "\"\"\"Robust Calculator with Comprehensive Error Handling\n\nA production-ready calculator that handles all possible errors gracefully.\nDemonstrates: custom exceptions, validation, defensive programming, EAFP.\n\"\"\"\n\nimport math\nfrom typing import List, Tuple\n\n# ============================================================================\n# Custom Exception Classes\n# ============================================================================\n\nclass CalculatorError(Exception):\n    \"\"\"Base exception for all calculator errors.\"\"\"\n    pass\n\nclass InvalidOperationError(CalculatorError):\n    \"\"\"Raised when an invalid operation is requested.\"\"\"\n    pass\n\nclass InvalidNumberError(CalculatorError):\n    \"\"\"Raised when input cannot be converted to a number.\"\"\"\n    pass\n\nclass DivisionByZeroError(CalculatorError):\n    \"\"\"Raised when attempting to divide by zero.\"\"\"\n    pass\n\nclass NegativeSquareRootError(CalculatorError):\n    \"\"\"Raised when attempting square root of negative number.\"\"\"\n    pass\n\n# ============================================================================\n# Calculator Class\n# ============================================================================\n\nclass RobustCalculator:\n    \"\"\"Calculator with comprehensive error handling.\"\"\"\n    \n    def __init__(self):\n        \"\"\"Initialize calculator with memory and history.\"\"\"\n        self.memory = 0.0\n        self.history: List[Tuple[str, float]] = []\n        self.operations = {\n            \u0027add\u0027: self.add,\n            \u0027subtract\u0027: self.subtract,\n            \u0027multiply\u0027: self.multiply,\n            \u0027divide\u0027: self.divide,\n            \u0027power\u0027: self.power,\n            \u0027sqrt\u0027: self.square_root,\n            \u0027modulo\u0027: self.modulo,\n        }\n    \n    def _validate_number(self, value, param_name=\"value\"):\n        \"\"\"Validate that value is a number.\n        \n        Args:\n            value: Value to validate\n            param_name: Name for error messages\n            \n        Returns:\n            float: Validated number\n            \n        Raises:\n            InvalidNumberError: If value is not a number\n        \"\"\"\n        if not isinstance(value, (int, float)):\n            raise InvalidNumberError(\n                f\"{param_name} must be a number, got {type(value).__name__}\"\n            )\n        \n        # Check for infinity and NaN\n        if math.isinf(value):\n            raise InvalidNumberError(f\"{param_name} cannot be infinity\")\n        if math.isnan(value):\n            raise InvalidNumberError(f\"{param_name} cannot be NaN\")\n        \n        return float(value)\n    \n    def _record_operation(self, operation: str, result: float):\n        \"\"\"Record operation in history.\"\"\"\n        self.history.append((operation, result))\n        # Keep only last 10 operations\n        if len(self.history) \u003e 10:\n            self.history.pop(0)\n    \n    def add(self, a, b):\n        \"\"\"Add two numbers.\"\"\"\n        a = self._validate_number(a, \"first number\")\n        b = self._validate_number(b, \"second number\")\n        result = a + b\n        self._record_operation(f\"{a} + {b}\", result)\n        return result\n    \n    def subtract(self, a, b):\n        \"\"\"Subtract b from a.\"\"\"\n        a = self._validate_number(a, \"first number\")\n        b = self._validate_number(b, \"second number\")\n        result = a - b\n        self._record_operation(f\"{a} - {b}\", result)\n        return result\n    \n    def multiply(self, a, b):\n        \"\"\"Multiply two numbers.\"\"\"\n        a = self._validate_number(a, \"first number\")\n        b = self._validate_number(b, \"second number\")\n        result = a * b\n        self._record_operation(f\"{a} * {b}\", result)\n        return result\n    \n    def divide(self, a, b):\n        \"\"\"Divide a by b.\n        \n        Raises:\n            DivisionByZeroError: If b is zero\n        \"\"\"\n        a = self._validate_number(a, \"dividend\")\n        b = self._validate_number(b, \"divisor\")\n        \n        if b == 0:\n            raise DivisionByZeroError(\n                \"Cannot divide by zero. Please enter a non-zero divisor.\"\n            )\n        \n        result = a / b\n        self._record_operation(f\"{a} / {b}\", result)\n        return result\n    \n    def power(self, base, exponent):\n        \"\"\"Raise base to exponent.\"\"\"\n        base = self._validate_number(base, \"base\")\n        exponent = self._validate_number(exponent, \"exponent\")\n        \n        try:\n            result = base ** exponent\n            # Check for overflow\n            if math.isinf(result):\n                raise InvalidNumberError(\n                    f\"Result too large: {base}^{exponent} causes overflow\"\n                )\n            self._record_operation(f\"{base} ^ {exponent}\", result)\n            return result\n        except OverflowError:\n            raise InvalidNumberError(\n                f\"Result too large: {base}^{exponent} causes overflow\"\n            )\n    \n    def square_root(self, number):\n        \"\"\"Calculate square root.\n        \n        Raises:\n            NegativeSquareRootError: If number is negative\n        \"\"\"\n        number = self._validate_number(number, \"number\")\n        \n        if number \u003c 0:\n            raise NegativeSquareRootError(\n                f\"Cannot calculate square root of negative number: {number}. \"\n                f\"Use complex numbers for this operation.\"\n            )\n        \n        result = math.sqrt(number)\n        self._record_operation(f\"sqrt({number})\", result)\n        return result\n    \n    def modulo(self, a, b):\n        \"\"\"Calculate a modulo b.\n        \n        Raises:\n            DivisionByZeroError: If b is zero\n        \"\"\"\n        a = self._validate_number(a, \"dividend\")\n        b = self._validate_number(b, \"divisor\")\n        \n        if b == 0:\n            raise DivisionByZeroError(\n                \"Cannot perform modulo with zero divisor\"\n            )\n        \n        result = a % b\n        self._record_operation(f\"{a} % {b}\", result)\n        return result\n    \n    def store_memory(self, value):\n        \"\"\"Store value in memory.\"\"\"\n        value = self._validate_number(value, \"memory value\")\n        self.memory = value\n        return value\n    \n    def recall_memory(self):\n        \"\"\"Recall value from memory.\"\"\"\n        return self.memory\n    \n    def clear_memory(self):\n        \"\"\"Clear memory.\"\"\"\n        self.memory = 0.0\n    \n    def show_history(self):\n        \"\"\"Show calculation history.\"\"\"\n        if not self.history:\n            return \"No calculation history\"\n        \n        result = \"\\nCalculation History (last 10):\\n\"\n        result += \"-\" * 40 + \"\\n\"\n        for i, (operation, value) in enumerate(self.history, 1):\n            result += f\"{i}. {operation} = {value}\\n\"\n        return result\n    \n    def clear_history(self):\n        \"\"\"Clear calculation history.\"\"\"\n        self.history.clear()\n\n# ============================================================================\n# Interactive Calculator REPL\n# ============================================================================\n\ndef safe_float_input(prompt):\n    \"\"\"Safely get a float from user.\n    \n    Args:\n        prompt: Prompt to display\n        \n    Returns:\n        float: Validated number\n        \n    Raises:\n        InvalidNumberError: If input is not a valid number\n        ValueError: If input is empty\n    \"\"\"\n    user_input = input(prompt).strip()\n    \n    if not user_input:\n        raise ValueError(\"Input cannot be empty\")\n    \n    try:\n        return float(user_input)\n    except ValueError:\n        raise InvalidNumberError(\n            f\"\u0027{user_input}\u0027 is not a valid number. Please enter a numeric value.\"\n        )\n\ndef run_calculator():\n    \"\"\"Run the interactive calculator.\"\"\"\n    calc = RobustCalculator()\n    \n    print(\"=\"*50)\n    print(\"  ROBUST CALCULATOR - Error Handling Demo\")\n    print(\"=\"*50)\n    print(\"\\nAvailable operations:\")\n    print(\"  add, subtract, multiply, divide\")\n    print(\"  power, sqrt, modulo\")\n    print(\"  memory, recall, clear-memory\")\n    print(\"  history, clear-history\")\n    print(\"  quit\\n\")\n    \n    while True:\n        try:\n            # Get operation\n            print(\"-\" * 50)\n            operation = input(\"\\nEnter operation (or \u0027quit\u0027 to exit): \").strip().lower()\n            \n            if not operation:\n                print(\"❌ Please enter an operation\")\n                continue\n            \n            if operation in (\u0027quit\u0027, \u0027exit\u0027, \u0027q\u0027):\n                print(\"\\n👋 Thanks for using Robust Calculator!\")\n                break\n            \n            # Handle special operations\n            if operation == \u0027history\u0027:\n                print(calc.show_history())\n                continue\n            \n            if operation == \u0027clear-history\u0027:\n                calc.clear_history()\n                print(\"✓ History cleared\")\n                continue\n            \n            if operation == \u0027recall\u0027:\n                print(f\"Memory: {calc.recall_memory()}\")\n                continue\n            \n            if operation == \u0027clear-memory\u0027:\n                calc.clear_memory()\n                print(\"✓ Memory cleared\")\n                continue\n            \n            # Operations requiring one operand\n            if operation == \u0027sqrt\u0027:\n                num = safe_float_input(\"Enter number: \")\n                result = calc.square_root(num)\n                print(f\"\\n✓ sqrt({num}) = {result}\")\n                continue\n            \n            if operation == \u0027memory\u0027:\n                value = safe_float_input(\"Enter value to store: \")\n                calc.store_memory(value)\n                print(f\"✓ Stored {value} in memory\")\n                continue\n            \n            # Operations requiring two operands\n            if operation in calc.operations:\n                a = safe_float_input(\"Enter first number: \")\n                b = safe_float_input(\"Enter second number: \")\n                \n                result = calc.operations[operation](a, b)\n                print(f\"\\n✓ Result: {result}\")\n            else:\n                raise InvalidOperationError(\n                    f\"Unknown operation \u0027{operation}\u0027. \"\n                    f\"Type an operation name from the list above.\"\n                )\n        \n        except InvalidNumberError as e:\n            print(f\"\\n❌ Invalid Number: {e}\")\n        except DivisionByZeroError as e:\n            print(f\"\\n❌ Division Error: {e}\")\n        except NegativeSquareRootError as e:\n            print(f\"\\n❌ Math Error: {e}\")\n        except InvalidOperationError as e:\n            print(f\"\\n❌ Invalid Operation: {e}\")\n        except ValueError as e:\n            print(f\"\\n❌ Input Error: {e}\")\n        except CalculatorError as e:\n            print(f\"\\n❌ Calculator Error: {e}\")\n        except KeyboardInterrupt:\n            print(\"\\n\\n👋 Calculator interrupted. Goodbye!\")\n            break\n        except Exception as e:\n            print(f\"\\n❌ Unexpected Error: {e}\")\n            print(\"Please report this bug!\")\n        finally:\n            # Always runs - could be used for logging\n            pass\n\n# ============================================================================\n# Demo and Tests\n# ============================================================================\n\nprint(\"=\" * 60)\nprint(\"DEMO: Testing Robust Calculator\")\nprint(\"=\" * 60)\n\ncalc = RobustCalculator()\n\nprint(\"\\n1. Valid Operations:\")\nprint(f\"   10 + 5 = {calc.add(10, 5)}\")\nprint(f\"   20 - 8 = {calc.subtract(20, 8)}\")\nprint(f\"   6 * 7 = {calc.multiply(6, 7)}\")\nprint(f\"   15 / 3 = {calc.divide(15, 3)}\")\nprint(f\"   2 ^ 8 = {calc.power(2, 8)}\")\nprint(f\"   sqrt(16) = {calc.square_root(16)}\")\n\nprint(\"\\n2. Error Handling:\")\n\ntry:\n    calc.divide(10, 0)\nexcept DivisionByZeroError as e:\n    print(f\"   ✓ Caught division by zero: {e}\")\n\ntry:\n    calc.square_root(-4)\nexcept NegativeSquareRootError as e:\n    print(f\"   ✓ Caught negative sqrt: {e}\")\n\ntry:\n    calc.add(\"hello\", 5)\nexcept InvalidNumberError as e:\n    print(f\"   ✓ Caught invalid number: {e}\")\n\ntry:\n    calc.power(10, 1000)  # Huge number\nexcept InvalidNumberError as e:\n    print(f\"   ✓ Caught overflow: {e}\")\n\nprint(\"\\n3. Memory and History:\")\ncalc.store_memory(42)\nprint(f\"   Stored: {calc.recall_memory()}\")\nprint(f\"   {calc.show_history()}\")\n\nprint(\"\\n✓ All error handling working correctly!\")\nprint(\"\\nTo run interactive calculator, call run_calculator()\")",
                                "language":  "python"
                            },
                            {
                                "type":  "ANALOGY",
                                "title":  "Code Architecture Breakdown",
                                "content":  "**Exception Hierarchy:**\n```\nCalculatorError (base)\n├── InvalidOperationError\n├── InvalidNumberError\n├── DivisionByZeroError\n└── NegativeSquareRootError\n```\n\nBenefits:\n- Can catch all calculator errors with `except CalculatorError`\n- Can catch specific errors separately\n- Clear error types for debugging\n\n**Validation Pattern (used throughout):**\n```python\ndef operation(self, param):\n    # 1. Validate input\n    param = self._validate_number(param, \"param_name\")\n    \n    # 2. Check business rules\n    if param == 0:\n        raise DivisionByZeroError(\"...\")\n    \n    # 3. Perform operation\n    result = do_calculation(param)\n    \n    # 4. Record for history\n    self._record_operation(\"...\", result)\n    \n    # 5. Return result\n    return result\n```\n\n**Error Handling in REPL:**\n```python\nwhile True:\n    try:\n        # Get user input\n        # Validate\n        # Execute operation\n        # Display result\n    except SpecificError1 as e:\n        # Handle gracefully\n    except SpecificError2 as e:\n        # Handle gracefully\n    except Exception as e:\n        # Unexpected errors\n    finally:\n        # Always cleanup\n```\n\nBenefits:\n- User never sees crashes\n- Specific errors get specific messages\n- Unexpected errors are logged\n- Calculator keeps running\n\n**Defensive Programming Checklist (applied throughout):**\n\n✅ **Every input validated:**\n- Type check: `isinstance(value, (int, float))`\n- Range check: `if b == 0: raise DivisionByZeroError`\n- Special values: Check for infinity, NaN\n\n✅ **Clear error messages:**\n- What went wrong: \"Cannot divide by zero\"\n- What was expected: \"Please enter a non-zero divisor\"\n- How to fix it: Actionable guidance\n\n✅ **Edge cases handled:**\n- Division by zero\n- Square root of negative\n- Overflow (numbers too large)\n- Invalid types\n- Empty input\n\n✅ **Graceful degradation:**\n- Error occurs → Show message → Let user try again\n- History preserved even after errors\n- Memory preserved\n\n**Why This Is Production-Ready:**\n\n1. **Never crashes** - all errors caught\n2. **Clear feedback** - users know what went wrong\n3. **Maintainable** - clear structure, documented\n4. **Testable** - each function isolated\n5. **Extensible** - easy to add new operations\n6. **User-friendly** - helpful messages, history, memory\n\nThis is how professional developers write robust code."
                            },
                            {
                                "type":  "KEY_POINT",
                                "title":  "Key Takeaways",
                                "content":  "- **Production code never crashes.** Use comprehensive error handling to catch and recover from all possible errors gracefully.\n- **Custom exception hierarchies** make error handling precise and code self-documenting. Create a base exception class and specific subclasses.\n- **Validate everything.** Never trust user input. Check type, range, format, and edge cases before processing.\n- **Clear error messages are essential.** Tell users what went wrong, what was expected, and how to fix it. Vague errors frustrate users.\n- **Use try/except/finally** for risky operations. Finally ensures cleanup happens even if errors occur.\n- **Defensive programming mindset:** Assume everything can fail. Guard against it with validation, error handling, and fallbacks.\n- **EAFP (try/except) vs LBYL (if/check):** Python prefers EAFP, but use what makes sense for your situation.\n- **Separate concerns:** Validation logic, business logic, error handling, and UI should be separate. Makes code testable and maintainable.\n- **Document your code** with docstrings, type hints, and comments. Future you (and other developers) will thank you.\n- **Test error cases** as thoroughly as success cases. Error handling code needs testing too!\n- **Security matters:** When using dangerous functions like eval(), validate and sanitize input thoroughly. Use whitelists, not blacklists."
                            }
                        ],
    "challenges":  [
                       {
                           "type":  "FREE_CODING",
                           "id":  "08_06-challenge-3",
                           "title":  "Your Challenge: Extend the Calculator",
                           "description":  "**Challenge 1: Add Scientific Functions**\n\nAdd these operations to the calculator:\n1. `log(number, base)` - Logarithm with base (default base 10)\n2. `sin(angle)` - Sine (angle in degrees)\n3. `cos(angle)` - Cosine (angle in degrees)\n\nRequirements:\n- Validate inputs (log of negative raises error)\n- Convert degrees to radians for sin/cos\n- Handle edge cases\n- Add to history\n- Update interactive menu\n\n**Challenge 2: Expression Evaluator**\n\nAdd ability to evaluate expressions like \"2 + 3 * 4\":\n- Use `eval()` CAREFULLY with validation\n- Only allow numbers and operators\n- Prevent code injection\n- Handle syntax errors\n\n**Challenge 3: Persistent History**\n\nSave calculation history to a file:\n- Use try/except/finally for file operations\n- Handle FileNotFoundError\n- Create file if doesn\u0027t exist\n- Load history on startup\n- Save on exit\n\n**Starter code for Challenge 1:**",
                           "instructions":  "**Challenge 1: Add Scientific Functions**\n\nAdd these operations to the calculator:\n1. `log(number, base)` - Logarithm with base (default base 10)\n2. `sin(angle)` - Sine (angle in degrees)\n3. `cos(angle)` - Cosine (angle in degrees)\n\nRequirements:\n- Validate inputs (log of negative raises error)\n- Convert degrees to radians for sin/cos\n- Handle edge cases\n- Add to history\n- Update interactive menu\n\n**Challenge 2: Expression Evaluator**\n\nAdd ability to evaluate expressions like \"2 + 3 * 4\":\n- Use `eval()` CAREFULLY with validation\n- Only allow numbers and operators\n- Prevent code injection\n- Handle syntax errors\n\n**Challenge 3: Persistent History**\n\nSave calculation history to a file:\n- Use try/except/finally for file operations\n- Handle FileNotFoundError\n- Create file if doesn\u0027t exist\n- Load history on startup\n- Save on exit\n\n**Starter code for Challenge 1:**",
                           "starterCode":  "import math\n\nclass RobustCalculator:\n    # ... (previous code) ...\n    \n    def logarithm(self, number, base=10):\n        \"\"\"Calculate logarithm.\n        \n        Args:\n            number: Number to calculate log of\n            base: Logarithm base (default 10)\n            \n        Raises:\n            InvalidNumberError: If number \u003c= 0 or base \u003c= 0\n        \"\"\"\n        # TODO: Validate number and base\n        # TODO: Check number \u003e 0 (can\u0027t log negative or zero)\n        # TODO: Check base \u003e 0 and base != 1\n        # TODO: Calculate using math.log(number, base)\n        # TODO: Record in history\n        # TODO: Return result\n        pass\n    \n    def sine(self, angle_degrees):\n        \"\"\"Calculate sine (angle in degrees).\n        \n        Args:\n            angle_degrees: Angle in degrees\n            \n        Returns:\n            float: Sine of angle\n        \"\"\"\n        # TODO: Validate angle\n        # TODO: Convert to radians: math.radians(angle_degrees)\n        # TODO: Calculate: math.sin(angle_radians)\n        # TODO: Record in history\n        # TODO: Return result\n        pass\n    \n    def cosine(self, angle_degrees):\n        \"\"\"Calculate cosine (angle in degrees).\"\"\"\n        # TODO: Similar to sine\n        pass",
                           "solution":  "import math\n\n# Custom exception for invalid number operations\nclass InvalidNumberError(ValueError):\n    \"\"\"Raised when a number is invalid for the operation.\"\"\"\n    pass\n\nclass RobustCalculator:\n    \"\"\"Calculator with scientific functions and history.\"\"\"\n    \n    def __init__(self):\n        self.history = []\n    \n    def _record(self, operation, result):\n        \"\"\"Record an operation in history.\"\"\"\n        self.history.append({\u0027operation\u0027: operation, \u0027result\u0027: result})\n    \n    def logarithm(self, number, base=10):\n        \"\"\"Calculate logarithm with validation.\"\"\"\n        # Step 1: Validate number\n        if not isinstance(number, (int, float)):\n            raise InvalidNumberError(f\"Number must be numeric, got {type(number).__name__}\")\n        if number \u003c= 0:\n            raise InvalidNumberError(\"Logarithm requires a positive number\")\n        \n        # Step 2: Validate base\n        if not isinstance(base, (int, float)):\n            raise InvalidNumberError(f\"Base must be numeric, got {type(base).__name__}\")\n        if base \u003c= 0 or base == 1:\n            raise InvalidNumberError(\"Base must be positive and not equal to 1\")\n        \n        # Step 3: Calculate and record\n        result = math.log(number, base)\n        self._record(f\"log{base}({number})\", result)\n        return result\n    \n    def sine(self, angle_degrees):\n        \"\"\"Calculate sine (angle in degrees).\"\"\"\n        # Step 1: Validate input\n        if not isinstance(angle_degrees, (int, float)):\n            raise InvalidNumberError(f\"Angle must be numeric, got {type(angle_degrees).__name__}\")\n        \n        # Step 2: Convert to radians and calculate\n        angle_radians = math.radians(angle_degrees)\n        result = math.sin(angle_radians)\n        \n        # Step 3: Record and return\n        self._record(f\"sin({angle_degrees}deg)\", result)\n        return result\n    \n    def cosine(self, angle_degrees):\n        \"\"\"Calculate cosine (angle in degrees).\"\"\"\n        # Step 1: Validate input\n        if not isinstance(angle_degrees, (int, float)):\n            raise InvalidNumberError(f\"Angle must be numeric, got {type(angle_degrees).__name__}\")\n        \n        # Step 2: Convert to radians and calculate\n        angle_radians = math.radians(angle_degrees)\n        result = math.cos(angle_radians)\n        \n        # Step 3: Record and return\n        self._record(f\"cos({angle_degrees}deg)\", result)\n        return result\n    \n    def show_history(self):\n        \"\"\"Display calculation history.\"\"\"\n        for i, entry in enumerate(self.history, 1):\n            print(f\"{i}. {entry[\u0027operation\u0027]} = {entry[\u0027result\u0027]:.6f}\")\n\n# Test the calculator\ncalc = RobustCalculator()\n\nprint(\"=== Scientific Calculator Tests ===\")\n\n# Logarithm tests\nprint(f\"log10(100) = {calc.logarithm(100)}\")\nprint(f\"log2(8) = {calc.logarithm(8, 2)}\")\n\n# Trigonometry tests\nprint(f\"sin(90) = {calc.sine(90)}\")\nprint(f\"cos(0) = {calc.cosine(0)}\")\nprint(f\"sin(45) = {calc.sine(45)}\")\n\n# Error handling test\ntry:\n    calc.logarithm(-5)\nexcept InvalidNumberError as e:\n    print(f\"Error: {e}\")\n\n# Show history\nprint(\"\\n=== Calculation History ===\")\ncalc.show_history()",
                           "language":  "python",
                           "testCases":  [
                                             {
                                                 "id":  "test-1",
                                                 "description":  "Code runs without errors",
                                                 "expectedOutput":  "",
                                                 "isVisible":  true
                                             }
                                         ],
                           "hints":  [
                                         {
                                             "level":  1,
                                             "text":  "For logarithm: check if number \u003c= 0: raise InvalidNumberError(\u0027Log requires positive number\u0027). For sin/cos: angle_rad = math.radians(angle), then math.sin(angle_rad). Don\u0027t forget to record in history!"
                                         }
                                     ],
                           "commonMistakes":  [
                                                  {
                                                      "mistake":  "Forgetting the colon after if/for/while",
                                                      "consequence":  "SyntaxError",
                                                      "correction":  "Add : at the end of the line"
                                                  },
                                                  {
                                                      "mistake":  "Using = instead of == for comparison",
                                                      "consequence":  "Assignment instead of comparison",
                                                      "correction":  "Use == for equality checks"
                                                  },
                                                  {
                                                      "mistake":  "Incorrect indentation",
                                                      "consequence":  "IndentationError",
                                                      "correction":  "Use consistent 4-space indentation"
                                                  }
                                              ],
                           "difficulty":  "beginner"
                       }
                   ],
    "difficulty":  "intermediate",
    "title":  "Mini-Project: Robust Calculator with Complete Error Handling",
    "estimatedMinutes":  45
}

## Review Instructions

Perform the following analysis:

### 1. Accuracy Check
- Verify all code examples are syntactically correct
- Confirm technical explanations match current python documentation
- Search the web for the latest python version and verify examples work with it
- Flag any deprecated APIs, syntax, or patterns

### 2. Completeness Check
- Does the lesson cover all concepts needed for a beginner to understand this topic?
- Are there missing explanations between concepts that would confuse a learner?
- Does the lesson have:
  - [ ] A clear analogy or real-world example (ANALOGY section)
  - [ ] Theoretical explanation (THEORY section)
  - [ ] Working code example (EXAMPLE section)
  - [ ] Common mistakes to avoid (WARNING section)
  - [ ] At least one practice challenge

### 3. Freshness Check
- Search for "python Mini-Project: Robust Calculator with Complete Error Handling 2024 2025" to find latest practices
- Compare lesson content against current best practices
- Identify any outdated patterns or recommendations

### 4. Pedagogical Gap Analysis
- What prerequisite knowledge is assumed but not explained?
- What follow-up questions would a learner likely have?
- What practical applications or use cases are missing?
- Are the challenges appropriately scaffolded for the difficulty level?

## Output Format

Provide your review as structured JSON:

```json
{
  "lessonId": "08_06",
  "reviewDate": "YYYY-MM-DD",
  "overallScore": 1-10,
  "accuracy": {
    "score": 1-10,
    "issues": ["issue 1", "issue 2"],
    "recommendations": ["fix 1", "fix 2"]
  },
  "completeness": {
    "score": 1-10,
    "missingSections": ["section type needed"],
    "gaps": ["gap 1", "gap 2"],
    "recommendations": ["add X", "expand Y"]
  },
  "freshness": {
    "score": 1-10,
    "outdatedItems": ["item 1"],
    "currentVersion": "language version checked",
    "recommendations": ["update X to Y"]
  },
  "pedagogicalGaps": {
    "score": 1-10,
    "missingPrerequisites": ["concept 1"],
    "unansweredQuestions": ["question learner would have"],
    "missingUseCases": ["practical application"],
    "recommendations": ["add section on X"]
  },
  "contentLengthIssues": {
    "shortSections": [
      {"sectionTitle": "title", "currentLength": 42, "recommendation": "expand to explain X"}
    ]
  },
  "suggestedNewContent": [
    {
      "sectionType": "THEORY|EXAMPLE|WARNING|etc",
      "title": "suggested title",
      "contentOutline": "what this section should cover"
    }
  ],
  "priority": "HIGH|MEDIUM|LOW"
}
```

