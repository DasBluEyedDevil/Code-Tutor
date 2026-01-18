import java.util.Scanner;

public class Main {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        String input = scanner.nextLine();

        // This converts your input into an Object (Integer, String, or null)
        Object obj;
        if (input.equals("null")) {
            obj = null;
        } else {
            try {
                obj = Integer.parseInt(input);
            } catch (NumberFormatException e) {
                obj = input;
            }
        }

        String result = switch (obj) {
            case Integer i -> "Integer: " + i;
            case String s -> "String: " + s;
            case null -> "It is null";
            default -> "Unknown type";
        };

        System.out.println(result);
    }
}
