import 'package:freezed_annotation/freezed_annotation.dart';

part 'product.freezed.dart';
part 'product.g.dart';

@freezed
class Category with _$Category {
  @JsonSerializable(fieldRename: FieldRename.snake)
  const factory Category({
    required int id,
    required String name,
    int? parentId,
  }) = _Category;

  factory Category.fromJson(Map<String, dynamic> json) => _$CategoryFromJson(json);
}

@freezed
class Product with _$Product {
  const Product._();

  @JsonSerializable(fieldRename: FieldRename.snake)
  const factory Product({
    required int id,
    required String name,
    String? description,
    required double price,
    double? discountPrice,
    required Category category,
    @Default([]) List<String> tags,
    @Default(true) bool inStock,
    required DateTime createdAt,
  }) = _Product;

  factory Product.fromJson(Map<String, dynamic> json) => _$ProductFromJson(json);

  bool get hasDiscount => discountPrice != null && discountPrice! < price;

  int get discountPercentage {
    if (!hasDiscount) return 0;
    return ((1 - discountPrice! / price) * 100).round();
  }
}