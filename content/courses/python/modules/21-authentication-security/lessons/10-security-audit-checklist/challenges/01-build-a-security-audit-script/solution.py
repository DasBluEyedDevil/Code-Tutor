from dataclasses import dataclass
from typing import List, Dict, Any, Optional
from enum import Enum

class AuditStatus(Enum):
    PASS = "PASS"
    FAIL = "FAIL"
    WARN = "WARN"

@dataclass
class AuditCheck:
    name: str
    category: str
    status: AuditStatus
    message: str
    recommendation: Optional[str] = None

class SecurityAuditor:
    """
    Security auditor that checks application configuration
    for common security issues.
    """
    
    def __init__(self, config: Dict[str, Any]):
        self.config = config
        self.checks: List[AuditCheck] = []
    
    def audit_password_policy(self) -> List[AuditCheck]:
        checks = []
        policy = self.config.get("password_policy", {})
        
        # Check minimum password length
        min_length = policy.get("min_length", 0)
        if min_length >= 12:
            checks.append(AuditCheck(
                name="Password Min Length",
                category="Password Policy",
                status=AuditStatus.PASS,
                message=f"Minimum length is {min_length} characters"
            ))
        else:
            checks.append(AuditCheck(
                name="Password Min Length",
                category="Password Policy",
                status=AuditStatus.FAIL,
                message=f"Minimum length is only {min_length} characters",
                recommendation="Set minimum password length to at least 12 characters"
            ))
        
        # Check complexity requirements
        complexity_checks = [
            ("require_uppercase", "uppercase letters"),
            ("require_lowercase", "lowercase letters"),
            ("require_digits", "digits"),
            ("require_special", "special characters")
        ]
        
        for setting, description in complexity_checks:
            if policy.get(setting, False):
                checks.append(AuditCheck(
                    name=f"Require {description}",
                    category="Password Policy",
                    status=AuditStatus.PASS,
                    message=f"Passwords require {description}"
                ))
            else:
                checks.append(AuditCheck(
                    name=f"Require {description}",
                    category="Password Policy",
                    status=AuditStatus.FAIL,
                    message=f"Passwords don't require {description}",
                    recommendation=f"Enable requirement for {description}"
                ))
        
        # Check lockout settings
        max_attempts = policy.get("max_attempts", 999)
        if max_attempts <= 5:
            checks.append(AuditCheck(
                name="Max Login Attempts",
                category="Password Policy",
                status=AuditStatus.PASS,
                message=f"Account locks after {max_attempts} failed attempts"
            ))
        else:
            checks.append(AuditCheck(
                name="Max Login Attempts",
                category="Password Policy",
                status=AuditStatus.FAIL,
                message=f"Account locks after {max_attempts} attempts (too many)",
                recommendation="Set max attempts to 5 or fewer"
            ))
        
        lockout_minutes = policy.get("lockout_minutes", 0)
        if lockout_minutes >= 15:
            checks.append(AuditCheck(
                name="Lockout Duration",
                category="Password Policy",
                status=AuditStatus.PASS,
                message=f"Lockout duration is {lockout_minutes} minutes"
            ))
        else:
            checks.append(AuditCheck(
                name="Lockout Duration",
                category="Password Policy",
                status=AuditStatus.FAIL,
                message=f"Lockout duration is only {lockout_minutes} minutes",
                recommendation="Set lockout duration to at least 15 minutes"
            ))
        
        return checks
    
    def audit_jwt_config(self) -> List[AuditCheck]:
        checks = []
        jwt = self.config.get("jwt", {})
        
        # Check algorithm
        algorithm = jwt.get("algorithm", "none")
        if algorithm.lower() == "none":
            checks.append(AuditCheck(
                name="JWT Algorithm",
                category="JWT Configuration",
                status=AuditStatus.FAIL,
                message="JWT algorithm is 'none' - CRITICAL vulnerability",
                recommendation="Use HS256 with strong key or RS256"
            ))
        else:
            key_length = jwt.get("secret_key_length", 0)
            if algorithm.startswith("HS") and key_length < 32:
                checks.append(AuditCheck(
                    name="JWT Algorithm",
                    category="JWT Configuration",
                    status=AuditStatus.WARN,
                    message=f"Using {algorithm} with short key ({key_length} chars)",
                    recommendation="Use a secret key of at least 32 characters"
                ))
            else:
                checks.append(AuditCheck(
                    name="JWT Algorithm",
                    category="JWT Configuration",
                    status=AuditStatus.PASS,
                    message=f"Using {algorithm} with adequate key length"
                ))
        
        # Check access token expiry
        access_expiry = jwt.get("access_token_expiry_minutes", 60)
        if access_expiry <= 30:
            checks.append(AuditCheck(
                name="Access Token Expiry",
                category="JWT Configuration",
                status=AuditStatus.PASS,
                message=f"Access tokens expire in {access_expiry} minutes"
            ))
        else:
            checks.append(AuditCheck(
                name="Access Token Expiry",
                category="JWT Configuration",
                status=AuditStatus.FAIL,
                message=f"Access tokens expire in {access_expiry} minutes (too long)",
                recommendation="Set access token expiry to 30 minutes or less"
            ))
        
        # Check refresh token expiry
        refresh_expiry = jwt.get("refresh_token_expiry_days", 30)
        if refresh_expiry <= 7:
            checks.append(AuditCheck(
                name="Refresh Token Expiry",
                category="JWT Configuration",
                status=AuditStatus.PASS,
                message=f"Refresh tokens expire in {refresh_expiry} days"
            ))
        else:
            checks.append(AuditCheck(
                name="Refresh Token Expiry",
                category="JWT Configuration",
                status=AuditStatus.WARN,
                message=f"Refresh tokens expire in {refresh_expiry} days (consider shorter)",
                recommendation="Consider setting refresh token expiry to 7 days or less"
            ))
        
        return checks
    
    def audit_security_headers(self) -> List[AuditCheck]:
        checks = []
        headers = self.config.get("security_headers", {})
        
        required = [
            ("strict_transport_security", "Strict-Transport-Security", "HSTS protects against downgrade attacks"),
            ("x_content_type_options", "X-Content-Type-Options", "Prevents MIME-type sniffing"),
            ("x_frame_options", "X-Frame-Options", "Prevents clickjacking attacks"),
            ("content_security_policy", "Content-Security-Policy", "Prevents XSS and injection attacks")
        ]
        
        for key, header_name, description in required:
            if headers.get(key, False):
                checks.append(AuditCheck(
                    name=header_name,
                    category="Security Headers",
                    status=AuditStatus.PASS,
                    message=f"{header_name} is configured"
                ))
            else:
                checks.append(AuditCheck(
                    name=header_name,
                    category="Security Headers",
                    status=AuditStatus.FAIL,
                    message=f"{header_name} is not configured",
                    recommendation=f"Enable {header_name}: {description}"
                ))
        
        return checks
    
    def audit_rate_limiting(self) -> List[AuditCheck]:
        checks = []
        rate_limit = self.config.get("rate_limiting", {})
        
        # Check if enabled
        if rate_limit.get("enabled", False):
            checks.append(AuditCheck(
                name="Rate Limiting Enabled",
                category="Rate Limiting",
                status=AuditStatus.PASS,
                message="Rate limiting is enabled"
            ))
        else:
            checks.append(AuditCheck(
                name="Rate Limiting Enabled",
                category="Rate Limiting",
                status=AuditStatus.FAIL,
                message="Rate limiting is disabled",
                recommendation="Enable rate limiting to prevent abuse"
            ))
            return checks  # Skip other checks if disabled
        
        # Check login limits
        login_limit = rate_limit.get("login_requests_per_minute", 999)
        if login_limit <= 10:
            checks.append(AuditCheck(
                name="Login Rate Limit",
                category="Rate Limiting",
                status=AuditStatus.PASS,
                message=f"Login limited to {login_limit} requests/minute"
            ))
        else:
            checks.append(AuditCheck(
                name="Login Rate Limit",
                category="Rate Limiting",
                status=AuditStatus.FAIL,
                message=f"Login allows {login_limit} requests/minute (too high)",
                recommendation="Limit login attempts to 10 or fewer per minute"
            ))
        
        # Check API limits
        api_limit = rate_limit.get("api_requests_per_minute", 0)
        if api_limit > 0 and api_limit <= 200:
            checks.append(AuditCheck(
                name="API Rate Limit",
                category="Rate Limiting",
                status=AuditStatus.PASS,
                message=f"API limited to {api_limit} requests/minute"
            ))
        elif api_limit > 200:
            checks.append(AuditCheck(
                name="API Rate Limit",
                category="Rate Limiting",
                status=AuditStatus.WARN,
                message=f"API allows {api_limit} requests/minute (consider lower)",
                recommendation="Consider limiting API requests to 100-200 per minute"
            ))
        
        return checks
    
    def run_audit(self) -> Dict[str, Any]:
        self.checks = []
        
        self.checks.extend(self.audit_password_policy())
        self.checks.extend(self.audit_jwt_config())
        self.checks.extend(self.audit_security_headers())
        self.checks.extend(self.audit_rate_limiting())
        
        passed = sum(1 for c in self.checks if c.status == AuditStatus.PASS)
        failed = sum(1 for c in self.checks if c.status == AuditStatus.FAIL)
        warnings = sum(1 for c in self.checks if c.status == AuditStatus.WARN)
        
        return {
            "total_checks": len(self.checks),
            "passed": passed,
            "failed": failed,
            "warnings": warnings,
            "overall_status": "FAIL" if failed > 0 else ("WARN" if warnings > 0 else "PASS"),
            "checks": self.checks
        }
    
    def print_report(self):
        results = self.run_audit()
        
        print("\n" + "=" * 60)
        print("SECURITY AUDIT REPORT")
        print("=" * 60)
        
        print(f"\nTotal Checks: {results['total_checks']}")
        print(f"  Passed: {results['passed']}")
        print(f"  Failed: {results['failed']}")
        print(f"  Warnings: {results['warnings']}")
        print(f"\nOverall Status: {results['overall_status']}")
        
        categories = {}
        for check in self.checks:
            if check.category not in categories:
                categories[check.category] = []
            categories[check.category].append(check)
        
        for category, checks in categories.items():
            print(f"\n--- {category} ---")
            for check in checks:
                symbol = {"PASS": "[+]", "FAIL": "[-]", "WARN": "[!]"}[check.status.value]
                print(f"{symbol} {check.name}: {check.message}")
                if check.recommendation and check.status != AuditStatus.PASS:
                    print(f"    Fix: {check.recommendation}")
        
        print("\n" + "=" * 60)
        return results["overall_status"] == "PASS"


test_config = {
    "password_policy": {
        "min_length": 8,
        "require_uppercase": True,
        "require_lowercase": True,
        "require_digits": True,
        "require_special": False,
        "max_attempts": 10,
        "lockout_minutes": 5
    },
    "jwt": {
        "algorithm": "HS256",
        "secret_key_length": 64,
        "access_token_expiry_minutes": 60,
        "refresh_token_expiry_days": 30
    },
    "security_headers": {
        "strict_transport_security": True,
        "x_content_type_options": True,
    },
    "rate_limiting": {
        "enabled": True,
        "login_requests_per_minute": 20,
        "api_requests_per_minute": 100
    }
}

print("Finance Tracker Security Audit")
auditor = SecurityAuditor(test_config)
passed = auditor.print_report()

if not passed:
    print("\nReview failed checks and update configuration before deployment.")
else:
    print("\nAll security checks passed!")