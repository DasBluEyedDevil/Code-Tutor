from dataclasses import dataclass, field
from typing import Optional, List, Dict, Any
import os
import re

@dataclass
class SecretValue:
    """Wrapper that hides sensitive values in string representation."""
    _value: str
    
    def get_secret_value(self) -> str:
        return self._value
    
    def __repr__(self) -> str:
        return "SecretValue('**********')"
    
    def __str__(self) -> str:
        return "**********"

class ConfigurationError(Exception):
    """Raised when configuration is invalid."""
    pass

@dataclass
class DatabaseConfig:
    url: str
    pool_size: int = 10
    pool_timeout: int = 30

@dataclass
class SecurityConfig:
    jwt_secret: SecretValue
    jwt_algorithm: str = "HS256"
    jwt_expiry_minutes: int = 15
    allowed_origins: List[str] = field(default_factory=lambda: ["http://localhost:3000"])

@dataclass
class AppConfig:
    name: str = "Finance Tracker"
    environment: str = "development"
    debug: bool = False

class ConfigManager:
    """
    Configuration manager that loads and validates settings.
    """
    
    VALID_ENVIRONMENTS = ["development", "staging", "production"]
    VALID_JWT_ALGORITHMS = ["HS256", "HS384", "HS512", "RS256", "RS384", "RS512"]
    
    def __init__(self):
        self.database: Optional[DatabaseConfig] = None
        self.security: Optional[SecurityConfig] = None
        self.app: Optional[AppConfig] = None
        self._loaded = False
    
    def load(self, env_overrides: Optional[Dict[str, str]] = None) -> 'ConfigManager':
        env = {**os.environ, **(env_overrides or {})}
        errors = []
        
        try:
            self.database = self._load_database_config(env)
        except ConfigurationError as e:
            errors.append(str(e))
        
        try:
            self.security = self._load_security_config(env)
        except ConfigurationError as e:
            errors.append(str(e))
        
        self.app = self._load_app_config(env)
        
        if errors:
            raise ConfigurationError(f"Configuration errors: {'; '.join(errors)}")
        
        self._loaded = True
        return self
    
    def _load_database_config(self, env: Dict[str, str]) -> DatabaseConfig:
        """Load and validate database configuration."""
        url = env.get("DATABASE_URL")
        if not url:
            raise ConfigurationError("DATABASE_URL is required")
        
        if not re.match(r'^(postgresql|postgres|mysql|sqlite)://', url):
            raise ConfigurationError("DATABASE_URL must be a valid database URL")
        
        pool_size = int(env.get("DATABASE_POOL_SIZE", "10"))
        pool_timeout = int(env.get("DATABASE_POOL_TIMEOUT", "30"))
        
        return DatabaseConfig(
            url=url,
            pool_size=pool_size,
            pool_timeout=pool_timeout
        )
    
    def _load_security_config(self, env: Dict[str, str]) -> SecurityConfig:
        """Load and validate security configuration."""
        jwt_secret = env.get("JWT_SECRET")
        if not jwt_secret:
            raise ConfigurationError("JWT_SECRET is required")
        if len(jwt_secret) < 32:
            raise ConfigurationError("JWT_SECRET must be at least 32 characters")
        
        jwt_algorithm = env.get("JWT_ALGORITHM", "HS256")
        if jwt_algorithm not in self.VALID_JWT_ALGORITHMS:
            raise ConfigurationError(f"JWT_ALGORITHM must be one of {self.VALID_JWT_ALGORITHMS}")
        
        jwt_expiry = int(env.get("JWT_EXPIRY_MINUTES", "15"))
        
        origins_str = env.get("ALLOWED_ORIGINS", "http://localhost:3000")
        allowed_origins = [origin.strip() for origin in origins_str.split(",")]
        
        return SecurityConfig(
            jwt_secret=SecretValue(jwt_secret),
            jwt_algorithm=jwt_algorithm,
            jwt_expiry_minutes=jwt_expiry,
            allowed_origins=allowed_origins
        )
    
    def _load_app_config(self, env: Dict[str, str]) -> AppConfig:
        """Load application configuration."""
        environment = env.get("ENVIRONMENT", "development")
        if environment not in self.VALID_ENVIRONMENTS:
            environment = "development"
        
        debug_str = env.get("DEBUG", "false").lower()
        debug = debug_str in ("true", "1", "yes")
        
        return AppConfig(
            name=env.get("APP_NAME", "Finance Tracker"),
            environment=environment,
            debug=debug
        )
    
    def require_loaded(self) -> None:
        if not self._loaded:
            raise ConfigurationError("Configuration not loaded. Call load() first.")
    
    def summary(self) -> str:
        self.require_loaded()
        return f"""
Configuration Summary:
  App: {self.app.name} ({self.app.environment})
  Debug: {self.app.debug}
  Database: {self._mask_url(self.database.url)}
  Pool Size: {self.database.pool_size}
  JWT Algorithm: {self.security.jwt_algorithm}
  JWT Expiry: {self.security.jwt_expiry_minutes} minutes
  Allowed Origins: {self.security.allowed_origins}
"""
    
    def _mask_url(self, url: str) -> str:
        return re.sub(r':([^:@]+)@', ':****@', url)


# Test the configuration manager
print("Configuration Manager Test")
print("=" * 50)

test_env = {
    "DATABASE_URL": "postgresql://admin:secretpass@localhost:5432/finance",
    "DATABASE_POOL_SIZE": "20",
    "JWT_SECRET": "this-is-a-very-secure-secret-key-for-jwt-minimum-32",
    "JWT_ALGORITHM": "HS256",
    "JWT_EXPIRY_MINUTES": "30",
    "ALLOWED_ORIGINS": "http://localhost:3000,https://app.example.com",
    "APP_NAME": "Finance Tracker Pro",
    "ENVIRONMENT": "staging",
    "DEBUG": "true"
}

config = ConfigManager()
try:
    config.load(test_env)
    print("Configuration loaded successfully!")
    print(config.summary())
    
    print(f"JWT Secret (repr): {config.security.jwt_secret}")
    print(f"JWT Secret (actual): {config.security.jwt_secret.get_secret_value()[:10]}...")
    
except ConfigurationError as e:
    print(f"Configuration Error: {e}")

print("\n--- Validation Tests ---")

print("\nTest 1: Missing DATABASE_URL")
try:
    ConfigManager().load({"JWT_SECRET": "a" * 32})
    print("  FAIL: Should have raised error")
except ConfigurationError as e:
    print(f"  PASS: {e}")

print("\nTest 2: JWT secret too short")
try:
    ConfigManager().load({
        "DATABASE_URL": "postgresql://user:pass@localhost/db",
        "JWT_SECRET": "short"
    })
    print("  FAIL: Should have raised error")
except ConfigurationError as e:
    print(f"  PASS: {e}")

print("\nConfiguration manager working correctly!")