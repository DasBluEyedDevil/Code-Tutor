// Solution: Switch Expression with yield
String getGrade(int score) {
    return switch (score / 10) {
        case 10, 9 -> {
            println("Excellent!");
            yield "A";
        }
        case 8 -> "B";
        case 7 -> "C";
        case 6 -> "D";
        default -> "F";
    };
}

void main() {
    println(getGrade(95));
}