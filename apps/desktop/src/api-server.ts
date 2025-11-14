import { Express } from 'express';
import * as path from 'path';
import * as fs from 'fs/promises';
import { app } from 'electron';
import { executeCode } from './executors';

// Get the content directory based on whether app is packaged
function getContentPath() {
  if (app.isPackaged) {
    return path.join(process.resourcesPath, 'content');
  }
  return path.join(__dirname, '../../../content');
}

export async function startApiServer(server: Express) {
  const contentPath = getContentPath();

  // GET /api/courses - List all available courses
  server.get('/api/courses', async (req, res) => {
    try {
      const coursesDir = path.join(contentPath, 'courses');
      const languages = await fs.readdir(coursesDir);

      const courses = [];
      for (const language of languages) {
        const courseJsonPath = path.join(coursesDir, language, 'course.json');
        try {
          const stats = await fs.stat(courseJsonPath);
          if (stats.isFile()) {
            const courseData = await fs.readFile(courseJsonPath, 'utf-8');
            const course = JSON.parse(courseData);
            courses.push({
              id: course.id,
              language: course.language,
              title: course.title,
              description: course.description,
              difficulty: course.difficulty,
              estimatedHours: course.estimatedHours,
              moduleCount: course.modules?.length || 0
            });
          }
        } catch (err) {
          // Skip if course.json doesn't exist
          console.warn(`No course.json found for ${language}`);
        }
      }

      res.json(courses);
    } catch (error: any) {
      console.error('Error fetching courses:', error);
      res.status(500).json({ error: 'Failed to fetch courses' });
    }
  });

  // GET /api/courses/:language - Get specific course with all content
  server.get('/api/courses/:language', async (req, res) => {
    try {
      const { language } = req.params;
      const courseJsonPath = path.join(contentPath, 'courses', language, 'course.json');

      const courseData = await fs.readFile(courseJsonPath, 'utf-8');
      const course = JSON.parse(courseData);

      res.json(course);
    } catch (error: any) {
      console.error(`Error fetching course ${req.params.language}:`, error);
      res.status(404).json({ error: 'Course not found' });
    }
  });

  // POST /api/execute - Execute code
  server.post('/api/execute', async (req, res) => {
    try {
      const { language, code } = req.body;

      if (!language || !code) {
        return res.status(400).json({
          error: 'Missing required fields: language and code'
        });
      }

      const result = await executeCode(language, code);
      res.json(result);
    } catch (error: any) {
      console.error('Execution error:', error);
      res.status(500).json({
        success: false,
        output: '',
        error: error.message || 'Execution failed'
      });
    }
  });

  // GET /api/progress - Get user progress (stub for now)
  server.get('/api/progress', async (req, res) => {
    // For desktop app, we'll store progress locally
    res.json({ message: 'Progress tracking coming soon' });
  });

  // POST /api/progress - Save user progress (stub for now)
  server.post('/api/progress', async (req, res) => {
    res.json({ success: true });
  });

  console.log('API routes registered');
}
