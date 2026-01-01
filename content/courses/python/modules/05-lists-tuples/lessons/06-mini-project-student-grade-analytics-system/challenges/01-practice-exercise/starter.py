# Movie Rating Analyzer - Starter Code

print("=== Movie Rating Analyzer ===")
print()

# YOUR CODE: Create movie data
# Each movie: (id, title, year, [ratings])
movies = [
    (1, "The Shawshank Redemption", 1994, [10, 9, 10, 9, 9, 10, 9]),
    (2, "The Dark Knight", 2008, [9, 9, 8, 10, 9, 8, 9]),
    # Add 4 more movies
]

print(f"Loaded {len(movies)} movies")
print()

# YOUR CODE: Display all movies with averages
print("All Movies:")
for movie_id, title, year, ratings in movies:
    avg =   # Calculate average
    print(f"{title} ({year}) - Avg: {avg:.1f}")

print()

# YOUR CODE: Calculate overall statistics
averages = [  # List comprehension for all averages
overall_avg = sum(averages) / len(averages)

print(f"Overall average rating: {overall_avg:.2f}")
print()

# YOUR CODE: Find highest rated
# Create list of (title, average) and sort
rated = [  # (title, avg) for each movie
rated.sort(key=lambda x: x[1], reverse=True)

print("Top 3 Movies:")
for rank, (title, avg) in enumerate(rated[:3], start=1):
    stars = "⭐" * int(avg)  # Round to nearest int
    print(f"{rank}. {title} - {avg:.1f} {stars}")

print()

# YOUR CODE: Filter highly rated (>=8)
highly_rated = [  # Filter movies with avg >= 8
print(f"Highly Rated Movies (>=8.0): {len(highly_rated)}")

# YOUR CODE: Filter recent (year >= 2020)
recent = [  # Filter movies from 2020 or later
print(f"Recent Movies (2020+): {len(recent)}")

print()

# YOUR CODE: Recommendations (recent AND highly rated)
recommendations = [  # Movies that are both recent and highly rated
if recommendations:
    print("Recommendations (Recent + Highly Rated):")
    for title, year, avg in recommendations:
        print(f"  - {title} ({year}) - {avg:.1f}")
else:
    print("No recent highly-rated movies found.")

print()

# YOUR CODE: Rating distribution
print("Rating Distribution:")
high_rated = len([  # 8-10
med_rated = len([   # 6-8
low_rated = len([   # <6

print(f"8-10: {'█' * high_rated} {high_rated} movies")
print(f"6-8:  {'█' * med_rated} {med_rated} movies")
print(f"<6:   {'█' * low_rated} {low_rated} movies")