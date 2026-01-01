---
type: "EXAMPLE"
title: "Step 3: Combat Simulation"
---

**Combat system demonstrating all OOP concepts:**

**1. Polymorphism:**
- Different classes all have `attack()` and `special_attack()`
- Same combat loop works for any character type
- Each implements abilities differently

**2. Encapsulation:**
- Health/mana managed through properties
- Validation prevents invalid values
- Private attributes protected

**3. Inheritance:**
- All share Character base functionality
- Each adds unique features
- Override methods for custom behavior

**4. Class attributes:**
- `total_characters` tracks all instances
- Each class has specific bonuses

**Complete RPG foundation ready to extend!**

```python
def combat_demo():
    """Demonstrate polymorphic combat"""
    print("\n" + "="*50)
    print("COMBAT ARENA")
    print("="*50)
    
    # Create fighters
    hero = Warrior("Aragorn", level=2)
    villain = Mage("Saruman", level=2)
    
    print("\n=== Round 1: Basic Attacks ===")
    hero.attack(villain)
    villain.attack(hero)
    
    print("\n=== Round 2: More Attacks ===")
    hero.attack(villain)
    villain.attack(hero)
    
    print("\n=== Round 3: Special Attacks ===")
    # Warrior has rage now
    hero.special_attack(villain)
    # Mage casts fireball
    villain.special_attack(hero)
    
    print("\n=== Current Status ===")
    print(hero)
    print(villain)
    
    print("\n=== Healing ===")
    hero.heal(30)
    villain.restore_mana(50)
    
    print("\n=== Final Status ===")
    print(hero)
    print(villain)

def polymorphism_demo():
    """Demonstrate polymorphism - different classes, same interface"""
    print("\n" + "="*50)
    print("POLYMORPHISM DEMONSTRATION")
    print("="*50)
    
    # Create different character types
    characters = [
        Warrior("Brutor"),
        Mage("Merlin"),
        Rogue("Shadow")
    ]
    
    dummy = Character("Target Dummy", "Dummy")
    dummy.health = 500  # Lots of HP
    
    print("\nAll characters attacking same target:\n")
    
    # Polymorphic loop - works with any Character subclass
    for char in characters:
        char.attack(dummy)
        print()
    
    print("\n=== Special Attacks ===")
    
    # Warrior builds rage first
    warrior = characters[0]
    warrior.attack(dummy)
    warrior.attack(dummy)
    warrior.special_attack(dummy)  # Berserker Rage
    
    # Mage has mana
    mage = characters[1]
    mage.special_attack(dummy)  # Fireball
    
    # Rogue needs stealth
    rogue = characters[2]
    rogue.enter_stealth()
    rogue.special_attack(dummy)  # Backstab
    
    print(f"\n{dummy.name}: {dummy.health}/{dummy.max_health} HP")

def leveling_demo():
    """Demonstrate leveling system"""
    print("\n" + "="*50)
    print("LEVELING SYSTEM")
    print("="*50)
    
    hero = Rogue("Leveling Hero")
    print(f"\nStarting stats: {hero}")
    
    print("\n=== Gaining Experience ===")
    hero.add_experience(50)
    hero.add_experience(30)
    hero.add_experience(30)  # Should level up (needs 100)
    
    print(f"\nAfter level up: {hero}")
    
    print("\n=== Inventory Management ===")
    hero.add_item("Dagger of Shadows")
    hero.add_item("Health Potion x3")
    hero.add_item("Smoke Bomb")
    hero.show_inventory()

# Run all demonstrations
combat_demo()
polymorphism_demo()
leveling_demo()

print("\n" + "="*50)
print(Character.get_character_count())
print("="*50)
```
