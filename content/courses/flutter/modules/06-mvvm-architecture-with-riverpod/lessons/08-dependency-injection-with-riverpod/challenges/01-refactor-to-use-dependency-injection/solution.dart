// File: lib/features/weather/weather_feature.dart
// SOLUTION: Proper Dependency Injection with Riverpod

import 'dart:convert';
import 'package:http/http.dart' as http;
import 'package:riverpod_annotation/riverpod_annotation.dart';

part 'weather_feature.g.dart';

// Weather Model (unchanged)
class Weather {
  final String city;
  final double temperature;
  final String condition;

  Weather({
    required this.city,
    required this.temperature,
    required this.condition,
  });

  factory Weather.fromJson(Map<String, dynamic> json) {
    return Weather(
      city: json['city'],
      temperature: (json['temperature'] as num).toDouble(),
      condition: json['condition'],
    );
  }
}

// =====================================================
// LAYER 1: API CLIENT (lowest level)
// =====================================================

class WeatherApiClient {
  final String baseUrl;

  WeatherApiClient({this.baseUrl = 'https://api.weather.example.com'});

  Future<Map<String, dynamic>> get(String endpoint) async {
    final response = await http.get(Uri.parse('$baseUrl$endpoint'));
    if (response.statusCode == 200) {
      return jsonDecode(response.body);
    }
    throw Exception('Failed to fetch weather: ${response.statusCode}');
  }
}

// Provider for API Client - keepAlive because it's used app-wide
@Riverpod(keepAlive: true)
WeatherApiClient weatherApiClient(Ref ref) {
  return WeatherApiClient();
}

// =====================================================
// LAYER 2: REPOSITORY (data access)
// =====================================================

abstract class WeatherRepository {
  Future<Weather> getWeather(String city);
}

class ApiWeatherRepository implements WeatherRepository {
  final WeatherApiClient _client;

  ApiWeatherRepository(this._client);

  @override
  Future<Weather> getWeather(String city) async {
    final data = await _client.get('/weather?city=$city');
    return Weather.fromJson(data);
  }
}

// Provider for Repository - injects WeatherApiClient
@riverpod
WeatherRepository weatherRepository(Ref ref) {
  // DEPENDENCY INJECTION: Get API client from Riverpod
  final client = ref.watch(weatherApiClientProvider);
  return ApiWeatherRepository(client);
}

// =====================================================
// LAYER 3: VIEWMODEL (business logic)
// =====================================================

@riverpod
class WeatherViewModel extends _$WeatherViewModel {
  @override
  Future<Weather?> build() async {
    // No city selected initially
    return null;
  }

  Future<void> loadWeather(String city) async {
    state = const AsyncLoading();

    // DEPENDENCY INJECTION: Get repository from Riverpod
    final repository = ref.read(weatherRepositoryProvider);

    state = await AsyncValue.guard(() => repository.getWeather(city));
  }

  void clear() {
    state = const AsyncData(null);
  }
}

// =====================================================
// THE DEPENDENCY CHAIN IS NOW:
// =====================================================
//
// WeatherViewModel (weatherViewModelProvider)
//        |
//        | ref.read(weatherRepositoryProvider)
//        v
// WeatherRepository (weatherRepositoryProvider)
//        |
//        | ref.watch(weatherApiClientProvider)
//        v
// WeatherApiClient (weatherApiClientProvider)
//
// Each layer can be overridden independently for testing!

// =====================================================
// BONUS: HOW TO TEST WITH OVERRIDES
// =====================================================

/*
// Mock implementation for testing
class MockWeatherRepository implements WeatherRepository {
  final Weather? mockWeather;
  final bool shouldFail;

  MockWeatherRepository({
    this.mockWeather,
    this.shouldFail = false,
  });

  @override
  Future<Weather> getWeather(String city) async {
    await Future.delayed(Duration(milliseconds: 10));

    if (shouldFail) {
      throw Exception('Mock network error');
    }

    return mockWeather ?? Weather(
      city: city,
      temperature: 72.0,
      condition: 'Sunny',
    );
  }
}

// In your test file:
import 'package:flutter_test/flutter_test.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

void main() {
  group('WeatherViewModel', () {
    testWidgets('displays weather after loading', (tester) async {
      await tester.pumpWidget(
        ProviderScope(
          overrides: [
            // Override at repository level for testing
            weatherRepositoryProvider.overrideWith((ref) => MockWeatherRepository(
              mockWeather: Weather(
                city: 'Test City',
                temperature: 75.0,
                condition: 'Cloudy',
              ),
            )),
          ],
          child: MaterialApp(home: WeatherScreen()),
        ),
      );

      // Test your widget behavior...
    });

    testWidgets('shows error when fetch fails', (tester) async {
      await tester.pumpWidget(
        ProviderScope(
          overrides: [
            weatherRepositoryProvider.overrideWith((ref) => MockWeatherRepository(
              shouldFail: true,
            )),
          ],
          child: MaterialApp(home: WeatherScreen()),
        ),
      );

      // Trigger weather load and verify error state...
    });

    test('unit test ViewModel in isolation', () async {
      final container = ProviderContainer(
        overrides: [
          weatherRepositoryProvider.overrideWith((ref) => MockWeatherRepository()),
        ],
      );

      // Load weather
      await container.read(weatherViewModelProvider.notifier).loadWeather('NYC');

      // Verify state
      final state = container.read(weatherViewModelProvider);
      expect(state.hasValue, isTrue);
      expect(state.value?.city, equals('NYC'));

      container.dispose();
    });
  });

  // Test with different API configurations
  test('can override API client for staging environment', () {
    final container = ProviderContainer(
      overrides: [
        weatherApiClientProvider.overrideWith((ref) => WeatherApiClient(
          baseUrl: 'https://staging.api.weather.example.com',
        )),
      ],
    );

    final client = container.read(weatherApiClientProvider);
    expect(client.baseUrl, contains('staging'));

    container.dispose();
  });
}
*/

// KEY IMPROVEMENTS FROM STARTER CODE:
//
// 1. Created weatherApiClientProvider with keepAlive: true
//    - Singleton pattern for HTTP client
//    - Can be overridden to point to different servers
//
// 2. Created weatherRepositoryProvider that injects WeatherApiClient
//    - Abstract WeatherRepository interface for mocking
//    - Gets client via ref.watch()
//
// 3. WeatherViewModel now gets repository via ref.read()
//    - No more hard-coded dependencies
//    - Fully testable in isolation
//
// 4. Each layer can be tested independently
//    - Override API client for integration tests
//    - Override repository for unit tests
//    - Override ViewModel for widget tests