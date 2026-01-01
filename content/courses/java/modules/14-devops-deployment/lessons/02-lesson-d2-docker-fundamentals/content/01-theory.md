---
type: "THEORY"
title: "What is Docker?"
---

Docker is a platform that packages your application and ALL its dependencies into a portable unit called a CONTAINER.

THINK OF IT LIKE SHIPPING:

BEFORE SHIPPING CONTAINERS:
- Load individual items onto ships
- Different sizes, shapes, fragile items
- Complex loading/unloading
- Items get damaged, lost, mixed up

AFTER SHIPPING CONTAINERS:
- Everything goes in a standard container
- Container fits on any ship, truck, train
- Predictable, stackable, secure
- Contents don't matter - container is container

DOCKER DOES THE SAME FOR SOFTWARE:
- Your app + Java + configs = one container
- Runs identically on any machine with Docker
- No more 'works on my machine'
- Dev, test, production - same container

KEY TERMINOLOGY:
- Image: The blueprint (like a class)
- Container: Running instance (like an object)
- Dockerfile: Recipe to build an image
- Registry: Storage for images (Docker Hub)