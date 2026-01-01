---
type: "THEORY"
title: "Grouping and Partitioning"
---

Collectors provides powerful grouping operations:

groupingBy(classifier): Group elements by a key
  Map<String, List<Person>> byDept = people.stream()
      .collect(Collectors.groupingBy(Person::department));
  // {Engineering=[Alice, Charlie], Marketing=[Bob, Diana]}

groupingBy with downstream collector:
  Map<String, Long> countByDept = people.stream()
      .collect(Collectors.groupingBy(
          Person::department,
          Collectors.counting()
      ));
  // {Engineering=2, Marketing=2}

  Map<String, Double> avgAgeByDept = people.stream()
      .collect(Collectors.groupingBy(
          Person::department,
          Collectors.averagingInt(Person::age)
      ));

partitioningBy(predicate): Split into true/false groups
  Map<Boolean, List<Person>> seniorVsJunior = people.stream()
      .collect(Collectors.partitioningBy(p -> p.age() >= 30));
  // {true=[Alice, Charlie], false=[Bob, Diana]}

Partitioning always returns exactly two groups (true and false), even if empty.