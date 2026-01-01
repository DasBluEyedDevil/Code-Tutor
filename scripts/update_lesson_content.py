import json
import argparse
import sys
import os

def main():
    parser = argparse.ArgumentParser(description="Update a specific lesson's content section in course.json")
    parser.add_argument("lesson_id", help="The ID of the lesson to update")
    parser.add_argument("section_index", type=int, help="The index of the content section to update (0-based)")
    parser.add_argument("new_code_file", help="Path to a file containing the new code content")
    args = parser.parse_args()

    course_file = "content/courses/python/course.json"

    try:
        with open(course_file, "r", encoding="utf-8") as f:
            data = json.load(f)
    except FileNotFoundError:
        print(f"Error: {course_file} not found.")
        sys.exit(1)
    except UnicodeDecodeError as e:
        print(f"Error reading {course_file}: {e}")
        sys.exit(1)

    try:
        with open(args.new_code_file, "r", encoding="utf-8") as f:
            new_code = f.read()
    except FileNotFoundError:
        print(f"Error: {args.new_code_file} not found.")
        sys.exit(1)
    except UnicodeDecodeError as e:
        print(f"Error reading {args.new_code_file}: {e}")
        sys.exit(1)

    lesson_found = False
    for module in data["modules"]:
        for lesson in module["lessons"]:
            if lesson["id"] == args.lesson_id:
                lesson_found = True
                if 0 <= args.section_index < len(lesson.get("contentSections", [])):
                    section = lesson["contentSections"][args.section_index]
                    if "code" in section:
                        print(f"Updating code in lesson {args.lesson_id}, section {args.section_index}")
                        section["code"] = new_code
                        section["language"] = "python" # Ensure language is set
                    else:
                        print(f"Error: Section {args.section_index} in lesson {args.lesson_id} does not have a 'code' field.")
                        sys.exit(1)
                else:
                    print(f"Error: Section index {args.section_index} out of range for lesson {args.lesson_id}.")
                    sys.exit(1)
                break
        if lesson_found:
            break

    if lesson_found:
        with open(course_file, "w", encoding="utf-8") as f:
            json.dump(data, f, indent=2, ensure_ascii=False)
        print(f"Successfully updated {course_file}")
    else:
        print(f"Lesson {args.lesson_id} not found.")
        sys.exit(1)

if __name__ == "__main__":
    main()
