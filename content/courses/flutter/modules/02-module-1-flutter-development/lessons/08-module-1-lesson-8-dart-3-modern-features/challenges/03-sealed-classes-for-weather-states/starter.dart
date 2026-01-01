// Sealed Classes Practice

// TODO: Define sealed class Weather and its subclasses

String getAdvice(Weather weather) {
  // TODO: Use exhaustive switch
  return '';
}

void main() {
  var conditions = [
    Sunny(85),
    Rainy('heavy'),
    Snowy(6),
  ];
  
  for (var weather in conditions) {
    print(getAdvice(weather));
  }
}