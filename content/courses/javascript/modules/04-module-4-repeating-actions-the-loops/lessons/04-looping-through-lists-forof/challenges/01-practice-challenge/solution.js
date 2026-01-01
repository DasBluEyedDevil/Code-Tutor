let celsiusTemps = [0, 10, 20, 30, 40];

for (let celsius of celsiusTemps) {
  let fahrenheit = (celsius * 9/5) + 32;
  console.log(celsius + '°C is ' + fahrenheit + '°F');
}