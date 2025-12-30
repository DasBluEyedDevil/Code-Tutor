import json
import argparse
import sys

def main():
    parser = argparse.ArgumentParser(description="Read a specific lesson from course.json")
    parser.add_argument("lesson_id", help="The ID of the lesson to read (e.g., module-01-lesson-01)")
    args = parser.parse_args()

    try:
        with open("content/courses/python/course.json", "r", encoding="utf-8") as f:
            data = json.load(f)
    except FileNotFoundError:
        print("Error: content/courses/python/course.json not found.")
        sys.exit(1)
    except UnicodeDecodeError as e:
        print(f"Error reading file: {e}")
        sys.exit(1)

    lesson_found = None
    for module in data["modules"]:
        for lesson in module["lessons"]:
            if lesson["id"] == args.lesson_id:
                lesson_found = lesson
                break
        if lesson_found:
            break

    if lesson_found:
        print(f"Title: {lesson_found['title']}")
        print("-" * 40)
        for section in lesson_found.get("contentSections", []):
            print(f"### {section['title']} ({section['type']})")
            print(section['content'])
            if 'code' in section:
                print("```python")
                print(section['code'])
                print("```")
            print()
    else:
        print(f"Lesson {args.lesson_id} not found.")

if __name__ == "__main__":
    main()
