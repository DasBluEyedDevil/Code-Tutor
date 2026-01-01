---
type: "THEORY"
title: "Material Design 3 Components"
---


### Cards


**Clickable cards**:


### Surface

Material surface with elevation and color:


### Divider

Visual separator:


### Chips


### TextField


### Checkbox, Switch, RadioButton


### Slider


---



```kotlin
@Composable
fun SliderExample() {
    var sliderValue by remember { mutableStateOf(50f) }

    Column {
        Text("Volume: ${sliderValue.toInt()}%")

        Slider(
            value = sliderValue,
            onValueChange = { sliderValue = it },
            valueRange = 0f..100f,
            steps = 10  // Creates 11 discrete values (0, 10, 20, ..., 100)
        )

        // Range slider
        var rangeValues by remember { mutableStateOf(20f..80f) }
        Text("Range: ${rangeValues.start.toInt()} - ${rangeValues.endInclusive.toInt()}")

        RangeSlider(
            value = rangeValues,
            onValueChange = { rangeValues = it },
            valueRange = 0f..100f
        )
    }
}
```
