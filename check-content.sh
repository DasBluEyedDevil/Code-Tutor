#!/bin/bash
# Complete Course Content Inventory

cd /home/user/Code-Tutor/content/courses

echo "=== COMPLETE COURSE CONTENT INVENTORY ==="
echo "Generated: $(date)"
echo ""
echo "Language     Modules  Lessons  Challenges  Quizzes  File Size"
echo "--------------------------------------------------------------------------"

for lang in java csharp python javascript kotlin flutter rust; do
  if [ -f "$lang/course.json" ]; then
    modules=$(jq '.modules | length' "$lang/course.json")
    lessons=$(jq '[.modules[].lessons[]] | length' "$lang/course.json")
    challenges=$(jq '[.modules[].lessons[].challenges[]?] | length' "$lang/course.json")
    quizzes=$(jq '[.modules[].quizzes[]?] | length' "$lang/course.json")
    filesize=$(du -h "$lang/course.json" | awk '{print $1}')

    printf "%-12s %7d  %7d  %10d  %7d  %9s\n" \
      "$lang" "$modules" "$lessons" "$challenges" "$quizzes" "$filesize"
  fi
done

echo ""
echo "=== TOTALS ==="
total_lessons=$(jq -s '[.[] | .modules[].lessons[]] | length' */course.json)
total_challenges=$(jq -s '[.[] | .modules[].lessons[].challenges[]?] | length' */course.json)
echo "Total Lessons: $total_lessons"
echo "Total Challenges: $total_challenges"
