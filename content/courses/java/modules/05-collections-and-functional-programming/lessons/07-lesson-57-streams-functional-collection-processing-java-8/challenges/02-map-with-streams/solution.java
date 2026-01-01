import module java.base;

void main() {
    var names = List.of("alice", "bob", "charlie");
    var upper = names.stream()
        .map(String::toUpperCase)
        .toList();
    println(upper);
}