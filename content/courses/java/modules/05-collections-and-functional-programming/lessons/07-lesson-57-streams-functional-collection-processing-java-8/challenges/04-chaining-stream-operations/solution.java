import module java.base;

void main() {
    var numbers = List.of(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
    var result = numbers.stream()
        .filter(n -> n > 5)
        .map(n -> n * 2)
        .toList();
    println(result);
}