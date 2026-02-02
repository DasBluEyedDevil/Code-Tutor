// Solution: Result Type Pattern

sealed interface Result permits Success, Failure {}

record Success(String message) implements Result {}
record Failure(String error) implements Result {}

Result checkNumber(int num) {
    if (num > 0) {
        return new Success("Positive");
    } else {
        return new Failure("Not positive");
    }
}

String formatResult(Result result) {
    return switch (result) {
        case Success(String msg) -> "Success: " + msg;
        case Failure(String err) -> "Failure: " + err;
    };
}

void main() {
    Result result = checkNumber(5);
    IO.println(formatResult(result));
}