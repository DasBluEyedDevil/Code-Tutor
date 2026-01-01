import json

# Read the file with UTF-8 encoding
with open('./content/courses/javascript/course.json', 'r', encoding='utf-8') as f:
    data = json.load(f)

# Find all module-21 occurrences
module_21_indices = []
for i, module in enumerate(data['modules']):
    if module.get('id') == 'module-21':
        module_21_indices.append(i)

print("Found " + str(len(module_21_indices)) + " module-21 modules at indices: " + str(module_21_indices))

# Show lesson counts for each
for idx in module_21_indices:
    lessons_count = len(data['modules'][idx].get('lessons', []))
    lesson_ids = [l['id'] for l in data['modules'][idx].get('lessons', [])]
    print("  Index " + str(idx) + ": " + str(lessons_count) + " lessons - " + str(lesson_ids))

# The first one should have 21.1-21.6, we need to add 21.7-21.10 to it
# Delete modules at indices 1 and 2 (keeping only the first one)
if len(module_21_indices) > 1:
    # Keep the first one, delete the rest
    # Extract lessons 21.7-21.10 from the third module (if it exists)
    if len(module_21_indices) >= 3:
        lessons_21_7_to_10 = []
        for lesson in data['modules'][module_21_indices[2]]['lessons']:
            if lesson['id'] in ['21.7', '21.8', '21.9', '21.10']:
                lessons_21_7_to_10.append(lesson)
        
        print("\nExtracted " + str(len(lessons_21_7_to_10)) + " lessons (21.7-21.10)")
        
        # Add these to the first module-21
        data['modules'][module_21_indices[0]]['lessons'].extend(lessons_21_7_to_10)
        
        print("First module-21 now has " + str(len(data['modules'][module_21_indices[0]]['lessons'])) + " lessons")
    
    # Delete the duplicate modules (keep only the first one)
    indices_to_delete = sorted([module_21_indices[1], module_21_indices[2]], reverse=True)
    for idx in indices_to_delete:
        del data['modules'][idx]
    
    print("\nDeleted modules at indices: " + str(indices_to_delete))
    print("Total modules now: " + str(len(data['modules'])))

# Save back with UTF-8 encoding
with open('./content/courses/javascript/course.json', 'w', encoding='utf-8') as f:
    json.dump(data, f, indent=2, ensure_ascii=False)

print("\nFile saved successfully")
