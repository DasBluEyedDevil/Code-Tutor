---
type: "WARNING"
title: "Dependency Injection Mistakes"
---

Avoid these common mistakes when implementing dependency injection with Riverpod:

### Mistake 1: Creating Instances Inside build()

```dart
// WRONG: Creating dependency inside the Notifier
@riverpod
class UserViewModel extends _$UserViewModel {
  @override
  Future<User> build() async {
    final repo = UserRepository();  // BAD! Created every rebuild!
    return repo.getUser();
  }
}

// CORRECT: Get from Riverpod
@riverpod
class UserViewModel extends _$UserViewModel {
  @override
  Future<User> build() async {
    final repo = ref.watch(userRepositoryProvider);  // GOOD!
    return repo.getUser();
  }
}
```

When you create instances inside build(), you cannot override them for testing, and you create new instances on every rebuild.

### Mistake 2: Not Using Providers for External Services

```dart
// WRONG: Using Firebase directly in ViewModel
@riverpod
class AuthViewModel extends _$AuthViewModel {
  @override
  User? build() {
    return FirebaseAuth.instance.currentUser;  // Can't mock this!
  }
}

// CORRECT: Create a provider for Firebase
@Riverpod(keepAlive: true)
FirebaseAuth firebaseAuth(Ref ref) => FirebaseAuth.instance;

@riverpod
class AuthViewModel extends _$AuthViewModel {
  @override
  User? build() {
    final auth = ref.watch(firebaseAuthProvider);  // Now mockable!
    return auth.currentUser;
  }
}
```

### Mistake 3: Circular Dependencies

```dart
// WRONG: A depends on B, B depends on A = CRASH!
@riverpod
ServiceA serviceA(Ref ref) {
  final b = ref.watch(serviceBProvider);
  return ServiceA(b);
}

@riverpod
ServiceB serviceB(Ref ref) {
  final a = ref.watch(serviceAProvider);  // Circular!
  return ServiceB(a);
}
```

**Fix**: Refactor to break the cycle. Usually this means:
- One service should not need the other
- Create a third service they both depend on
- Use callbacks or events instead of direct references

### Mistake 4: Overusing keepAlive

```dart
// WRONG: Everything is keepAlive
@Riverpod(keepAlive: true)
class ProductList extends _$ProductList { ... }  // Should dispose!

@Riverpod(keepAlive: true)
class UserProfile extends _$UserProfile { ... }  // Should dispose!
```

**keepAlive** prevents automatic cleanup. Only use it for truly global, app-lifetime services like:
- Auth state
- Analytics
- App configuration
- Cached API clients

Feature-specific ViewModels should NOT use keepAlive.

### Mistake 5: Accessing Providers Too Late

```dart
// WRONG: Accessing provider after dispose
@riverpod
class MyViewModel extends _$MyViewModel {
  @override
  int build() => 0;

  void doSomething() {
    Future.delayed(Duration(seconds: 5), () {
      final service = ref.read(someServiceProvider);  // Might crash!
      // Widget might have navigated away, disposing this ViewModel
    });
  }
}

// CORRECT: Check if still mounted
void doSomething() {
  Future.delayed(Duration(seconds: 5), () {
    // For AsyncNotifier, check state is not disposed
    if (!ref.exists(someServiceProvider)) return;
    final service = ref.read(someServiceProvider);
  });
}
```

```dart
// =====================================================
// COMMON DI MISTAKES - BEFORE AND AFTER
// =====================================================

// MISTAKE 1: Hard-coded dependencies
// ---------------------------------

// BEFORE (Untestable):
class OrderViewModel {
  final _repo = OrderRepository();      // Hard-coded!
  final _analytics = AnalyticsService(); // Hard-coded!
  final _storage = LocalStorage();       // Hard-coded!

  Future<void> placeOrder(Order order) async {
    await _repo.save(order);
    _analytics.track('order_placed');
    await _storage.setLastOrder(order);
  }
}

// AFTER (Testable with DI):
@riverpod
class OrderViewModel extends _$OrderViewModel {
  @override
  AsyncValue<void> build() => const AsyncData(null);

  Future<void> placeOrder(Order order) async {
    state = const AsyncLoading();

    final repo = ref.read(orderRepositoryProvider);
    final analytics = ref.read(analyticsProvider);
    final storage = ref.read(localStorageProvider);

    state = await AsyncValue.guard(() async {
      await repo.save(order);
      analytics.track('order_placed');
      await storage.setLastOrder(order);
    });
  }
}

// Now in tests:
testWidgets('tracks order placement', (tester) async {
  final mockAnalytics = MockAnalytics();

  await tester.pumpWidget(
    ProviderScope(
      overrides: [
        orderRepositoryProvider.overrideWith((ref) => MockOrderRepo()),
        analyticsProvider.overrideWith((ref) => mockAnalytics),
        localStorageProvider.overrideWith((ref) => MockStorage()),
      ],
      child: MyApp(),
    ),
  );

  // Trigger order placement...

  verify(mockAnalytics.track('order_placed')).called(1);
});

// MISTAKE 2: Circular dependencies
// --------------------------------

// WRONG: Creates infinite loop
@riverpod
CartService cartService(Ref ref) {
  return CartService(
    inventory: ref.watch(inventoryServiceProvider),
  );
}

@riverpod
InventoryService inventoryService(Ref ref) {
  return InventoryService(
    cart: ref.watch(cartServiceProvider),  // Circular!
  );
}

// FIX: Break the dependency
// Option A: Remove one direction of dependency
@riverpod
CartService cartService(Ref ref) {
  return CartService(
    inventory: ref.watch(inventoryServiceProvider),
  );
}

@riverpod
InventoryService inventoryService(Ref ref) {
  return InventoryService();  // Doesn't need cart
}

// Option B: Use callbacks instead of direct reference
@riverpod
InventoryService inventoryService(Ref ref) {
  return InventoryService(
    onStockChanged: (productId, quantity) {
      // Notify cart via separate mechanism
      ref.read(stockChangedProvider.notifier).state = (productId, quantity);
    },
  );
}
```
