// Solution: Sealed Classes for Weather States

sealed class Weather {}

class Sunny extends Weather {
  final int temperature;
  Sunny(this.temperature);
}

class Rainy extends Weather {
  final String intensity;  // 'light', 'moderate', 'heavy'
  Rainy(this.intensity);
}

class Snowy extends Weather {
  final int inches;
  Snowy(this.inches);
}

String getAdvice(Weather weather) {
  return switch (weather) {
    Sunny(temperature: var temp) when temp > 90 => 
      'Very hot ($temp F)! Stay hydrated and seek shade.',
    Sunny(temperature: var temp) when temp > 70 => 
      'Nice day ($temp F)! Perfect for outdoor activities.',
    Sunny(temperature: var temp) => 
      'Cool but sunny ($temp F). Bring a light jacket.',
    Rainy(intensity: 'heavy') => 
      'Heavy rain! Stay indoors or bring an umbrella.',
    Rainy(intensity: 'light') => 
      'Light rain. A jacket should be enough.',
    Rainy(intensity: var i) => 
      '$i rain. Consider an umbrella.',
    Snowy(inches: var snowIn) when snowIn > 6 =>
      'Heavy snow ($snowIn inches)! Roads may be dangerous.',
    Snowy(inches: var snowIn) =>
      'Light snow ($snowIn inches). Drive carefully.',
  };
}

void main() {
  var conditions = [
    Sunny(85),
    Sunny(95),
    Rainy('heavy'),
    Rainy('light'),
    Snowy(6),
    Snowy(12),
  ];
  
  for (var weather in conditions) {
    print(getAdvice(weather));
  }
}