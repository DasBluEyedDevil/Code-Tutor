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

        // Write your switch expression below to check the type of 'obj'

    }
}
