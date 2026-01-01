def network_calls():
    raise ExceptionGroup("Network failures", [
        ConnectionError("Server 1 unreachable"),
        TimeoutError("Server 2 timed out"),
        ConnectionError("Server 3 unreachable"),
    ])

try:
    network_calls()
except* ConnectionError as eg:
    for exc in eg.exceptions:
        print(f"Retrying... ({exc})")
except* ____ as eg:
    for exc in eg.exceptions:
        print(f"Timeout! ({exc})")
