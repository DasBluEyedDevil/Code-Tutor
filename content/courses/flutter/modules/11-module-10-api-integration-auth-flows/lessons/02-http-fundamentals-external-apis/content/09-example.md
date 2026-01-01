---
type: "EXAMPLE"
title: "Real-World Example: Weather API Integration"
---


Let us build a complete weather service using the OpenWeatherMap API, demonstrating proper HTTP patterns:



```dart
// lib/features/weather/data/models/weather.dart

/// Weather data from OpenWeatherMap API.
class Weather {
  final String cityName;
  final double temperature;
  final double feelsLike;
  final int humidity;
  final String description;
  final String icon;
  final double windSpeed;
  final DateTime sunrise;
  final DateTime sunset;

  Weather({
    required this.cityName,
    required this.temperature,
    required this.feelsLike,
    required this.humidity,
    required this.description,
    required this.icon,
    required this.windSpeed,
    required this.sunrise,
    required this.sunset,
  });

  /// Parse the complex nested JSON from OpenWeatherMap.
  /// 
  /// Example response:
  /// ```json
  /// {
  ///   "name": "London",
  ///   "main": {
  ///     "temp": 15.5,
  ///     "feels_like": 14.2,
  ///     "humidity": 72
  ///   },
  ///   "weather": [{
  ///     "description": "light rain",
  ///     "icon": "10d"
  ///   }],
  ///   "wind": { "speed": 5.2 },
  ///   "sys": {
  ///     "sunrise": 1699426800,
  ///     "sunset": 1699462800
  ///   }
  /// }
  /// ```
  factory Weather.fromJson(Map<String, dynamic> json) {
    final main = json['main'] as Map<String, dynamic>;
    final weatherList = json['weather'] as List<dynamic>;
    final weather = weatherList.first as Map<String, dynamic>;
    final wind = json['wind'] as Map<String, dynamic>;
    final sys = json['sys'] as Map<String, dynamic>;

    return Weather(
      cityName: json['name'] as String,
      temperature: (main['temp'] as num).toDouble(),
      feelsLike: (main['feels_like'] as num).toDouble(),
      humidity: main['humidity'] as int,
      description: weather['description'] as String,
      icon: weather['icon'] as String,
      windSpeed: (wind['speed'] as num).toDouble(),
      sunrise: DateTime.fromMillisecondsSinceEpoch(
        (sys['sunrise'] as int) * 1000,
      ),
      sunset: DateTime.fromMillisecondsSinceEpoch(
        (sys['sunset'] as int) * 1000,
      ),
    );
  }

  /// Get the full URL for the weather icon.
  String get iconUrl => 'https://openweathermap.org/img/wn/$icon@2x.png';

  @override
  String toString() {
    return 'Weather($cityName: ${temperature.round()}C, $description)';
  }
}

// lib/features/weather/data/weather_api_service.dart
import 'package:dio/dio.dart';
import 'models/weather.dart';

/// Service for fetching weather data from OpenWeatherMap API.
/// 
/// Requires an API key from https://openweathermap.org/api
/// Free tier allows 1000 calls/day.
class WeatherApiService {
  final Dio _dio;
  
  WeatherApiService({required String apiKey})
      : _dio = Dio(BaseOptions(
          baseUrl: 'https://api.openweathermap.org/data/2.5',
          queryParameters: {
            'appid': apiKey,
            'units': 'metric', // Use Celsius
          },
          connectTimeout: const Duration(seconds: 10),
          receiveTimeout: const Duration(seconds: 10),
        ));

  /// Get current weather by city name.
  /// 
  /// Example: getWeatherByCity('London') or getWeatherByCity('London,UK')
  Future<Weather> getWeatherByCity(String cityName) async {
    try {
      final response = await _dio.get(
        '/weather',
        queryParameters: {'q': cityName},
      );
      return Weather.fromJson(response.data);
    } on DioException catch (e) {
      throw _handleError(e);
    }
  }

  /// Get current weather by coordinates.
  /// 
  /// Useful when you have the user's GPS location.
  Future<Weather> getWeatherByCoordinates({
    required double latitude,
    required double longitude,
  }) async {
    try {
      final response = await _dio.get(
        '/weather',
        queryParameters: {
          'lat': latitude,
          'lon': longitude,
        },
      );
      return Weather.fromJson(response.data);
    } on DioException catch (e) {
      throw _handleError(e);
    }
  }

  /// Get weather for multiple cities at once.
  /// 
  /// More efficient than multiple single-city calls.
  Future<List<Weather>> getWeatherForCities(List<String> cities) async {
    // OpenWeatherMap does not have a batch endpoint on free tier,
    // so we make parallel requests.
    final futures = cities.map((city) => getWeatherByCity(city));
    return await Future.wait(futures);
  }

  /// Convert DioException to user-friendly error.
  WeatherException _handleError(DioException e) {
    if (e.type == DioExceptionType.connectionError ||
        e.type == DioExceptionType.connectionTimeout) {
      return WeatherException(
        'Unable to connect to weather service. Please check your internet.',
        type: WeatherErrorType.network,
      );
    }

    final statusCode = e.response?.statusCode;
    final responseData = e.response?.data;

    if (statusCode == 401) {
      return WeatherException(
        'Invalid API key. Please check configuration.',
        type: WeatherErrorType.invalidApiKey,
      );
    }

    if (statusCode == 404) {
      // OpenWeatherMap returns 404 for unknown cities
      final message = responseData is Map
          ? responseData['message'] ?? 'City not found'
          : 'City not found';
      return WeatherException(
        message,
        type: WeatherErrorType.cityNotFound,
      );
    }

    if (statusCode == 429) {
      return WeatherException(
        'API rate limit exceeded. Please try again later.',
        type: WeatherErrorType.rateLimited,
      );
    }

    return WeatherException(
      'Failed to fetch weather data. Please try again.',
      type: WeatherErrorType.unknown,
    );
  }
}

enum WeatherErrorType {
  network,
  cityNotFound,
  invalidApiKey,
  rateLimited,
  unknown,
}

class WeatherException implements Exception {
  final String message;
  final WeatherErrorType type;

  WeatherException(this.message, {required this.type});

  @override
  String toString() => message;
}

// lib/features/weather/presentation/weather_screen.dart
import 'package:flutter/material.dart';
import '../data/weather_api_service.dart';
import '../data/models/weather.dart';

class WeatherScreen extends StatefulWidget {
  const WeatherScreen({super.key});

  @override
  State<WeatherScreen> createState() => _WeatherScreenState();
}

class _WeatherScreenState extends State<WeatherScreen> {
  // Replace with your actual API key
  final _weatherService = WeatherApiService(
    apiKey: const String.fromEnvironment('OPENWEATHER_API_KEY'),
  );
  
  final _cityController = TextEditingController();
  Weather? _weather;
  bool _isLoading = false;
  String? _error;

  Future<void> _fetchWeather() async {
    final city = _cityController.text.trim();
    if (city.isEmpty) return;

    setState(() {
      _isLoading = true;
      _error = null;
    });

    try {
      final weather = await _weatherService.getWeatherByCity(city);
      setState(() {
        _weather = weather;
        _isLoading = false;
      });
    } on WeatherException catch (e) {
      setState(() {
        _error = e.message;
        _isLoading = false;
      });
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Weather')),
      body: Padding(
        padding: const EdgeInsets.all(16),
        child: Column(
          children: [
            TextField(
              controller: _cityController,
              decoration: InputDecoration(
                labelText: 'City name',
                hintText: 'Enter city name (e.g., London)',
                suffixIcon: IconButton(
                  icon: const Icon(Icons.search),
                  onPressed: _fetchWeather,
                ),
              ),
              onSubmitted: (_) => _fetchWeather(),
            ),
            const SizedBox(height: 24),
            if (_isLoading)
              const CircularProgressIndicator()
            else if (_error != null)
              Text(
                _error!,
                style: TextStyle(color: Theme.of(context).colorScheme.error),
              )
            else if (_weather != null)
              _WeatherCard(weather: _weather!),
          ],
        ),
      ),
    );
  }
}

class _WeatherCard extends StatelessWidget {
  final Weather weather;

  const _WeatherCard({required this.weather});

  @override
  Widget build(BuildContext context) {
    return Card(
      child: Padding(
        padding: const EdgeInsets.all(16),
        child: Column(
          children: [
            Text(
              weather.cityName,
              style: Theme.of(context).textTheme.headlineMedium,
            ),
            Image.network(
              weather.iconUrl,
              width: 100,
              height: 100,
            ),
            Text(
              '${weather.temperature.round()}C',
              style: Theme.of(context).textTheme.displayMedium,
            ),
            Text(
              weather.description,
              style: Theme.of(context).textTheme.titleMedium,
            ),
            const Divider(),
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceAround,
              children: [
                _InfoTile(
                  icon: Icons.thermostat,
                  label: 'Feels like',
                  value: '${weather.feelsLike.round()}C',
                ),
                _InfoTile(
                  icon: Icons.water_drop,
                  label: 'Humidity',
                  value: '${weather.humidity}%',
                ),
                _InfoTile(
                  icon: Icons.air,
                  label: 'Wind',
                  value: '${weather.windSpeed} m/s',
                ),
              ],
            ),
          ],
        ),
      ),
    );
  }
}

class _InfoTile extends StatelessWidget {
  final IconData icon;
  final String label;
  final String value;

  const _InfoTile({
    required this.icon,
    required this.label,
    required this.value,
  });

  @override
  Widget build(BuildContext context) {
    return Column(
      children: [
        Icon(icon, color: Theme.of(context).colorScheme.primary),
        Text(label, style: Theme.of(context).textTheme.bodySmall),
        Text(value, style: Theme.of(context).textTheme.titleMedium),
      ],
    );
  }
}
```
