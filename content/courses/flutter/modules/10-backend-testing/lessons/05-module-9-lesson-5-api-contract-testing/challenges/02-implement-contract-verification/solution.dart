import 'dart:convert';
import 'package:test/test.dart';
import 'package:http/http.dart' as http;

void main() {
  group('Product API Contract Tests', () {
    final baseUrl = 'http://localhost:8080';
    
    test('GET /api/products/{id} returns valid Product', () async {
      final response = await http.get(
        Uri.parse('$baseUrl/api/products/123'),
        headers: {'Accept': 'application/json'},
      );
      
      expect(response.statusCode, 200);
      
      final json = jsonDecode(response.body) as Map<String, dynamic>;
      
      // Verify all required fields exist
      expect(json.containsKey('id'), isTrue);
      expect(json.containsKey('name'), isTrue);
      expect(json.containsKey('price'), isTrue);
      expect(json.containsKey('inStock'), isTrue);
      expect(json.containsKey('category'), isTrue);
      
      // Verify field types
      expect(json['id'], isA<int>());
      expect(json['name'], isA<String>());
      expect(json['price'], isA<num>());
      expect((json['price'] as num) >= 0, isTrue);
      expect(json['inStock'], isA<bool>());
      expect(json['category'], isA<String>());
    });
  });
}