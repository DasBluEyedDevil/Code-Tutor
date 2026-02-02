import module java.base;

void main() {
    var names = List.of("Alice", "Bob");
    names.forEach(IO::println);
}