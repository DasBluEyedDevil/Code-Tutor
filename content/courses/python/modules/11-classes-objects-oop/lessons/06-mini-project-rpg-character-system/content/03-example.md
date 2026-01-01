---
type: "EXAMPLE"
title: "Step 2: Warrior, Mage, Rogue Classes"
---

**Three character classes demonstrating inheritance:**

**Warrior:**
- Higher starting HP (150)
- Builds rage with attacks
- Special: Berserker Rage (requires 50 rage)

**Mage:**
- Lower HP (80), higher mana (100)
- Special: Fireball (costs 30 mana)
- Unique: restore_mana() ability

**Rogue:**
- Normal HP
- Can enter stealth
- Special: Backstab (requires stealth)
- Critical hit chance on normal attacks

**Key inheritance features:**
- All call `super().__init__()`
- Override `special_attack()` polymorphically
- Some override `attack()` with enhanced versions
- Access parent's private attributes via name mangling

```python
class Warrior(Character):
    """Warrior class - high HP, melee damage"""
    
    # Class attribute
    armor_bonus = 1.5
    
    def __init__(self, name, level=1):
        super().__init__(name, "Warrior", level)
        # Warriors start with more health
        self._Character__max_health = 150
        self.health = 150
        self._rage = 0
    
    def special_attack(self, target):
        """Berserker Rage - powerful attack"""
        if self._rage < 50:
            print(f"{self.name} needs 50 rage (has {self._rage})")
            return 0
        
        damage = 30 * self.level
        self._rage = 0
        print(f"⚔️  {self.name} uses BERSERKER RAGE!")
        print(f"   Devastating attack for {damage} damage!")
        target.take_damage(damage)
        return damage
    
    def attack(self, target):
        """Override attack to build rage"""
        damage = super().attack(target)
        self._rage += 20
        print(f"   Rage increased to {self._rage}")
        return damage

class Mage(Character):
    """Mage class - magic damage, low HP"""
    
    # Class attribute
    spell_power = 1.8
    
    def __init__(self, name, level=1):
        super().__init__(name, "Mage", level)
        # Mages have less health but more mana
        self._Character__max_health = 80
        self._Character__max_mana = 100
        self.health = 80
        self.mana = 100
    
    def special_attack(self, target):
        """Fireball - magical damage"""
        mana_cost = 30
        if self.mana < mana_cost:
            print(f"{self.name} needs {mana_cost} mana (has {self.mana})")
            return 0
        
        self.mana -= mana_cost
        damage = int(25 * self.level * self.spell_power)
        print(f"🔥 {self.name} casts FIREBALL!")
        print(f"   Magical damage: {damage}")
        target.take_damage(damage)
        return damage
    
    def restore_mana(self, amount=20):
        """Mage-specific ability"""
        old_mana = self.mana
        self.mana += amount
        restored = self.mana - old_mana
        print(f"{self.name} meditates, restoring {restored} mana ({self.mana}/{self.max_mana})")

class Rogue(Character):
    """Rogue class - speed and critical hits"""
    
    # Class attribute
    crit_chance = 0.3  # 30% crit chance
    
    def __init__(self, name, level=1):
        super().__init__(name, "Rogue", level)
        self._stealth = False
    
    def special_attack(self, target):
        """Backstab - critical damage from stealth"""
        if not self._stealth:
            print(f"{self.name} needs to be in stealth!")
            return 0
        
        damage = 40 * self.level
        self._stealth = False
        print(f"🗡️  {self.name} uses BACKSTAB from stealth!")
        print(f"   Critical damage: {damage}")
        target.take_damage(damage)
        return damage
    
    def enter_stealth(self):
        """Rogue-specific ability"""
        self._stealth = True
        print(f"{self.name} enters stealth mode...")
    
    def attack(self, target):
        """Override attack for critical hits"""
        import random
        damage = 10 * self.level
        
        if random.random() < self.crit_chance:
            damage *= 2
            print(f"💥 CRITICAL HIT!")
        
        print(f"{self.name} attacks {target.name} for {damage} damage!")
        target.take_damage(damage)
        return damage

print("\n=== Creating Different Classes ===")
warrior = Warrior("Conan")
mage = Mage("Gandalf")
rogue = Rogue("Assassin")

print("\n=== Character Stats ===")
print(warrior)
print(mage)
print(rogue)

print(f"\n{Character.get_character_count()}")
```
