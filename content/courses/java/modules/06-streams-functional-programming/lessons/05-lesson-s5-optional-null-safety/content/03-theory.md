---
type: "THEORY"
title: "Using Optionals Safely"
---

Extracting values from Optional:

orElse(default): Return value or default if empty
  String name = opt.orElse("Unknown");

orElseGet(supplier): Return value or compute default lazily
  String name = opt.orElseGet(() -> expensiveComputation());
  // Supplier only called if Optional is empty

orElseThrow(): Return value or throw exception
  String name = opt.orElseThrow();  // NoSuchElementException
  String name = opt.orElseThrow(() -> new UserNotFoundException(id));

Conditional actions:

ifPresent(consumer): Execute if value present
  opt.ifPresent(name -> System.out.println(name));

ifPresentOrElse(consumer, emptyAction): Java 9+
  opt.ifPresentOrElse(
      name -> System.out.println("Found: " + name),
      () -> System.out.println("Not found")
  );

AVOID these anti-patterns:
  if (opt.isPresent()) { opt.get(); }  // Just use orElse/ifPresent!
  opt.get()  // NEVER without checking - defeats the purpose!