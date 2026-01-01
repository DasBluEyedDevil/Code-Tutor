---
type: "THEORY"
title: "Custom Hero Animations"
---


**flightShuttleBuilder** lets you customize what's displayed during the flight animation.

**Use Cases:**
- Show a loading indicator during flight
- Display different content while transitioning
- Add effects like blur or shadows during flight
- Handle aspect ratio changes between source and destination

**placeholderBuilder** defines what's shown in the original position while the Hero is flying.

**Use Cases:**
- Show a ghost/skeleton of the original content
- Display "Loading..." placeholder
- Maintain layout while content flies away

**createRectTween** customizes the animation path.

**Use Cases:**
- Arc motion instead of straight line
- Custom easing for the position
- Overshooting effects

