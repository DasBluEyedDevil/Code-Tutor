# Data Analyzer - COMPLETE SOLUTION

print("=== Sales Data Analyzer ===")
print()

# Create list of 12 monthly sales values
sales = [45000, 48000, 52000, 55000, 58000, 61000, 
         59000, 57000, 54000, 56000, 60000, 65000]

print(f"Monthly Sales: {sales}")
print()

# Extract quarterly data using slicing
print("Quarterly Analysis:")

# Q1 (indices 0-2)
q1 = sales[:3]  # First 3 months
q1_avg = sum(q1) / len(q1)
print(f"Q1 (Jan-Mar): {q1}")
print(f"  Average: ${q1_avg:,.2f}")
print()

# Q2 (indices 3-5)
q2 = sales[3:6]  # Months 3-6
q2_avg = sum(q2) / len(q2)
print(f"Q2 (Apr-Jun): {q2}")
print(f"  Average: ${q2_avg:,.2f}")
print()

# Q3 (indices 6-8)
q3 = sales[6:9]  # Months 6-9
q3_avg = sum(q3) / len(q3)
print(f"Q3 (Jul-Sep): {q3}")
print(f"  Average: ${q3_avg:,.2f}")
print()

# Q4 (indices 9-11)
q4 = sales[-3:]  # Last 3 months
q4_avg = sum(q4) / len(q4)
print(f"Q4 (Oct-Dec): {q4}")
print(f"  Average: ${q4_avg:,.2f}")
print()

# Find best quarter
quarter_avgs = [q1_avg, q2_avg, q3_avg, q4_avg]
best_avg = max(quarter_avgs)
best_quarter = quarter_avgs.index(best_avg) + 1  # +1 for 1-based quarter number

print(f"Best Quarter: Q{best_quarter} with average of ${best_avg:,.2f}")
print()

# Half-year analysis
print("Half-Year Analysis:")

first_half = sales[:6]  # First 6 months
first_half_avg = sum(first_half) / len(first_half)
print(f"First Half (Jan-Jun): Average ${first_half_avg:,.2f}")

second_half = sales[6:]  # Last 6 months
second_half_avg = sum(second_half) / len(second_half)
print(f"Second Half (Jul-Dec): Average ${second_half_avg:,.2f}")
print()

# Alternating months (every 2nd starting from 0)
alternating = sales[::2]  # Every other month
print(f"Alternating Months: {alternating}")
print()

# Reverse the data
reversed_sales = sales[::-1]  # Reverse using slicing
print(f"Reversed Data: {reversed_sales}")
print()

# Top 3 and bottom 3
# First, create a sorted copy
sorted_sales = sorted(sales)

top_3 = sorted_sales[-3:]  # Last 3 (highest)
bottom_3 = sorted_sales[:3]  # First 3 (lowest)

print(f"Top 3 Months: {top_3[::-1]}")  # Reverse to show high to low
print(f"Bottom 3 Months: {bottom_3}")