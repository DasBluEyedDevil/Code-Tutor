from dataclasses import dataclass, field
from typing import List, Dict, Any, Optional
from enum import Enum

class Severity(Enum):
    ERROR = "ERROR"
    WARNING = "WARNING"
    INFO = "INFO"

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
        self.issues = []
        
        # Check DEBUG
        if self.settings.get('DEBUG', False):
            self.issues.append(SettingsIssue(
                setting='DEBUG',
                severity=Severity.ERROR,
                message='DEBUG is True - exposes sensitive information',
                fix='Set DEBUG=False and use environment variable'
            ))
        
        # Check SECRET_KEY
        secret = self.settings.get('SECRET_KEY', '')
        if 'dev' in secret.lower() or 'change' in secret.lower():
            self.issues.append(SettingsIssue(
                setting='SECRET_KEY',
                severity=Severity.ERROR,
                message='SECRET_KEY appears to be a placeholder',
                fix='Generate a secure key and store in environment variable'
            ))
        
        # Check ALLOWED_HOSTS
        hosts = self.settings.get('ALLOWED_HOSTS', [])
        if not hosts:
            self.issues.append(SettingsIssue(
                setting='ALLOWED_HOSTS',
                severity=Severity.ERROR,
                message='ALLOWED_HOSTS is empty - required in production',
                fix='Add your domain names to ALLOWED_HOSTS'
            ))
        elif '*' in hosts:
            self.issues.append(SettingsIssue(
                setting='ALLOWED_HOSTS',
                severity=Severity.WARNING,
                message='ALLOWED_HOSTS contains "*" - allows any host',
                fix='Specify exact domain names instead of wildcard'
            ))
        
        # Check Database
        db = self.settings.get('DATABASES', {}).get('default', {})
        engine = db.get('ENGINE', '')
        if 'sqlite' in engine.lower():
            self.issues.append(SettingsIssue(
                setting='DATABASES',
                severity=Severity.WARNING,
                message='Using SQLite - not recommended for production',
                fix='Use PostgreSQL or MySQL for production'
            ))
        
        # Check INSTALLED_APPS
        apps = self.settings.get('INSTALLED_APPS', [])
        if not apps:
            self.issues.append(SettingsIssue(
                setting='INSTALLED_APPS',
                severity=Severity.ERROR,
                message='INSTALLED_APPS is empty',
                fix='Add required Django apps and your custom apps'
            ))
        else:
            self.issues.append(SettingsIssue(
                setting='INSTALLED_APPS',
                severity=Severity.INFO,
                message=f'{len(apps)} apps configured',
                fix=None
            ))
        
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