import module java.base;

void main() {
    var numbers = List.of(1, 2, 3, 4, 5);
    var sum = numbers.stream()
        .mapToInt(Integer::intValue)
        .sum();
    IO.println(sum);
}