#!/usr/bin/env tsx

/**
 * Enhanced Flutter Course Converter - Extracts ALL Interactive Content
 * Source: /tmp/Flutter-Training-Course/lessons/
 * - 87 markdown lessons across 13 modules
 * - Parse sections: Introduction, Analogy, Code Examples, Exercises, Challenges
 * - Extract challenges with markers: "## ✅ YOUR CHALLENGE:", "**Requirements**:", etc.
 * - Convert to ContentSections + Challenges + Experiments
 */

import * as fs from 'fs/promises';
import * as path from 'path';

// ============================================================================
// Type Definitions
// ============================================================================

interface ContentSection {
  type: 'THEORY' | 'ANALOGY' | 'EXAMPLE' | 'KEY_POINT' | 'WARNING' | 'EXPERIMENT';
  title: string;
  content: string;
  code?: string;
  language?: string;
}

interface FreeCodingChallenge {
  type: 'FREE_CODING';
  id: string;
  title: string;
  description: string;
  instructions: string;
  starterCode: string;
  solution: string;
  language: string;
  testCases: TestCase[];
  hints?: Hint[];
  bonusChallenges?: BonusChallenge[];
  difficulty?: 'beginner' | 'intermediate' | 'advanced';
}

interface TestCase {
  description: string;
  expectedOutput: any;
  isVisible: boolean;
}

interface Hint {
  level: number;
  text: string;
  code?: string;
}

interface BonusChallenge {
  id: string;
  title: string;
  description: string;
  difficulty: 1 | 2 | 3 | 4 | 5;
  hints?: string[];
  solution?: string;
}

interface Experiment {
  id: string;
  title: string;
  description: string;
  code: string;
  language: string;
  expectedBehavior: string;
  takeaway: string;
}

interface InteractiveLesson {
  id: string;
  title: string;
  moduleId: string;
  order: number;
  estimatedMinutes: number;
  difficulty: 'beginner' | 'intermediate' | 'advanced';
  contentSections: ContentSection[];
  challenges: FreeCodingChallenge[];
  experiments?: Experiment[];
}

interface Module {
  id: string;
  title: string;
  description: string;
  difficulty: 'beginner' | 'intermediate' | 'advanced';
  estimatedHours: number;
  lessons: InteractiveLesson[];
}

interface Course {
  id: string;
  language: string;
  title: string;
  description: string;
  difficulty: 'beginner';
  estimatedHours: number;
  prerequisites: string[];
  modules: Module[];
  metadata: {
    version: string;
    lastUpdated: string;
    author: string;
    interactiveElements: {
      totalLessons: number;
      totalChallenges: number;
      totalExperiments: number;
      totalBonusChallenges: number;
    };
  };
}

// ============================================================================
// Markdown Parser for Flutter
// ============================================================================

interface ParsedMarkdown {
  title: string;
  sections: ContentSection[];
  challenges: FreeCodingChallenge[];
  experiments: Experiment[];
  estimatedMinutes: number;
}

function parseFlutterMarkdown(markdown: string, lessonId: string, moduleId: string): ParsedMarkdown {
  const lines = markdown.split('\n');
  const sections: ContentSection[] = [];
  const challenges: FreeCodingChallenge[] = [];
  const experiments: Experiment[] = [];

  let title = 'Untitled Lesson';
  let currentSection: ContentSection | null = null;
  let inCodeBlock = false;
  let codeContent: string[] = [];
  let codeLang = 'dart';
  let inChallenge = false;
  let challengeContent: string[] = [];
  let challengeIndex = 0;

  for (let i = 0; i < lines.length; i++) {
    const line = lines[i];

    // Extract main title
    if (line.startsWith('# ') && !title.includes('Module')) {
      title = line.replace('# ', '').trim();
      continue;
    }

    // Detect challenge sections
    if (line.includes('## ✅ YOUR CHALLENGE') || line.includes('## Challenge') || line.includes('## 🎯 Challenge')) {
      inChallenge = true;
      challengeContent = [];

      // Save previous section
      if (currentSection && currentSection.content.trim()) {
        sections.push(currentSection);
        currentSection = null;
      }
      continue;
    }

    // Detect end of challenge (next section or experiment)
    if (inChallenge && (line.startsWith('## ') && !line.includes('Requirement') && !line.includes('Hint') && !line.includes('Bonus'))) {
      // Process challenge
      const challenge = extractChallengeFromContent(challengeContent.join('\n'), lessonId, challengeIndex);
      if (challenge) {
        challenges.push(challenge);
        challengeIndex++;
      }
      inChallenge = false;
      challengeContent = [];
    }

    if (inChallenge) {
      challengeContent.push(line);
      continue;
    }

    // Code blocks
    if (line.startsWith('```')) {
      if (!inCodeBlock) {
        inCodeBlock = true;
        codeLang = line.replace('```', '').trim() || 'dart';
        codeContent = [];
      } else {
        inCodeBlock = false;
        if (currentSection && codeContent.length > 0) {
          currentSection.code = codeContent.join('\n');
          currentSection.language = codeLang;
        }
        codeContent = [];
      }
      continue;
    }

    if (inCodeBlock) {
      codeContent.push(line);
      continue;
    }

    // Detect experiments
    if (line.includes('**Try it!**') || line.includes('**Experiment:**')) {
      const expContent = extractExperimentFromLine(lines, i, lessonId, experiments.length);
      if (expContent.experiment) {
        experiments.push(expContent.experiment);
        i = expContent.endIndex;
      }
      continue;
    }

    // Section headers
    if (line.startsWith('## ')) {
      if (currentSection && currentSection.content.trim()) {
        sections.push(currentSection);
      }

      const sectionTitle = line.replace('## ', '').trim();
      let sectionType: ContentSection['type'] = 'THEORY';

      const lowerTitle = sectionTitle.toLowerCase();
      if (lowerTitle.includes('analogy') || lowerTitle.includes('think of') || lowerTitle.includes('imagine') || lowerTitle.includes('problem')) {
        sectionType = 'ANALOGY';
      } else if (lowerTitle.includes('example') || lowerTitle.includes('code') || lowerTitle.includes('your first')) {
        sectionType = 'EXAMPLE';
      } else if (lowerTitle.includes('key') || lowerTitle.includes('remember') || lowerTitle.includes('important') || lowerTitle.includes('summary')) {
        sectionType = 'KEY_POINT';
      } else if (lowerTitle.includes('warning') || lowerTitle.includes('pitfall') || lowerTitle.includes('common mistake') || lowerTitle.includes('gotcha')) {
        sectionType = 'WARNING';
      } else if (lowerTitle.includes('experiment') || lowerTitle.includes('try') || lowerTitle.includes('practice')) {
        sectionType = 'EXPERIMENT';
      }

      currentSection = {
        type: sectionType,
        title: sectionTitle,
        content: ''
      };
    } else if (line.startsWith('---')) {
      // Separator - save current section
      if (currentSection && currentSection.content.trim()) {
        sections.push(currentSection);
        currentSection = null;
      }
    } else {
      // Regular content
      if (currentSection) {
        currentSection.content += line + '\n';
      } else if (line.trim()) {
        currentSection = {
          type: 'THEORY',
          title: 'Introduction',
          content: line + '\n'
        };
      }
    }
  }

  // Save last section
  if (currentSection && currentSection.content.trim()) {
    sections.push(currentSection);
  }

  // Save last challenge if any
  if (inChallenge && challengeContent.length > 0) {
    const challenge = extractChallengeFromContent(challengeContent.join('\n'), lessonId, challengeIndex);
    if (challenge) {
      challenges.push(challenge);
    }
  }

  // Estimate time based on content
  const estimatedMinutes = Math.max(20, Math.min(60, sections.length * 5 + challenges.length * 10));

  return {
    title,
    sections: sections.filter(s => s.content.trim().length > 0),
    challenges,
    experiments,
    estimatedMinutes
  };
}

function extractChallengeFromContent(content: string, lessonId: string, index: number): FreeCodingChallenge | null {
  const lines = content.split('\n');

  let title = 'Practice Challenge';
  let description = '';
  let requirements: string[] = [];
  let hints: string[] = [];
  let bonusChallenges: BonusChallenge[] = [];
  let starterCode = '';
  let solution = '';
  let inCodeBlock = false;
  let codeContent: string[] = [];
  let isStarterCode = true;

  for (let i = 0; i < lines.length; i++) {
    const line = lines[i];

    // Extract title
    if (i < 3 && !title.includes('Challenge') && line.trim() && !line.startsWith('**')) {
      title = line.replace(/^#+\s*/, '').trim();
    }

    // Extract requirements
    if (line.includes('**Requirements:**') || line.includes('**Your Task:**')) {
      for (let j = i + 1; j < lines.length; j++) {
        if (lines[j].startsWith('- ') || lines[j].match(/^\d+\.\s/)) {
          requirements.push(lines[j].replace(/^[-\d.]\s*/, '').trim());
        } else if (lines[j].trim() === '') {
          continue;
        } else {
          break;
        }
      }
    }

    // Extract hints
    if (line.includes('**Hints:**') || line.includes('**Hint:**')) {
      for (let j = i + 1; j < lines.length; j++) {
        if (lines[j].startsWith('- ') || lines[j].match(/^\d+\.\s/)) {
          hints.push(lines[j].replace(/^[-\d.]\s*/, '').trim());
        } else if (lines[j].trim() === '') {
          continue;
        } else {
          break;
        }
      }
    }

    // Extract bonus challenges
    if (line.includes('**Bonus Challenge:**') || line.includes('**Extra Credit:**')) {
      const bonusDesc = lines.slice(i + 1, i + 5).join(' ').trim();
      bonusChallenges.push({
        id: `${lessonId}-bonus-${bonusChallenges.length}`,
        title: 'Bonus Challenge',
        description: bonusDesc,
        difficulty: 3
      });
    }

    // Code blocks
    if (line.startsWith('```')) {
      if (!inCodeBlock) {
        inCodeBlock = true;
        codeContent = [];
      } else {
        inCodeBlock = false;
        const code = codeContent.join('\n');
        if (isStarterCode && !starterCode) {
          starterCode = code;
        } else if (!solution) {
          solution = code;
        }
        isStarterCode = false;
        codeContent = [];
      }
      continue;
    }

    if (inCodeBlock) {
      codeContent.push(line);
    } else if (!line.startsWith('**') && !line.startsWith('- ') && line.trim()) {
      description += line + ' ';
    }
  }

  if (!requirements.length && !description.trim()) {
    return null;
  }

  const instructions = requirements.length > 0
    ? requirements.map((r, i) => `${i + 1}. ${r}`).join('\n')
    : description.trim();

  const hintObjects: Hint[] = hints.map((hint, i) => ({
    level: i + 1,
    text: hint
  }));

  return {
    type: 'FREE_CODING',
    id: `${lessonId}-challenge-${index}`,
    title,
    description: instructions,
    instructions,
    starterCode: starterCode || '// Your code here\n',
    solution: solution || starterCode || '',
    language: 'dart',
    testCases: [
      {
        description: 'Widget builds without errors',
        expectedOutput: '',
        isVisible: true
      }
    ],
    hints: hintObjects.length > 0 ? hintObjects : undefined,
    bonusChallenges: bonusChallenges.length > 0 ? bonusChallenges : undefined,
    difficulty: 'beginner'
  };
}

function extractExperimentFromLine(
  lines: string[],
  startIndex: number,
  lessonId: string,
  experimentIndex: number
): { experiment: Experiment | null; endIndex: number } {
  let code = '';
  let description = '';
  let expectedBehavior = '';
  let inCodeBlock = false;
  let codeContent: string[] = [];
  let endIndex = startIndex;

  for (let i = startIndex; i < Math.min(startIndex + 30, lines.length); i++) {
    const line = lines[i];
    endIndex = i;

    if (line.startsWith('```')) {
      if (!inCodeBlock) {
        inCodeBlock = true;
        codeContent = [];
      } else {
        inCodeBlock = false;
        code = codeContent.join('\n');
        break;
      }
      continue;
    }

    if (inCodeBlock) {
      codeContent.push(line);
    } else if (line.trim() && !line.startsWith('**')) {
      if (!code) {
        description += line + ' ';
      } else {
        expectedBehavior += line + ' ';
      }
    }

    if (line.startsWith('## ') || line.startsWith('---')) {
      break;
    }
  }

  if (!code) {
    return { experiment: null, endIndex };
  }

  return {
    experiment: {
      id: `${lessonId}-exp-${experimentIndex}`,
      title: 'Interactive Experiment',
      description: description.trim() || 'Try this code and observe the behavior',
      code,
      language: 'dart',
      expectedBehavior: expectedBehavior.trim() || 'Observe the output and behavior',
      takeaway: 'Understanding how this works helps build better Flutter apps'
    },
    endIndex
  };
}

// ============================================================================
// Main Conversion
// ============================================================================

async function convertFlutterInteractive(sourceDir: string, outputPath: string) {
  console.log('🔄 Converting Flutter course with ALL interactive content...\n');

  const lessonsDir = path.join(sourceDir, 'lessons');

  // Find all module directories
  const moduleDirs = (await fs.readdir(lessonsDir))
    .filter(d => d.startsWith('module-'))
    .sort();

  console.log(`📁 Found ${moduleDirs.length} module directories\n`);

  const modules: Module[] = [];
  let totalLessons = 0;
  let totalChallenges = 0;
  let totalExperiments = 0;
  let totalBonusChallenges = 0;

  for (const moduleDir of moduleDirs) {
    const moduleNumber = parseInt(moduleDir.replace('module-', ''));
    const modulePath = path.join(lessonsDir, moduleDir);

    const lessonFiles = (await fs.readdir(modulePath))
      .filter(f => f.endsWith('.md'))
      .sort();

    const lessons: InteractiveLesson[] = [];
    let moduleChallenges = 0;
    let moduleExperiments = 0;

    for (let i = 0; i < lessonFiles.length; i++) {
      const file = lessonFiles[i];
      const content = await fs.readFile(path.join(modulePath, file), 'utf-8');

      const lessonId = `${moduleNumber}.${i + 1}`;
      const moduleId = `module-${String(moduleNumber).padStart(2, '0')}`;

      const parsed = parseFlutterMarkdown(content, lessonId, moduleId);

      const bonusCount = parsed.challenges.reduce(
        (sum, c) => sum + (c.bonusChallenges?.length || 0),
        0
      );

      lessons.push({
        id: lessonId,
        title: parsed.title,
        moduleId,
        order: i + 1,
        estimatedMinutes: parsed.estimatedMinutes,
        difficulty: moduleNumber > 9 ? 'advanced' : moduleNumber > 5 ? 'intermediate' : 'beginner',
        contentSections: parsed.sections,
        challenges: parsed.challenges,
        experiments: parsed.experiments.length > 0 ? parsed.experiments : undefined
      });

      moduleChallenges += parsed.challenges.length;
      moduleExperiments += parsed.experiments.length;
      totalBonusChallenges += bonusCount;
    }

    const totalMinutes = lessons.reduce((sum, l) => sum + l.estimatedMinutes, 0);
    const estimatedHours = Math.ceil(totalMinutes / 60);

    let difficulty: 'beginner' | 'intermediate' | 'advanced' = 'beginner';
    if (moduleNumber > 9) difficulty = 'advanced';
    else if (moduleNumber > 5) difficulty = 'intermediate';

    modules.push({
      id: `module-${String(moduleNumber).padStart(2, '0')}`,
      title: `Module ${moduleNumber}: Flutter Development`,
      description: `Learn Flutter development - Module ${moduleNumber}`,
      difficulty,
      estimatedHours,
      lessons
    });

    console.log(`   ✅ Module ${moduleNumber}: ${lessons.length} lessons, ${moduleChallenges} challenges, ${moduleExperiments} experiments`);
    totalLessons += lessons.length;
    totalChallenges += moduleChallenges;
    totalExperiments += moduleExperiments;
  }

  const course: Course = {
    id: 'flutter',
    language: 'dart',
    title: 'Flutter Complete Development Course',
    description: 'Master Flutter from basics to advanced with 87+ interactive lessons, hands-on challenges, and experiments.',
    difficulty: 'beginner',
    estimatedHours: modules.reduce((sum, m) => sum + m.estimatedHours, 0),
    prerequisites: ['Basic programming knowledge recommended'],
    modules,
    metadata: {
      version: '2.0.0',
      lastUpdated: new Date().toISOString().split('T')[0],
      author: 'Code Tutor',
      interactiveElements: {
        totalLessons,
        totalChallenges,
        totalExperiments,
        totalBonusChallenges
      }
    }
  };

  // Write output
  const outputDir = path.dirname(outputPath);
  await fs.mkdir(outputDir, { recursive: true });
  await fs.writeFile(outputPath, JSON.stringify(course, null, 2), 'utf-8');

  console.log('\n✅ Conversion complete!\n');
  console.log('📊 Statistics:');
  console.log(`   📚 ${modules.length} modules`);
  console.log(`   📄 ${totalLessons} lessons`);
  console.log(`   💻 ${totalChallenges} coding challenges`);
  console.log(`   🧪 ${totalExperiments} experiments`);
  console.log(`   ⭐ ${totalBonusChallenges} bonus challenges`);
  console.log(`   ⏱️  ~${course.estimatedHours} hours of content`);
  console.log(`   💾 ${outputPath}\n`);
}

// ============================================================================
// Execute
// ============================================================================

const sourceDir = process.argv[2] || '/tmp/Flutter-Training-Course';
const outputPath = process.argv[3] || path.join(__dirname, '../content/courses/flutter/course.json');

convertFlutterInteractive(sourceDir, outputPath)
  .then(() => {
    console.log('✨ Flutter interactive course ready!');
    process.exit(0);
  })
  .catch((error) => {
    console.error('❌ Conversion failed:', error);
    process.exit(1);
  });
