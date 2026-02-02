// Solution: Pattern Matching for instanceof
String describe(Object obj) {
    if (obj instanceof String s) {
        return "String: " + s;
    } else if (obj instanceof Integer n) {
        return "Number: " + n;
    } else {
        return "Unknown";
    }
}

void main() {
    IO.println(describe("Hello"));
}