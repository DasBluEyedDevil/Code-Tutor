// Solution: Override Animal Sounds
// This demonstrates polymorphism and method overriding

class Animal {
    public String makeSound() {
        return "Some sound";
    }
}

class Dog extends Animal {
    // Override makeSound to return "Bark"
    // @Override annotation is best practice
    @Override
    public String makeSound() {
        return "Bark";
    }
}

class Cat extends Animal {
    // Override makeSound to return "Meow"
    @Override
    public String makeSound() {
        return "Meow";
    }
}