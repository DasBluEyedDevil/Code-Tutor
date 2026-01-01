class Temperature:
    def __init__(self, celsius=0):
        # TODO: Initialize with validation
        pass
    
    @property
    def celsius(self):
        # TODO: Return celsius
        pass
    
    @celsius.setter
    def celsius(self, value):
        # TODO: Validate and set celsius
        pass
    
    @property
    def fahrenheit(self):
        # TODO: Convert celsius to fahrenheit
        # Formula: (C * 9/5) + 32
        pass
    
    @fahrenheit.setter
    def fahrenheit(self, value):
        # TODO: Convert fahrenheit to celsius and set
        # Formula: (F - 32) * 5/9
        pass
    
    @property
    def kelvin(self):
        # TODO: Convert celsius to kelvin (read-only!)
        # Formula: C + 273.15
        pass

# TODO: Create temperature objects and test properties