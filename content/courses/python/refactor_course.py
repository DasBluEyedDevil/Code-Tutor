import os
import json
import re
import sys
import shutil

# Mapping of course language IDs to file extensions
LANGUAGE_EXTENSIONS = {
    "csharp": "cs",
    "python": "py",
    "java": "java",
    "javascript": "js",
    "typescript": "ts",
    "cpp": "cpp",
    "cplusplus": "cpp",
    "go": "go",
    "golang": "go",
    "rust": "rs",
    "swift": "swift",
    "kotlin": "kt",
    "dart": "dart",
    "flutter": "dart",
    "html": "html",
    "css": "css",
    "sql": "sql"
}

def slugify(text):
    """
    Converts text to a slug (e.g., "Getting Started" -> "getting-started").
    """
    if not text:
        return "unknown"
    text = text.lower()
    text = re.sub(r'[^a-z0-9\s-]', '', text)
    text = re.sub(r'[\s_-]+', '-', text)
    return text.strip('-')

def write_file(path, content):
    """Writes content to a file, ensuring the directory exists."""
    os.makedirs(os.path.dirname(path), exist_ok=True)
    with open(path, 'w', encoding='utf-8') as f:
        f.write(content)

def write_json(path, data):
    """Writes data to a JSON file with indentation."""
    write_file(path, json.dumps(data, indent=2, ensure_ascii=False))

def get_frontmatter(metadata):
    """Generates YAML frontmatter string from a dict."""
    yaml = "---\n"
    for key, value in metadata.items():
        if value:
            # Escape quotes if necessary, simplified for this use case
            clean_value = str(value).replace('"', '\\"')
            yaml += f'{key}: "{clean_value}"\n'
    yaml += "---\n\n"
    return yaml

def process_course(course_dir):
    json_path = os.path.join(course_dir, 'course.json')

    if not os.path.exists(json_path):
        print(f"Error: course.json not found in {course_dir}")
        return

    print(f"Processing {course_dir}...")

    # 1. Load original JSON
    with open(json_path, 'r', encoding='utf-8') as f:
        course_data = json.load(f)

    # 2. Determine file extension
    lang_id = course_data.get('language', 'text').lower()
    # Handle edge cases or default to txt
    file_ext = LANGUAGE_EXTENSIONS.get(lang_id, 'txt')

    # 3. Create Backup
    shutil.copy(json_path, json_path + '.bak')
    print("Created backup: course.json.bak")

    # 4. Process Modules
    modules = course_data.pop('modules', [])

    # Ensure modules directory exists
    modules_dir_base = os.path.join(course_dir, 'modules')
    if os.path.exists(modules_dir_base):
        # Optional: Warn or clean up existing modules dir if re-running
        print(f"Warning: '{modules_dir_base}' already exists. Merging/Overwriting...")

    for mod_index, module in enumerate(modules, 1):
        mod_title = module.get('title', f'module-{mod_index}')
        mod_slug = f"{mod_index:02d}-{slugify(mod_title)}"
        mod_dir = os.path.join(modules_dir_base, mod_slug)

        # Process Lessons
        lessons = module.pop('lessons', [])

        for lesson_index, lesson in enumerate(lessons, 1):
            # Use 'order' field if available, else iterator
            order = lesson.get('order', lesson_index)
            lesson_title = lesson.get('title', f'lesson-{order}')
            lesson_slug = f"{order:02d}-{slugify(lesson_title)}"
            lesson_dir = os.path.join(mod_dir, 'lessons', lesson_slug)

            # Process Content Sections
            sections = lesson.pop('contentSections', [])
            for sec_index, section in enumerate(sections, 1):
                sec_type = section.get('type', 'text').lower()
                sec_filename = f"{sec_index:02d}-{sec_type}.md"
                sec_path = os.path.join(lesson_dir, 'content', sec_filename)

                # Prepare Frontmatter
                frontmatter_data = {
                    "type": section.get('type'),
                    "title": section.get('title'),
                }

                # Prepare Content
                md_content = get_frontmatter(frontmatter_data)
                md_content += section.get('content', '') or ''

                # Append Code if present
                code_snippet = section.get('code')
                if code_snippet:
                    code_lang = section.get('language', lang_id)
                    md_content += f"\n\n```{code_lang}\n{code_snippet}\n```\n"

                write_file(sec_path, md_content)

            # Process Challenges
            challenges = lesson.pop('challenges', [])
            for chal_index, challenge in enumerate(challenges, 1):
                chal_title = challenge.get('title', f'challenge-{chal_index}')
                chal_slug = f"{chal_index:02d}-{slugify(chal_title)}"
                chal_dir = os.path.join(lesson_dir, 'challenges', chal_slug)

                # Extract code files
                starter_code = challenge.pop('starterCode', None)
                solution_code = challenge.pop('solution', None)

                # Write Metadata
                write_json(os.path.join(chal_dir, 'challenge.json'), challenge)

                # Write Code Files
                if starter_code:
                    write_file(os.path.join(chal_dir, f'starter.{file_ext}'), starter_code)
                if solution_code:
                    write_file(os.path.join(chal_dir, f'solution.{file_ext}'), solution_code)

            # Write Lesson Metadata
            write_json(os.path.join(lesson_dir, 'lesson.json'), lesson)

        # Write Module Metadata
        write_json(os.path.join(mod_dir, 'module.json'), module)

    # 5. Overwrite root course.json with stripped metadata
    write_json(json_path, course_data)
    print("Refactoring complete.")

if __name__ == "__main__":
    # Use argument if provided, otherwise current directory
    target_dir = sys.argv[1] if len(sys.argv) > 1 else os.getcwd()

    if os.path.isdir(target_dir):
        process_course(target_dir)
    else:
        print(f"Error: {target_dir} is not a directory.")