import json

with open('content/courses/javascript/course.json', 'r', encoding='utf-8') as f:
    course = json.load(f)

# Find Module 9
module9 = next((m for m in course['modules'] if m['id'] == 'module-error-handling'), None)

print("=== MODULE 9 (ERROR HANDLING) VERIFICATION ===\n")
print(f"Module ID: {module9['id']}")
print(f"Module Title: {module9['title']}")
print(f"Estimated Hours: {module9['estimatedHours']}")
print(f"Number of Lessons: {len(module9['lessons'])}\n")

print("=== LESSON DETAILS ===\n")

expected_times = [30, 25, 30, 35, 30]

for idx, lesson in enumerate(module9['lessons']):
    print(f"Lesson {lesson['order']}: {lesson['title']}")
    print(f"  ID: {lesson['id']}")
    print(f"  Estimated Minutes: {lesson['estimatedMinutes']}")
    print(f"  moduleId: {lesson['moduleId']}")
    
    content_types = [s['type'] for s in lesson['contentSections']]
    print(f"  Content sections ({len(content_types)}): {', '.join(content_types)}")
    
    # Check ANALOGY length
    analogy = next((s for s in lesson['contentSections'] if s['type'] == 'ANALOGY'), None)
    if analogy:
        print(f"    - ANALOGY: {len(analogy['content'])} chars")
    
    # Count examples
    examples = [s for s in lesson['contentSections'] if s['type'] == 'EXAMPLE']
    print(f"    - EXAMPLE: {len(examples)} examples")
    
    # Check for challenge
    has_challenge = len(lesson.get('challenges', [])) > 0
    print(f"    - Challenge: {'YES' if has_challenge else 'NO'}")
    print()

print("\n=== SPEC COMPLIANCE CHECKLIST ===\n")

# Check 1: Module 9 exists
print(f"✓ Module 9 exists: {'YES' if module9 else 'NO'}")

# Check 2: 5 lessons
print(f"✓ Has 5 lessons: {'YES' if len(module9['lessons']) == 5 else 'NO'} (found {len(module9['lessons'])})")

# Check 3: Estimated hours 2.5
print(f"✓ Estimated hours is 2.5: {'YES' if module9['estimatedHours'] == 2.5 else 'NO'}")

# Check 4: Lesson times match spec [30, 25, 30, 35, 30]
times_match = all(lesson['estimatedMinutes'] == expected_times[idx] 
                   for idx, lesson in enumerate(module9['lessons']))
print(f"✓ Lesson times [30,25,30,35,30]: {'YES' if times_match else 'NO'}")

# Check 5: Each lesson has ANALOGY, EXAMPLES, THEORY, WARNING
all_complete = True
for lesson in module9['lessons']:
    types = [s['type'] for s in lesson['contentSections']]
    has_required = all(t in types for t in ['ANALOGY', 'EXAMPLE', 'THEORY', 'WARNING'])
    has_challenge = len(lesson.get('challenges', [])) > 0
    
    if not has_required or not has_challenge:
        all_complete = False
        print(f"  ✗ Lesson {lesson['id']}: missing required content")

print(f"✓ All lessons have required sections: {'YES' if all_complete else 'NO'}")

# Check 6: Module inserted as 9, others renumbered
module10 = next((m for m in course['modules'] if m['id'] == 'module-10'), None)
module11 = next((m for m in course['modules'] if m['id'] == 'module-11'), None)

print(f"✓ Subsequent modules renumbered (Module 10 exists): {'YES' if module10 else 'NO'}")
print(f"✓ Subsequent modules renumbered (Module 11 exists): {'YES' if module11 else 'NO'}")

# Check 7: Lesson IDs start with 9.X
lesson_ids_correct = all(lesson['id'].startswith('9.') for lesson in module9['lessons'])
print(f"✓ Lesson IDs are 9.1-9.5: {'YES' if lesson_ids_correct else 'NO'}")

# Check 8: Module IDs updated
module_ids_correct = all(lesson['moduleId'] == 'module-error-handling' for lesson in module9['lessons'])
print(f"✓ Lesson moduleIds updated: {'YES' if module_ids_correct else 'NO'}")

print("\n=== FINAL VERDICT ===\n")
all_pass = (module9 and 
            len(module9['lessons']) == 5 and
            module9['estimatedHours'] == 2.5 and
            times_match and
            all_complete and
            module10 is not None and
            module11 is not None and
            lesson_ids_correct and
            module_ids_correct)

if all_pass:
    print("✅ SPEC COMPLIANCE: PASS")
else:
    print("❌ SPEC COMPLIANCE: FAIL")
