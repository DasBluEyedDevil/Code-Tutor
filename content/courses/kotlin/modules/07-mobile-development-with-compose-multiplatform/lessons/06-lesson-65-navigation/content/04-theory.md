---
type: "THEORY"
title: "Basic Navigation"
---


### NavController

**NavController** manages navigation between screens:


### NavHost

**NavHost** defines navigation graph (screens and routes):


### Screen Composables


---



```kotlin
@Composable
fun HomeScreen(onNavigateToProfile: () -> Unit) {
    Column(modifier = Modifier.fillMaxSize()) {
        Text("Home Screen", style = MaterialTheme.typography.headlineLarge)

        Button(onClick = onNavigateToProfile) {
            Text("Go to Profile")
        }
    }
}

@Composable
fun ProfileScreen(onNavigateBack: () -> Unit) {
    Column(modifier = Modifier.fillMaxSize()) {
        Text("Profile Screen", style = MaterialTheme.typography.headlineLarge)

        Button(onClick = onNavigateBack) {
            Text("Back")
        }
    }
}
```
