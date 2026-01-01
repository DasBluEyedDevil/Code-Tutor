// Solution: Simple Phonebook
// This demonstrates using HashMap for key-value storage

import java.util.HashMap;

public class Phonebook {
    // HashMap to store name -> phone number mappings
    HashMap<String, String> contacts;
    
    // Constructor initializes the HashMap
    public Phonebook() {
        contacts = new HashMap<>();
    }
    
    // Add a contact
    public void addContact(String name, String phone) {
        contacts.put(name, phone);
    }
    
    // Get phone number by name (returns null if not found)
    public String getPhone(String name) {
        return contacts.get(name);
    }
}