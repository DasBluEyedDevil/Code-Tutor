from typing import Protocol, runtime_checkable

@runtime_checkable
class Notifier(Protocol):
    """Protocol for notification services."""
    
    def send(self, message: str) -> bool:
        """Send a notification message."""
        ...

class EmailNotifier:
    """Email notification implementation."""
    
    def __init__(self, email: str):
        self.email = email
    
    def send(self, message: str) -> bool:
        print(f"[EMAIL to {self.email}] {message}")
        return True

class SMSNotifier:
    """SMS notification implementation."""
    
    def __init__(self, phone: str):
        self.phone = phone
    
    def send(self, message: str) -> bool:
        print(f"[SMS to {self.phone}] {message}")
        return True

class PushNotifier:
    """Push notification implementation."""
    
    def __init__(self, device_token: str):
        self.device_token = device_token[:8] + "..."  # Truncate for display
    
    def send(self, message: str) -> bool:
        print(f"[PUSH to {self.device_token}] {message}")
        return True

class AlertService:
    """Service that sends alerts via multiple notifiers."""
    
    def __init__(self, notifiers: list[Notifier]):
        self.notifiers = notifiers
    
    def send_alert(self, message: str) -> int:
        """Send alert to all notifiers. Returns count of successful sends."""
        successful = 0
        for notifier in self.notifiers:
            if notifier.send(message):
                successful += 1
        return successful
    
    def check_budget(self, spent: float, limit: float) -> None:
        """Check if budget is exceeded and send alerts."""
        if spent > limit:
            overage = spent - limit
            pct = (spent / limit) * 100
            message = f"BUDGET ALERT: Spent ${spent:.2f} ({pct:.1f}% of ${limit:.2f} limit). Over by ${overage:.2f}!"
            
            print(f"\nBudget exceeded! Sending alerts to {len(self.notifiers)} channels...")
            count = self.send_alert(message)
            print(f"Successfully sent {count} alerts.")
        else:
            remaining = limit - spent
            print(f"Budget OK: ${spent:.2f} spent, ${remaining:.2f} remaining")

# Test the implementation
print("=== Notification System Demo ===")
print()

# Verify Protocol implementation
print("Protocol checks:")
print(f"  EmailNotifier implements Notifier: {isinstance(EmailNotifier('test@test.com'), Notifier)}")
print(f"  SMSNotifier implements Notifier: {isinstance(SMSNotifier('+1234567890'), Notifier)}")
print(f"  PushNotifier implements Notifier: {isinstance(PushNotifier('token'), Notifier)}")
print()

# Create notifiers
email = EmailNotifier("user@example.com")
sms = SMSNotifier("+1234567890")
push = PushNotifier("device-token-123")

# Create alert service with all notifiers
alert_service = AlertService([email, sms, push])

# Test budget checks
print("=== Budget Checks ===")
alert_service.check_budget(spent=350, limit=500)  # Under budget
print()
alert_service.check_budget(spent=550, limit=500)  # Over budget!