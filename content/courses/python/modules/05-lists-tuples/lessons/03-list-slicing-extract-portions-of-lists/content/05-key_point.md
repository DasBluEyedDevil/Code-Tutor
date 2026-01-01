---
type: "KEY_POINT"
title: "Key Takeaways"
---

- **Slice notation**: list[start:stop:step]
- **stop is EXCLUSIVE**: [2:5] gets indices 2, 3, 4 (NOT 5!)
- **All parameters optional**: [:], [5:], [:10], [::2]
- **Negative indices work**: [-3:] for last 3, [:-1] for all but last
- **Slicing creates copies**: Original list unchanged
- **Step parameter**: [::2] every 2nd, [::3] every 3rd
- **Reverse with [::-1]**: Negative step goes backwards
- **Length formula**: (stop - start) // step
- **Empty slices**: [5:5] or [10:20] beyond list → []

### Essential Slice Patterns:
```
# Getting portions:
first_n = my_list[:n]        # First n items
last_n = my_list[-n:]        # Last n items
middle = my_list[i:j]        # Items from i to j-1
all_but_first = my_list[1:]  # Everything except first
all_but_last = my_list[:-1]  # Everything except last

# Copying:
copy = my_list[:]            # Shallow copy

# Stepping:
every_2nd = my_list[::2]     # Even indices
odd_indices = my_list[1::2]  # Odd indices
every_3rd = my_list[::3]     # Every 3rd item

# Reversing:
reversed_list = my_list[::-1]  # Complete reverse

# Complex:
reverse_first_5 = my_list[4::-1]   # First 5 reversed
every_2nd_reversed = my_list[::-2]  # Every 2nd, backwards

```
### Common Use Cases:

- **Split data**: first_half = data[:len(data)//2]
- **Remove header/footer**: content = lines[1:-1]
- **Get top N**: top_10 = sorted(scores)[-10:]
- **Process chunks**: batch = items[i*batch_size:(i+1)*batch_size]
- **Reverse string**: reversed_str = text[::-1]

### Before Moving On:
Make sure you can:

- Extract first/last n items
- Get a range of items by index
- Copy a list with [:]
- Use negative indices in slices
- Skip items with step parameter
- Reverse a list with [::-1]
- Understand that stop is exclusive

### Coming Up Next:
In **Lesson 4: Tuples - Immutable Sequences**, you'll learn about:

- What tuples are and when to use them
- Creating tuples with ()
- Tuple unpacking
- Differences between lists and tuples
- When immutability is an advantage