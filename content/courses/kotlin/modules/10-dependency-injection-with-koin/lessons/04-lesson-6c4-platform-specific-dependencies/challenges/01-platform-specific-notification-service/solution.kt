// ========== commonMain/kotlin/platform/NotificationService.kt ==========

interface NotificationService {
    fun showNotification(title: String, body: String)
    fun scheduleNotification(title: String, body: String, delayMs: Long)
    fun cancelAllNotifications()
}

// ========== androidMain/kotlin/platform/NotificationService.android.kt ==========

class AndroidNotificationService(
    private val context: Context
) : NotificationService {
    
    private val notificationManager = context.getSystemService(
        Context.NOTIFICATION_SERVICE
    ) as NotificationManager
    
    private val channelId = "notes_channel"
    
    init {
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.O) {
            val channel = NotificationChannel(
                channelId,
                "Notes",
                NotificationManager.IMPORTANCE_DEFAULT
            )
            notificationManager.createNotificationChannel(channel)
        }
    }
    
    override fun showNotification(title: String, body: String) {
        val notification = NotificationCompat.Builder(context, channelId)
            .setSmallIcon(R.drawable.ic_notification)
            .setContentTitle(title)
            .setContentText(body)
            .setPriority(NotificationCompat.PRIORITY_DEFAULT)
            .build()
        
        notificationManager.notify(System.currentTimeMillis().toInt(), notification)
    }
    
    override fun scheduleNotification(title: String, body: String, delayMs: Long) {
        // Use WorkManager for scheduled notifications
        val data = workDataOf(
            "title" to title,
            "body" to body
        )
        
        val request = OneTimeWorkRequestBuilder<NotificationWorker>()
            .setInitialDelay(delayMs, TimeUnit.MILLISECONDS)
            .setInputData(data)
            .build()
        
        WorkManager.getInstance(context).enqueue(request)
    }
    
    override fun cancelAllNotifications() {
        notificationManager.cancelAll()
        WorkManager.getInstance(context).cancelAllWork()
    }
}

// ========== iosMain/kotlin/platform/NotificationService.ios.kt ==========

class IOSNotificationService : NotificationService {
    
    private val center = UNUserNotificationCenter.currentNotificationCenter()
    
    override fun showNotification(title: String, body: String) {
        val content = UNMutableNotificationContent().apply {
            setTitle(title)
            setBody(body)
            setSound(UNNotificationSound.defaultSound())
        }
        
        val trigger = UNTimeIntervalNotificationTrigger
            .triggerWithTimeInterval(0.1, repeats = false)
        
        val request = UNNotificationRequest.requestWithIdentifier(
            NSUUID().UUIDString,
            content = content,
            trigger = trigger
        )
        
        center.addNotificationRequest(request) { error ->
            error?.let { println("Notification error: $it") }
        }
    }
    
    override fun scheduleNotification(title: String, body: String, delayMs: Long) {
        val content = UNMutableNotificationContent().apply {
            setTitle(title)
            setBody(body)
            setSound(UNNotificationSound.defaultSound())
        }
        
        val trigger = UNTimeIntervalNotificationTrigger
            .triggerWithTimeInterval(delayMs / 1000.0, repeats = false)
        
        val request = UNNotificationRequest.requestWithIdentifier(
            NSUUID().UUIDString,
            content = content,
            trigger = trigger
        )
        
        center.addNotificationRequest(request, withCompletionHandler = null)
    }
    
    override fun cancelAllNotifications() {
        center.removeAllPendingNotificationRequests()
        center.removeAllDeliveredNotifications()
    }
}

// ========== Platform Modules ==========

// androidMain/kotlin/di/PlatformModule.kt
val platformModule = module {
    single<NotificationService> { AndroidNotificationService(get()) }
}

// iosMain/kotlin/di/PlatformModule.kt
val platformModule = module {
    single<NotificationService> { IOSNotificationService() }
}