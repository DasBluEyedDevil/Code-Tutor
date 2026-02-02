---
type: "EXAMPLE"
title: "@JsonKey: Customizing Field Serialization"
---

The `@JsonKey` annotation gives you fine-grained control over how individual fields are serialized and deserialized.

**Basic @JsonKey Usage**

```dart
import 'package:json_annotation/json_annotation.dart';

part 'product.g.dart';

@JsonSerializable()
class Product {
  final int id;
  
  // Custom JSON key name
  @JsonKey(name: 'product_name')
  final String name;
  
  // Default value if missing from JSON
  @JsonKey(defaultValue: 0.0)
  final double price;
  
  // Ignore this field in serialization
  @JsonKey(includeToJson: false, includeFromJson: false)
  final String? localCache;
  
  // Required field - throws if missing
  @JsonKey(required: true)
  final String sku;
  
  // Nullable field with explicit null handling
  @JsonKey(name: 'discount_percent')
  final double? discountPercent;
  
  Product({
    required this.id,
    required this.name,
    required this.price,
    this.localCache,
    required this.sku,
    this.discountPercent,
  });
  
  factory Product.fromJson(Map<String, dynamic> json) => _$ProductFromJson(json);
  Map<String, dynamic> toJson() => _$ProductToJson(this);
}
```

**Handling Enums**

Enums are common in API responses. json_serializable handles them automatically:

```dart
enum OrderStatus {
  pending,
  processing,
  shipped,
  delivered,
  cancelled,
}

@JsonSerializable()
class Order {
  final int id;
  
  // Enum is serialized as string by default
  final OrderStatus status;
  
  // Use @JsonValue for custom enum values
  final PaymentMethod paymentMethod;
  
  Order({
    required this.id,
    required this.status,
    required this.paymentMethod,
  });
  
  factory Order.fromJson(Map<String, dynamic> json) => _$OrderFromJson(json);
  Map<String, dynamic> toJson() => _$OrderToJson(this);
}

// Custom enum values using @JsonValue
enum PaymentMethod {
  @JsonValue('credit_card')
  creditCard,
  
  @JsonValue('bank_transfer')
  bankTransfer,
  
  @JsonValue('paypal')
  paypal,
  
  @JsonValue('crypto')
  cryptocurrency,
}
```

**Unknown Enum Values**

Handle unexpected enum values gracefully:

```dart
@JsonEnum(alwaysCreate: true)
enum UserRole {
  @JsonValue('admin')
  admin,
  
  @JsonValue('moderator')
  moderator,
  
  @JsonValue('user')
  user,
  
  @JsonValue('unknown')
  unknown,  // Fallback for unrecognized values
}

@JsonSerializable()
class Account {
  final int id;
  
  @JsonKey(unknownEnumValue: UserRole.unknown)
  final UserRole role;
  
  Account({required this.id, required this.role});
  
  factory Account.fromJson(Map<String, dynamic> json) => _$AccountFromJson(json);
  Map<String, dynamic> toJson() => _$AccountToJson(this);
}
```

**Custom Converters with @JsonKey**

For complex types, create custom converters:

```dart
import 'package:json_annotation/json_annotation.dart';

part 'event.g.dart';

// Custom converter for Color
class ColorConverter implements JsonConverter<Color, int> {
  const ColorConverter();
  
  @override
  Color fromJson(int json) => Color(json);
  
  @override
  int toJson(Color color) => color.value;
}

// Custom converter for Duration (stored as seconds)
class DurationSecondsConverter implements JsonConverter<Duration, int> {
  const DurationSecondsConverter();
  
  @override
  Duration fromJson(int seconds) => Duration(seconds: seconds);
  
  @override
  int toJson(Duration duration) => duration.inSeconds;
}

@JsonSerializable()
class CalendarEvent {
  final String id;
  final String title;
  
  @JsonKey(name: 'start_time')
  final DateTime startTime;
  
  @DurationSecondsConverter()
  @JsonKey(name: 'duration_seconds')
  final Duration duration;
  
  @ColorConverter()
  @JsonKey(name: 'color_value')
  final Color color;
  
  CalendarEvent({
    required this.id,
    required this.title,
    required this.startTime,
    required this.duration,
    required this.color,
  });
  
  factory CalendarEvent.fromJson(Map<String, dynamic> json) => 
      _$CalendarEventFromJson(json);
  Map<String, dynamic> toJson() => _$CalendarEventToJson(this);
}
```

**Nullable DateTime with Custom Converter**

```dart
class NullableDateTimeConverter implements JsonConverter<DateTime?, String?> {
  const NullableDateTimeConverter();
  
  @override
  DateTime? fromJson(String? json) {
    if (json == null || json.isEmpty) return null;
    return DateTime.tryParse(json);
  }
  
  @override
  String? toJson(DateTime? dateTime) => dateTime?.toIso8601String();
}

@JsonSerializable()
class Task {
  final int id;
  final String title;
  
  @NullableDateTimeConverter()
  @JsonKey(name: 'completed_at')
  final DateTime? completedAt;
  
  Task({required this.id, required this.title, this.completedAt});
  
  factory Task.fromJson(Map<String, dynamic> json) => _$TaskFromJson(json);
  Map<String, dynamic> toJson() => _$TaskToJson(this);
}
```

