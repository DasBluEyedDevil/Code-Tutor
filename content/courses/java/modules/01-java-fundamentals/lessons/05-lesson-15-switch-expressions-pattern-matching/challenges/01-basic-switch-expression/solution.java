// Solution: Basic Switch Expression
String getSeason(int month) {
    return switch (month) {
        case 12, 1, 2 -> "Winter";
        case 3, 4, 5 -> "Spring";
        case 6, 7, 8 -> "Summer";
        case 9, 10, 11 -> "Fall";
        default -> "Unknown";
    };
}

void main() {
    println(getSeason(7));
}