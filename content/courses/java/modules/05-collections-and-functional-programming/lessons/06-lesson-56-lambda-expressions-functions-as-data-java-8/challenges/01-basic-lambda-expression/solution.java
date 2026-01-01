import module java.base;

void main() {
    var names = new ArrayList<>(List.of("Charlie", "Bob", "Alice", "Dan"));
    names.sort((a, b) -> a.length() - b.length());
    println(names.get(0));
}