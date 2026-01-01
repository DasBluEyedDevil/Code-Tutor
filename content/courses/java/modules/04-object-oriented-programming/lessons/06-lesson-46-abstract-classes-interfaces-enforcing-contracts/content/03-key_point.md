---
type: "KEY_POINT"
title: "Abstract Class is Like a Cake Recipe Template"
---

ABSTRACT CLASS = Recipe template with mandatory steps:

Recipe Template:
1. Prepare batter (ABSTRACT - each cake type does it differently)
2. Pour into pan (CONCRETE - same for all cakes)
3. Bake (CONCRETE - same for all)
4. Decorate (ABSTRACT - each cake type unique)

You can't make a "generic cake" from the template.
You must make: Chocolate Cake, Vanilla Cake, etc.
Each MUST specify how to prepare batter and decorate.

JAVA:
abstract class Recipe {  // Can't instantiate
    abstract void prepareBatter();  // MUST implement
    void pourIntoPan() { ... }  // Inherited as-is
    void bake() { ... }  // Inherited as-is
    abstract void decorate();  // MUST implement
}

class ChocolateCake extends Recipe {
    void prepareBatter() { /* chocolate batter */ }
    void decorate() { /* chocolate frosting */ }
}