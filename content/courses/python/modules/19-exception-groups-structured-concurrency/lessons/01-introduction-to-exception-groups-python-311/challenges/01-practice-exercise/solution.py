def validate_config(config: dict):
    errors = []
    
    if "host" not in config:
        errors.append(ValueError("Missing 'host'"))
    elif not isinstance(config["host"], str):
        errors.append(TypeError("'host' must be a string"))
    
    if "port" not in config:
        errors.append(ValueError("Missing 'port'"))
    elif not isinstance(config["port"], int):
        errors.append(TypeError("'port' must be an integer"))
    elif not (1 <= config["port"] <= 65535):
        errors.append(ValueError("'port' must be between 1 and 65535"))
    
    if errors:
        raise ExceptionGroup("Config validation failed", errors)
    
    return config
