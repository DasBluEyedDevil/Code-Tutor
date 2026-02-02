// File: lib/features/weather/weather_feature.dart
// STARTER: Hard-coded dependencies - Refactor to use DI!

import 'dart:convert';
import 'package:http/http.dart' as http;
import 'package:flutter_riverpod/flutter_riverpod.dart';

// Weather Model
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

// API Client - handles HTTP communication
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

// Repository - handles data operations
class WeatherRepository {
  final WeatherApiClient _client;

  WeatherRepository(this._client);

  Future<Weather> getWeather(String city) async {
    final data = await _client.get('/weather?city=$city');
    return Weather.fromJson(data);
  }
}

// PROBLEM: This ViewModel creates its own dependencies!
// This makes it impossible to test or swap implementations.
class WeatherNotifier extends AsyncNotifier<Weather?> {
  // BAD: Creating dependencies directly!
  final _client = WeatherApiClient();
  late final _repository = WeatherRepository(_client);

  @override
  Future<Weather?> build() async {
    return null;  // No city selected initially
  }

  Future<void> loadWeather(String city) async {
    state = const AsyncLoading();
    state = await AsyncValue.guard(() => _repository.getWeather(city));
  }

  void clear() {
    state = const AsyncData(null);
  }
}

// BAD: Provider with hard-coded dependencies
final weatherViewModelProvider =
    AsyncNotifierProvider<WeatherNotifier, Weather?>(() {
  return WeatherNotifier();
});

// TODO: Refactor to use proper Dependency Injection!
//
// 1. Create: weatherApiClientProvider (keepAlive)
//    - Should return WeatherApiClient
//
// 2. Create: weatherRepositoryProvider
//    - Should get WeatherApiClient via ref.watch(weatherApiClientProvider)
//    - Should return WeatherRepository
//
// 3. Refactor: WeatherNotifier
//    - Remove the hard-coded _client and _repository
//    - Get repository via ref.watch(weatherRepositoryProvider)
//    - Use @riverpod annotation and code generation pattern
//
// The dependency chain should be:
// WeatherViewModel -> WeatherRepository -> WeatherApiClient