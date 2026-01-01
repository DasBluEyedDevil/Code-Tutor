---
type: "THEORY"
title: "Exercise 1: Media Player System"
---


**Goal**: Create a flexible media player system using interfaces.

**Requirements**:
1. Interface `Playable` with methods: `play()`, `pause()`, `stop()`
2. Interface `Downloadable` with method: `download()`
3. Class `Song` implements `Playable` and `Downloadable`
4. Class `Podcast` implements `Playable` and `Downloadable`
5. Class `LiveStream` implements only `Playable` (can't download)
6. Create a playlist that can hold any `Playable` item

---

