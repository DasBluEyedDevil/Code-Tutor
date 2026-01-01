import module java.base;

void main() {
    var names = List.of("Alice", "Bob");
    names.forEach(System.out::println);
}