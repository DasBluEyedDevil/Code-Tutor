# Movie Rating Analyzer - COMPLETE SOLUTION

print("=== Movie Rating Analyzer ===")
print()

# Create movie data
# Each movie: (id, title, year, [ratings])
movies = [
    (1, "The Shawshank Redemption", 1994, [10, 9, 10, 9, 9, 10, 9]),
    (2, "The Dark Knight", 2008, [9, 9, 8, 10, 9, 8, 9]),
    (3, "Inception", 2010, [9, 8, 9, 8, 9, 8, 9]),
    (4, "Dune", 2021, [8, 9, 8, 8, 9, 9, 8]),
    (5, "Everything Everywhere", 2022, [9, 9, 9, 8, 9, 9, 9]),
    (6, "The Room", 2003, [3, 4, 2, 5, 3, 4, 3])
]

print(f"Loaded {len(movies)} movies")
print()

# Display all movies with averages
print("All Movies:")
for movie_id, title, year, ratings in movies:
    avg = sum(ratings) / len(ratings)
    print(f"{title} ({year}) - Avg: {avg:.1f}")

print()

# Calculate overall statistics
averages = [sum(ratings)/len(ratings) for (id, title, year, ratings) in movies]
overall_avg = sum(averages) / len(averages)

print(f"Overall average rating: {overall_avg:.2f}")
print()

# Find highest rated
rated = [(title, sum(ratings)/len(ratings)) for (id, title, year, ratings) in movies]
rated.sort(key=lambda x: x[1], reverse=True)

print("Top 3 Movies:")
for rank, (title, avg) in enumerate(rated[:3], start=1):
    stars = "⭐" * int(avg)
    print(f"{rank}. {title} - {avg:.1f} {stars}")

print()

# Filter highly rated (>=8)
highly_rated = [(title, sum(ratings)/len(ratings)) for (id, title, year, ratings) in movies if sum(ratings)/len(ratings) >= 8.0]
print(f"Highly Rated Movies (>=8.0): {len(highly_rated)}")

# Filter recent (year >= 2020)
recent = [(title, year) for (id, title, year, ratings) in movies if year >= 2020]
print(f"Recent Movies (2020+): {len(recent)}")

print()

# Recommendations (recent AND highly rated)
recommendations = [
    (title, year, sum(ratings)/len(ratings))
    for (id, title, year, ratings) in movies
    if year >= 2020 and sum(ratings)/len(ratings) >= 8.0
]

if recommendations:
    print("Recommendations (Recent + Highly Rated):")
    for title, year, avg in recommendations:
        print(f"  - {title} ({year}) - {avg:.1f}")
else:
    print("No recent highly-rated movies found.")

print()

# Rating distribution
print("Rating Distribution:")
high_rated = len([avg for avg in averages if avg >= 8])
med_rated = len([avg for avg in averages if 6 <= avg < 8])
low_rated = len([avg for avg in averages if avg < 6])

print(f"8-10: {'█' * high_rated} {high_rated} movies")
print(f"6-8:  {'█' * med_rated} {med_rated} movies")
print(f"<6:   {'█' * low_rated} {low_rated} movies")