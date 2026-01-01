from typing import Protocol, runtime_checkable

# TODO: Create Notifier protocol with send(message: str) -> bool

# TODO: Create EmailNotifier that implements Notifier

# TODO: Create SMSNotifier that implements Notifier

# TODO: Create PushNotifier that implements Notifier

# TODO: Create AlertService class that:
#   - Takes a list of Notifier in __init__
#   - Has check_budget(spent: float, limit: float) method
#   - Sends alert to all notifiers if spent > limit

# Test your implementation
email = EmailNotifier("user@example.com")
sms = SMSNotifier("+1234567890")
push = PushNotifier("device-token-123")

alert_service = AlertService([email, sms, push])
alert_service.check_budget(spent=550, limit=500)