import json

with open('content/courses/flutter/course.json', 'r') as f:
    data = json.load(f)

print(f"Course: {data['title']}")
print("-" * 20)

for module in data['modules']:
    print(f"Module: {module['title']}")
    for lesson in module['lessons']:
        print(f"  Lesson: {lesson['title']}")
        if not lesson.get('contentSections'):
             print(f"    WARNING: No contentSections found!")
             continue

        for section in lesson['contentSections']:
            content = section.get('content', '')
            if len(content) < 100:
                print(f"    WARNING: Short content in section '{section['title']}': {len(content)} chars")
                print(f"      Content: {content.strip()}")
            if 'TODO' in content:
                print(f"    WARNING: 'TODO' found in section '{section['title']}'")
            if 'coming soon' in content.lower():
                print(f"    WARNING: 'Coming soon' found in section '{section['title']}'")

            code = section.get('code', '')
            if 'TODO' in code:
                print(f"    WARNING: 'TODO' found in code block of section '{section['title']}'")
