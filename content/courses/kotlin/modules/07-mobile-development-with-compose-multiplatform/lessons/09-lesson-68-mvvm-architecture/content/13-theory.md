---
type: "THEORY"
title: "Solution 2"
---



---



```kotlin
// API
interface WeatherApi {
    @GET("weather")
    suspend fun getWeather(@Query("city") city: String): WeatherResponse
}

@Serializable
data class WeatherResponse(
    val temperature: Double,
    val description: String,
    val city: String
)

// Entity
@Entity
data class WeatherEntity(
    @PrimaryKey val city: String,
    val temperature: Double,
    val description: String,
    val timestamp: Long = System.currentTimeMillis()
)

// Repository
class WeatherRepository(
    private val api: WeatherApi,
    private val dao: WeatherDao
) {
    suspend fun getWeather(city: String): Result<WeatherEntity> {
        return try {
            val response = api.getWeather(city)
            val entity = WeatherEntity(
                city = response.city,
                temperature = response.temperature,
                description = response.description
            )
            dao.insert(entity)
            Result.Success(entity)
        } catch (e: Exception) {
            val cached = dao.getWeather(city)
            if (cached != null) {
                Result.Success(cached)
            } else {
                Result.Error(e.message ?: "Unknown error")
            }
        }
    }
}

// ViewModel
class WeatherViewModel(
    private val repository: WeatherRepository
) : ViewModel() {

    private val _uiState = MutableStateFlow<UiState<WeatherEntity>>(UiState.Loading)
    val uiState: StateFlow<UiState<WeatherEntity>> = _uiState.asStateFlow()

    fun loadWeather(city: String) {
        viewModelScope.launch {
            _uiState.value = UiState.Loading

            when (val result = repository.getWeather(city)) {
                is Result.Success -> {
                    _uiState.value = UiState.Success(result.data)
                }
                is Result.Error -> {
                    _uiState.value = UiState.Error(result.message)
                }
            }
        }
    }
}
```
