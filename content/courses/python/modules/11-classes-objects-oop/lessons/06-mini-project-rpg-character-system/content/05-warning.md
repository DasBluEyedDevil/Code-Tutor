---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. God Class with Too Many Responsibilities**
```python
# WRONG - One class does everything
class Game:
    def __init__(self):
        self.player_health = 100
        self.inventory = []
        self.enemies = []

    def attack(self): pass
    def defend(self): pass
    def render_graphics(self): pass
    def play_sound(self): pass
    def save_game(self): pass  # Too many responsibilities!

# CORRECT - Split into focused classes
class Player:
    def attack(self): pass
    def defend(self): pass

class Renderer:
    def render(self): pass

class AudioManager:
    def play_sound(self): pass
```

**2. Not Initializing Inherited Attributes**
```python
# WRONG - Child forgets parent's __init__
class Character:
    def __init__(self, name, health):
        self.name = name
        self.health = health

class Warrior(Character):
    def __init__(self, name, weapon):
        self.weapon = weapon  # health and name not set!

w = Warrior("Conan", "Sword")
print(w.health)  # AttributeError!

# CORRECT - Always call parent's __init__
class Warrior(Character):
    def __init__(self, name, health, weapon):
        super().__init__(name, health)
        self.weapon = weapon
```

**3. Modifying List Attributes from Multiple Characters**
```python
# WRONG - Shared class attribute modified by all
class Character:
    abilities = []  # Shared between all characters!

    def learn(self, ability):
        self.abilities.append(ability)

warrior = Character()
warrior.learn("Slash")
mage = Character()
print(mage.abilities)  # ['Slash'] - Wrong!

# CORRECT - Instance attribute for each character
class Character:
    def __init__(self):
        self.abilities = []  # Each instance gets own list
```

**4. Not Separating Game Logic from Display**
```python
# WRONG - Mixed logic and display
class Combat:
    def attack(self, attacker, defender):
        damage = attacker.power - defender.defense
        defender.health -= damage
        print(f"{attacker.name} hits for {damage}!")  # UI in logic!
        print("="*40)

# CORRECT - Separate concerns
class Combat:
    def attack(self, attacker, defender):
        damage = attacker.power - defender.defense
        defender.health -= damage
        return damage  # Return data, don't print

class Display:
    def show_attack(self, attacker, damage):
        print(f"{attacker.name} hits for {damage}!")
```

**5. Hardcoding Values Instead of Using Constants**
```python
# WRONG - Magic numbers throughout code
class Player:
    def __init__(self):
        self.health = 100
        self.max_health = 100
        self.damage = 10

    def heal(self):
        self.health = min(self.health + 25, 100)  # Magic numbers!

# CORRECT - Use class constants
class Player:
    MAX_HEALTH = 100
    BASE_DAMAGE = 10
    HEAL_AMOUNT = 25

    def __init__(self):
        self.health = self.MAX_HEALTH
        self.damage = self.BASE_DAMAGE

    def heal(self):
        self.health = min(self.health + self.HEAL_AMOUNT, self.MAX_HEALTH)
```