from datetime import datetime

def parse_timestamp(timestamp_str):
    """Parse timestamp string to datetime object.
    
    Args:
        timestamp_str: String like '2024-01-15 10:00:00'
        
    Returns:
        datetime object or None if parsing fails
    """
    try:
        return datetime.strptime(timestamp_str, '%Y-%m-%d %H:%M:%S')
    except ValueError:
        return None

def group_by_hour(entries):
    """Group log entries by hour.
    
    Returns:
        dict: {hour: [entries]}
    """
    # TODO: Parse timestamps
    # TODO: Extract hour from datetime
    # TODO: Group entries by hour
    # TODO: Return grouped data
    pass

def find_peak_error_hour(entries):
    """Find hour with most errors.
    
    Returns:
        tuple: (hour, error_count)
    """
    # TODO: Filter errors only
    # TODO: Group by hour
    # TODO: Count errors per hour
    # TODO: Return hour with max errors
    pass