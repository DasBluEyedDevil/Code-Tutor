---
type: "THEORY"
title: "Rive State Machines - Interactive Animations"
---


**State Machines** are Rive's killer feature. They let you define:

1. **States** - Different animation states (idle, walking, jumping)
2. **Transitions** - How to move between states
3. **Inputs** - User-controlled values that trigger transitions

**Input Types:**

| Type | Description | Example |
|------|-------------|--------|
| **Trigger** | One-shot event | "jump" button pressed |
| **Boolean** | On/off state | "isHovered", "isActive" |
| **Number** | Numeric value | Speed, health percentage |

**How It Works:**

```
User Tap -> Find Input -> Change Value -> State Machine Reacts -> Animation Plays
```

The state machine evaluates conditions you set in the Rive editor and automatically transitions between animations based on input values.

**Example State Machine Design:**

```
States:
- Idle (default)
- Walking
- Running
- Jumping

Inputs:
- speed (Number: 0-100)
- jump (Trigger)

Transitions:
- Idle -> Walking: when speed > 0
- Walking -> Running: when speed > 50
- Any -> Jumping: when jump is triggered
- Jumping -> previous: when animation completes
```

