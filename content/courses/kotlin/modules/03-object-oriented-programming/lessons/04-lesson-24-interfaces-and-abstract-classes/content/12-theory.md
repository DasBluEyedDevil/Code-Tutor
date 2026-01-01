---
type: "THEORY"
title: "Exercise 2: Smart Home System"
---


**Goal**: Create a smart home system with different device types.

**Requirements**:
1. Interface `SmartDevice` with properties: `name`, `isOn`, methods: `turnOn()`, `turnOff()`
2. Interface `Schedulable` with method: `schedule(time: String)`
3. Interface `VoiceControllable` with method: `respondToVoice(command: String)`
4. Class `SmartLight` implements all three interfaces
5. Class `SmartThermostat` implements `SmartDevice` and `Schedulable`
6. Class `SmartSpeaker` implements `SmartDevice` and `VoiceControllable`
7. Create a home controller that manages all devices

---

