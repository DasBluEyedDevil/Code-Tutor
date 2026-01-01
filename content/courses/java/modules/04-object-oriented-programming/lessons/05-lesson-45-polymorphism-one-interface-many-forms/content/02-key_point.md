---
type: "KEY_POINT"
title: "Polymorphism is Like a Universal Remote"
---

UNIVERSAL REMOTE:
- Has a "Power" button
- Works for TV, DVD player, sound system
- SAME button, DIFFERENT behavior depending on device

POLYMORPHISM IN JAVA:
Animal[] animals = {new Dog(), new Cat(), new Bird()};

for (Animal a : animals) {
    a.makeSound();  // SAME method call
}

Output:
Woof!  (Dog's version)
Meow!  (Cat's version)
Chirp!  (Bird's version)

ONE interface (makeSound), MANY forms (each animal's unique sound).
That's poly (many) + morph (forms) = polymorphism!