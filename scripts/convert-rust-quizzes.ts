#!/usr/bin/env tsx

/**
 * Rust Quiz Converter - Extracts quizzes from markdown files
 * Adds quizzes to existing Rust course.json
 */

import * as fs from 'fs/promises';
import * as path from 'path';

interface Quiz {
  id: string;
  title: string;
  description?: string;
  passingScore: number;
  estimatedMinutes: number;
  questions: QuizQuestion[];
}

interface QuizQuestion {
  id: string;
  type: 'multiple-choice';
  question: string;
  options: string[];
  correctAnswer: number;
  explanation: string;
  points: number;
}

interface Module {
  id: string;
  title: string;
  description: string;
  order: number;
  estimatedHours: number;
  difficulty: string;
  lessons: any[];
  quizzes?: Quiz[];
}

interface Course {
  id: string;
  language: string;
  title: string;
  description: string;
  difficulty: string;
  estimatedHours: number;
  modules: Module[];
  metadata: any;
}

/**
 * Parse quiz from markdown file
 */
async function parseQuizMarkdown(filePath: string, moduleId: string): Promise<Quiz | null> {
  try {
    const content = await fs.readFile(filePath, 'utf-8');
    const lines = content.split('\n');

    let title = '';
    let currentQuestion = '';
    let currentOptions: string[] = [];
    let correctAnswer = -1;
    let questionCount = 0;
    const questions: QuizQuestion[] = [];
    let passingScore = 80;
    let inAnswerSection = false;
    let answerMap = new Map<number, string>(); // Map question number to answer letter

    // Extract title
    const titleMatch = content.match(/^#\s+(.+)/m);
    if (titleMatch) {
      title = titleMatch[1].replace(/📝\s*/, '').trim();
    }

    // Extract passing score if mentioned
    const passingMatch = content.match(/Passing Score[:\s]*(\d+)%/i);
    if (passingMatch) {
      passingScore = parseInt(passingMatch[1]);
    }

    // First pass: Extract answers from answer key
    const answerKeyMatch = content.match(/##\s+Answer Key.*?<details>(.*?)<\/details>/s) ||
                          content.match(/##\s+Answer(?:s|Key)(.*?)(?=##|$)/s);
    if (answerKeyMatch) {
      const answerSection = answerKeyMatch[1];
      // Match patterns like "**Q1: b**" or "1. c) GET"
      const answerMatches = answerSection.matchAll(/\*\*Q(\d+):\s*([a-d])\*\*|^(\d+)\.\s*([a-d])\)/gmi);
      for (const match of answerMatches) {
        const qNum = parseInt(match[1] || match[3]);
        const answer = (match[2] || match[4]).toLowerCase();
        answerMap.set(qNum, answer);
      }
    }

    // Second pass: Extract questions and options
    for (let i = 0; i < lines.length; i++) {
      const line = lines[i].trim();

      // Check if we entered the answers section
      if (line.match(/^##\s+Answers?/i) || line.match(/^##\s+Answer Key/i)) {
        break; // Stop processing questions
      }

      // Detect question (format: ### Question N (points) or ### N.)
      const questionMatch = line.match(/^###\s+(?:Question\s+)?(\d+)[\.\):\s]+(?:\((\d+)\s*points?\))?(.*)$/i);

      if (questionMatch) {
        // Save previous question if exists
        if (currentQuestion && currentOptions.length > 0) {
          // Determine correct answer from answer map or inline marking
          if (correctAnswer === -1 && answerMap.has(questionCount)) {
            const answerLetter = answerMap.get(questionCount)!;
            correctAnswer = answerLetter.charCodeAt(0) - 'a'.charCodeAt(0);
          }

          if (correctAnswer !== -1) {
            questions.push({
              id: `rust-quiz-${moduleId}-q${questionCount}`,
              type: 'multiple-choice',
              question: currentQuestion,
              options: currentOptions,
              correctAnswer,
              explanation: '',
              points: 2
            });
          }
        }

        // Start new question
        questionCount = parseInt(questionMatch[1]);
        let questionText = questionMatch[3].trim();
        currentOptions = [];
        correctAnswer = -1;

        // Check if question continues on next line (format 2: question in bold on next line)
        if (!questionText || questionText === '') {
          i++;
          while (i < lines.length) {
            const nextLine = lines[i].trim();
            if (nextLine.startsWith('**') && nextLine.endsWith('**')) {
              questionText = nextLine.replace(/\*\*/g, '').trim();
              break;
            } else if (nextLine && !nextLine.startsWith('a)') && !nextLine.startsWith('---')) {
              questionText = nextLine;
              break;
            }
            i++;
          }
        } else {
          // Remove bold markers if present
          questionText = questionText.replace(/\*\*/g, '').trim();
        }

        currentQuestion = questionText;
      }

      // Detect options (a), b), c), d))
      const optionMatch = line.match(/^([a-d])\)\s+(.+)/i);
      if (optionMatch && currentQuestion) {
        const optionLetter = optionMatch[1].toLowerCase();
        let optionText = optionMatch[2].trim();

        // Check if this is the correct answer (marked with ✓ or ✅)
        const isCorrect = optionText.includes('✓') || optionText.includes('✅');
        if (isCorrect) {
          correctAnswer = currentOptions.length;
          optionText = optionText.replace(/[✓✅]/g, '').trim();
        }

        currentOptions.push(optionText);
      }

      // Handle code blocks in questions
      if (line.startsWith('```') && currentQuestion) {
        let codeBlock = line + '\n';
        i++;
        while (i < lines.length && !lines[i].trim().startsWith('```')) {
          codeBlock += lines[i] + '\n';
          i++;
        }
        if (i < lines.length) {
          codeBlock += lines[i];
        }
        currentQuestion += '\n\n' + codeBlock;
      }
    }

    // Add last question
    if (currentQuestion && currentOptions.length > 0) {
      // Determine correct answer from answer map or inline marking
      if (correctAnswer === -1 && answerMap.has(questionCount)) {
        const answerLetter = answerMap.get(questionCount)!;
        correctAnswer = answerLetter.charCodeAt(0) - 'a'.charCodeAt(0);
      }

      if (correctAnswer !== -1) {
        questions.push({
          id: `rust-quiz-${moduleId}-q${questionCount}`,
          type: 'multiple-choice',
          question: currentQuestion,
          options: currentOptions,
          correctAnswer,
          explanation: '',
          points: 2
        });
      }
    }

    if (questions.length === 0) {
      console.log(`   ⚠️  No questions found in ${path.basename(filePath)}`);
      return null;
    }

    const quiz: Quiz = {
      id: `rust-quiz-${moduleId}`,
      title,
      description: `Assessment quiz for Module ${moduleId.replace('module-', '')}`,
      passingScore,
      estimatedMinutes: Math.ceil(questions.length * 1.5),
      questions
    };

    console.log(`   ✅ Module ${moduleId}: ${questions.length} questions, ${passingScore}% passing`);
    return quiz;

  } catch (error) {
    console.log(`   ⚠️  Error parsing ${filePath}: ${error}`);
    return null;
  }
}

/**
 * Main conversion function
 */
async function convertRustQuizzes() {
  console.log('🔄 Adding Rust quizzes to course...\n');

  const sourceDir = '/tmp/Rust-Training-Course/course_content/interactive';
  const courseFile = '/home/user/Code-Tutor/content/courses/rust/course.json';

  // Check if source directory exists
  try {
    await fs.access(sourceDir);
  } catch {
    console.log('❌ Source directory not found:', sourceDir);
    console.log('   Please clone: git clone https://github.com/DasBluEyedDevil/Rust-Training-Course.git /tmp/Rust-Training-Course');
    process.exit(1);
  }

  // Load existing course
  let course: Course;
  try {
    const courseData = await fs.readFile(courseFile, 'utf-8');
    course = JSON.parse(courseData);
  } catch {
    console.log('❌ Could not load course file:', courseFile);
    process.exit(1);
  }

  // Find quiz files
  const quizFiles = [
    { file: 'quiz_module_04.md', moduleId: 'module-04' },
    { file: 'quiz_module_06.md', moduleId: 'module-06' },
    { file: 'quiz_module_07.md', moduleId: 'module-07' },
    { file: 'quiz_module_08.md', moduleId: 'module-08' },
    { file: 'quiz_module_09.md', moduleId: 'module-09' },
    { file: 'quiz_module_12.md', moduleId: 'module-12' },
    { file: 'quiz_module_13.md', moduleId: 'module-13' },
    { file: 'quiz_module_14.md', moduleId: 'module-14' }
  ];

  console.log('📋 Processing quiz files...');
  let totalQuestions = 0;
  let quizzesAdded = 0;

  for (const { file, moduleId } of quizFiles) {
    const filePath = path.join(sourceDir, file);

    try {
      await fs.access(filePath);
      const quiz = await parseQuizMarkdown(filePath, moduleId);

      if (quiz) {
        // Find the module and add quiz
        const module = course.modules.find(m => m.id === moduleId);
        if (module) {
          if (!module.quizzes) {
            module.quizzes = [];
          }
          module.quizzes.push(quiz);
          totalQuestions += quiz.questions.length;
          quizzesAdded++;
        } else {
          console.log(`   ⚠️  Module ${moduleId} not found in course`);
        }
      }
    } catch {
      console.log(`   ⚠️  Quiz file not found: ${file}`);
    }
  }

  // Update metadata
  if (!course.metadata) {
    course.metadata = {};
  }
  if (!course.metadata.interactiveElements) {
    course.metadata.interactiveElements = {};
  }
  course.metadata.interactiveElements.totalQuizzes = quizzesAdded;
  course.metadata.interactiveElements.totalQuizQuestions = totalQuestions;
  course.metadata.lastUpdated = new Date().toISOString().split('T')[0];
  course.metadata.version = '2.1.0';

  // Save updated course
  await fs.writeFile(courseFile, JSON.stringify(course, null, 2), 'utf-8');

  console.log('\n✅ Conversion complete!\n');
  console.log('📊 Statistics:');
  console.log(`   📋 ${quizzesAdded} quizzes added`);
  console.log(`   ❓ ${totalQuestions} total questions`);
  console.log(`   💾 ${courseFile}\n`);
  console.log('✨ Rust course now includes quizzes!');
}

// Run converter
convertRustQuizzes().catch(console.error);
