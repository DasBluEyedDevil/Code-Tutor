from datetime import datetime, timedelta
from enum import Enum
from typing import Optional, Dict, Any
import json

class SecurityEventType(Enum):
    LOGIN_SUCCESS = "LOGIN_SUCCESS"
    LOGIN_FAILURE = "LOGIN_FAILURE"
    ACCESS_GRANTED = "ACCESS_GRANTED"
    ACCESS_DENIED = "ACCESS_DENIED"
    DATA_VIEWED = "DATA_VIEWED"
    DATA_MODIFIED = "DATA_MODIFIED"
    SUSPICIOUS_ACTIVITY = "SUSPICIOUS_ACTIVITY"

class SecurityAuditLogger:
    def __init__(self):
        self.logs = []
    
    def log_event(self, event_type: SecurityEventType, 
                  user_id: Optional[int],
                  ip_address: str,
                  resource: str,
                  details: Optional[Dict[str, Any]] = None,
                  success: bool = True) -> Dict:
        """Log a security event with full context"""
        log_entry = {
            "timestamp": datetime.now().isoformat(),
            "event_type": event_type.value,
            "user_id": user_id,
            "ip_address": ip_address,
            "resource": resource,
            "success": success,
            "details": details or {}
        }
        
        self.logs.append(log_entry)
        return log_entry
    
    def log_login_attempt(self, email: str, ip_address: str, 
                          success: bool, user_id: Optional[int] = None) -> Dict:
        """Log a login attempt"""
        event_type = SecurityEventType.LOGIN_SUCCESS if success else SecurityEventType.LOGIN_FAILURE
        return self.log_event(
            event_type=event_type,
            user_id=user_id,
            ip_address=ip_address,
            resource="auth/login",
            details={"email": email},
            success=success
        )
    
    def log_data_access(self, user_id: int, ip_address: str,
                        resource: str, action: str,
                        record_ids: list) -> Dict:
        """Log data access event"""
        event_type = SecurityEventType.DATA_VIEWED if action == 'view' else SecurityEventType.DATA_MODIFIED
        return self.log_event(
            event_type=event_type,
            user_id=user_id,
            ip_address=ip_address,
            resource=resource,
            details={"action": action, "record_ids": record_ids},
            success=True
        )
    
    def get_suspicious_activity(self, ip_address: str, 
                                 minutes: int = 15) -> list:
        """Find failed login attempts from an IP in recent minutes"""
        cutoff = datetime.now() - timedelta(minutes=minutes)
        return [
            log for log in self.logs
            if log["event_type"] == SecurityEventType.LOGIN_FAILURE.value
            and log["ip_address"] == ip_address
            and datetime.fromisoformat(log["timestamp"]) > cutoff
        ]

# Test the security logger
logger = SecurityAuditLogger()

# Log some events
logger.log_login_attempt("alice@example.com", "192.168.1.100", True, user_id=1)
logger.log_login_attempt("hacker@evil.com", "10.0.0.50", False)
logger.log_login_attempt("hacker@evil.com", "10.0.0.50", False)
logger.log_data_access(1, "192.168.1.100", "transactions", "view", [101, 102, 103])
logger.log_data_access(1, "192.168.1.100", "accounts", "modify", [5])

# Check for suspicious activity
suspicious = logger.get_suspicious_activity("10.0.0.50")
print(f"Suspicious activity from 10.0.0.50: {len(suspicious)} failed attempts")

# Print all logs
print("\nAudit Log:")
for log in logger.logs:
    print(json.dumps(log, indent=2))