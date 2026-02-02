def validate_config(config: dict):
    errors = []
    
    if "host" not in config:
        errors.append(ValueError("Missing 'host'"))
    elif not isinstance(config["host"], str):
        errors.append(TypeError("'host' must be a string"))
    
    if "port" not in config:
        errors.append(____)
    elif not isinstance(config["port"], int):
        errors.append(____)
    elif not (1 <= config["port"] <= 65535):
        errors.append(____)
    
    if errors:
        raise ____("Config validation failed", errors)
    
    return config
