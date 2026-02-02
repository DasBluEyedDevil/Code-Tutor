from dataclasses import dataclass, field
from typing import List, Dict, Any, Optional
from enum import Enum

class Severity(Enum):
    ERROR = "ERROR"      # Must fix before deployment
    WARNING = "WARNING"  # Should fix
    INFO = "INFO"        # Recommendation

@dataclass
class SettingsIssue:
    setting: str
    severity: Severity
    message: str
    fix: Optional[str] = None

@dataclass
class ValidationResult:
    is_production_ready: bool
    issues: List[SettingsIssue] = field(default_factory=list)
    
    def print_report(self):
        print("\n=== Django Settings Validation ===")
        print(f"Production Ready: {'YES' if self.is_production_ready else 'NO'}")
        
        for severity in Severity:
            relevant = [i for i in self.issues if i.severity == severity]
            if relevant:
                print(f"\n{severity.value}S ({len(relevant)}):")
                for issue in relevant:
                    print(f"  [{issue.setting}] {issue.message}")
                    if issue.fix:
                        print(f"    Fix: {issue.fix}")

class SettingsValidator:
    def __init__(self, settings: Dict[str, Any]):
        self.settings = settings
        self.issues: List[SettingsIssue] = []
    
    def validate(self) -> ValidationResult:
        """
        Check these settings:
        1. DEBUG should be False for production
        2. SECRET_KEY should not contain 'dev' or 'change'
        3. ALLOWED_HOSTS should not be empty or ['*']
        4. Database should not be SQLite for production
        5. INSTALLED_APPS should exist and have items
        """
        self.issues = []
        # TODO: Implement validation checks
        # Add issues to self.issues list
        
        has_errors = any(i.severity == Severity.ERROR for i in self.issues)
        return ValidationResult(
            is_production_ready=not has_errors,
            issues=self.issues
        )

# Test with problematic settings
test_settings = {
    'DEBUG': True,
    'SECRET_KEY': 'dev-key-change-in-production',
    'ALLOWED_HOSTS': [],
    'DATABASES': {
        'default': {
            'ENGINE': 'django.db.backends.sqlite3',
            'NAME': 'db.sqlite3'
        }
    },
    'INSTALLED_APPS': [
        'django.contrib.admin',
        'transactions',
    ]
}

validator = SettingsValidator(test_settings)
result = validator.validate()
result.print_report()