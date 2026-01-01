---
type: "EXAMPLE"
title: "Step 1: Base Character Class"
---

**Base Character class features:**

**Encapsulation:**
- Private attributes: `__health`, `__mana`, `__experience`
- Properties with validation
- Health can't exceed max or go below 0

**Class attributes:**
- `total_characters` tracks all characters

**Instance methods:**
- `attack()`, `take_damage()`, `heal()`
- `add_experience()`, `level_up()`
- `add_item()`, `show_inventory()`

**Foundation for inheritance:**
- Children will override `special_attack()`

```python
class Character:
    """Base character class for RPG"""
    
    # Class attributes
    total_characters = 0
    
    def __init__(self, name, char_class, level=1):
        # Public attributes
        self.name = name
        self.char_class = char_class
        self.level = level
        
        # Private attributes (encapsulation)
        self.__health = 100
        self.__max_health = 100
        self.__mana = 50
        self.__max_mana = 50
        self.__experience = 0
        
        # Protected attributes
        self._inventory = []
        self._is_alive = True
        
        # Increment class counter
        Character.total_characters += 1
        print(f"Character created: {name} the {char_class} (Level {level})")
    
    # Properties with encapsulation
    @property
    def health(self):
        return self.__health
    
    @health.setter
    def health(self, value):
        """Set health with validation"""
        if value > self.__max_health:
            value = self.__max_health
        if value <= 0:
            value = 0
            self._is_alive = False
            print(f"{self.name} has been defeated!")
        self.__health = value
    
    @property
    def mana(self):
        return self.__mana
    
    @mana.setter
    def mana(self, value):
        """Set mana with validation"""
        if value > self.__max_mana:
            value = self.__max_mana
        if value < 0:
            value = 0
        self.__mana = value
    
    @property
    def max_health(self):
        return self.__max_health
    
    @property
    def max_mana(self):
        return self.__max_mana
    
    @property
    def experience(self):
        return self.__experience
    
    # Instance methods
    def attack(self, target):
        """Basic attack"""
        damage = 10 * self.level
        print(f"{self.name} attacks {target.name} for {damage} damage!")
        target.take_damage(damage)
        return damage
    
    def take_damage(self, amount):
        """Take damage"""
        self.health -= amount
        if self._is_alive:
            print(f"{self.name} has {self.health}/{self.max_health} HP remaining")
    
    def heal(self, amount):
        """Heal character"""
        old_health = self.health
        self.health += amount
        healed = self.health - old_health
        print(f"{self.name} healed for {healed} HP ({self.health}/{self.max_health})")
    
    def add_experience(self, amount):
        """Add experience and check for level up"""
        self.__experience += amount
        exp_needed = self.level * 100
        print(f"{self.name} gained {amount} XP ({self.__experience}/{exp_needed})")
        
        if self.__experience >= exp_needed:
            self.level_up()
    
    def level_up(self):
        """Level up character"""
        self.level += 1
        self.__max_health += 20
        self.__max_mana += 10
        self.health = self.__max_health  # Full heal on level up
        self.mana = self.__max_mana
        self.__experience = 0
        print(f"🎉 {self.name} leveled up to {self.level}!")
        print(f"   HP: {self.__max_health}, Mana: {self.__max_mana}")
    
    def add_item(self, item):
        """Add item to inventory"""
        self._inventory.append(item)
        print(f"{self.name} obtained: {item}")
    
    def show_inventory(self):
        """Display inventory"""
        if not self._inventory:
            print(f"{self.name}'s inventory is empty")
        else:
            print(f"{self.name}'s inventory:")
            for i, item in enumerate(self._inventory, 1):
                print(f"  {i}. {item}")
    
    def special_attack(self, target):
        """Override in subclasses"""
        raise NotImplementedError("Subclass must implement special_attack")
    
    def __str__(self):
        status = "ALIVE" if self._is_alive else "DEFEATED"
        return f"{self.name} | {self.char_class} | Lvl {self.level} | HP: {self.health}/{self.max_health} | Mana: {self.mana}/{self.max_mana} | {status}"
    
    @classmethod
    def get_character_count(cls):
        return f"Total characters created: {cls.total_characters}"

print("=== Testing Base Character ===")
char = Character("TestHero", "Generic")
print(char)
char.add_item("Health Potion")
char.add_item("Sword")
char.show_inventory()
print(Character.get_character_count())
```
