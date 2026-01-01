# City Distance Calculator - Starter Code

import math

def calculate_distance(coord1, coord2):
    """Calculate distance between two coordinates using Haversine formula"""
    lat1, lon1 = coord1
    lat2, lon2 = coord2
    
    # Convert to radians
    lat1, lon1, lat2, lon2 = map(math.radians, [lat1, lon1, lat2, lon2])
    
    # Haversine formula
    dlat = lat2 - lat1
    dlon = lon2 - lon1
    a = math.sin(dlat/2)**2 + math.cos(lat1) * math.cos(lat2) * math.sin(dlon/2)**2
    c = 2 * math.asin(math.sqrt(a))
    
    # Earth radius in miles
    r = 3956
    
    return c * r

print("=== City Distance Calculator ===")
print()

# YOUR CODE: Create dictionary of cities with tuple coordinates
cities = {
    "New York":    (40.7128, -74.0060),
    "Los Angeles": (  ,   ),  # Fill in coordinates
    "Chicago":     (41.8781, -87.6298),
    "Houston":     (  ,   ),  # Fill in coordinates
    "Phoenix":     (33.4484, -112.0740)
}

# Display cities
print("Available cities:")
city_list = list(cities.keys())
for i, city_name in enumerate(city_list, start=1):
    coords = cities[city_name]
    lat, lon = coords  # Unpack tuple
    print(f"{i}. {city_name}: ({lat}, {lon})")

print()

# YOUR CODE: Get first city
choice1 = int(input("Select first city (1-5): "))
city1_name = city_list[choice1 - 1]
city1_coords = cities[city1_name]
lat1, lon1 = city1_coords  # Unpack

print(f"Selected: {city1_name} ({lat1}, {lon1})")
print()

# YOUR CODE: Get second city
choice2 = int(input("Select second city (1-5): "))
city2_name = city_list[  ]  # Convert 1-based to 0-based
city2_coords = cities[city2_name]
lat2, lon2 =   # Unpack coordinates

print(f"Selected: {city2_name} ({lat2}, {lon2})")
print()

# YOUR CODE: Calculate distance
distance = calculate_distance(  ,   )

# YOUR CODE: Determine which is further north (higher latitude)
if lat1 > lat2:
    north_city = city1_name
else:
    north_city = city2_name

# YOUR CODE: Determine which is further west (more negative longitude)
if lon1 < lon2:  # More negative = further west
    west_city = 
else:
    west_city = 

# Create results tuple
results = (distance, north_city, west_city)

# Display results
print("Results:")
print(f"  Distance: {results[0]:.1f} miles")
print(f"  Further north: {results[1]}")
print(f"  Further west: {results[2]}")