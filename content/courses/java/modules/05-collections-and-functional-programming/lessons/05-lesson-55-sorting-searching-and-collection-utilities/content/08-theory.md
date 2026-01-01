---
type: "THEORY"
title: "💻 Real-World Example: Leaderboard"
---

```java
Building a game leaderboard:

import java.util.*;

class Player {
    String name;
    int score;
    
    Player(String name, int score) {
        this.name = name;
        this.score = score;
    }
}

ArrayList<Player> players = new ArrayList<>();
players.add(new Player("Alice", 500));
players.add(new Player("Bob", 750));
players.add(new Player("Carol", 300));

// Sort by score (highest first)
Collections.sort(players, (p1, p2) -> p2.score - p1.score);

// Display leaderboard
for (int i = 0; i < players.size(); i++) {
    Player p = players.get(i);
    System.out.println((i+1) + ". " + p.name + ": " + p.score);
}

Output:
1. Bob: 750
2. Alice: 500
3. Carol: 300
```