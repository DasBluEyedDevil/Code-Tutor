// Create a notification service that works on both platforms

// TODO: Define the interface in commonMain
interface NotificationService {
    fun showNotification(title: String, body: String)
    fun scheduleNotification(title: String, body: String, delayMs: Long)
    fun cancelAllNotifications()
}

// TODO: Create Android implementation (use NotificationManager)
// class AndroidNotificationService(...) : NotificationService

// TODO: Create iOS implementation (use UNUserNotificationCenter)
// class IOSNotificationService : NotificationService

// TODO: Register in platform modules