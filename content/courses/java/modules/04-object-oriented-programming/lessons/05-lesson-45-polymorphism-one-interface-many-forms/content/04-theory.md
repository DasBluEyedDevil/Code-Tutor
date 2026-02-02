---
type: "THEORY"
title: "Polymorphism in Action"
---

The MAGIC of polymorphism:

Animal myPet = new Dog();  // Dog IS-A Animal
myPet.makeSound();  // Calls Dog's version → "Woof!"

myPet = new Cat();  // Now it's a Cat
myPet.makeSound();  // Calls Cat's version → "Meow!"

This enables POWERFUL patterns:

void feedAnimal(Animal a) {
    IO.println("Feeding...");
    a.makeSound();  // Will call the RIGHT version automatically!
}

feedAnimal(new Dog());  // "Feeding..." then "Woof!"
feedAnimal(new Cat());  // "Feeding..." then "Meow!"
feedAnimal(new Bird());  // "Feeding..." then "Chirp!"

ONE method works with ALL Animal types!