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

  // Progress tracking implementation
  const progressFilePath = path.join(app.getPath('userData'), 'progress.json');

  async function readProgress(): Promise<any> {
    try {
      const data = await fs.readFile(progressFilePath, 'utf-8');
      return JSON.parse(data);
    } catch {
      return {};
    }
  }

  async function writeProgress(data: any): Promise<void> {
    await fs.writeFile(progressFilePath, JSON.stringify(data, null, 2));
  }

  // GET /api/progress/:userId? - Get user progress
  server.get('/api/progress/:userId?', async (req, res) => {
    try {
      const userId = req.params.userId || 'default';
      const allProgress = await readProgress();
      const userProgress = allProgress[userId] || {};
      res.json(userProgress);
    } catch (error: any) {
      console.error('Error reading progress:', error);
      res.status(500).json({ error: 'Failed to read progress' });
    }
  });

  // POST /api/progress - Save user progress
  server.post('/api/progress', async (req, res) => {
    try {
      const { courseId, moduleId, lessonId, userId = 'default', ...progressData } = req.body;

      if (!courseId || !moduleId || !lessonId) {
        return res.status(400).json({
          error: 'courseId, moduleId, and lessonId are required'
        });
      }

      const allProgress = await readProgress();

      if (!allProgress[userId]) {
        allProgress[userId] = {};
      }
      if (!allProgress[userId][courseId]) {
        allProgress[userId][courseId] = {};
      }
      if (!allProgress[userId][courseId][moduleId]) {
        allProgress[userId][courseId][moduleId] = {};
      }

      allProgress[userId][courseId][moduleId][lessonId] = {
        ...allProgress[userId][courseId][moduleId][lessonId],
        ...progressData,
        lastUpdated: new Date().toISOString(),
      };

      await writeProgress(allProgress);

      res.json({ success: true });
    } catch (error: any) {
      console.error('Error saving progress:', error);
      res.status(500).json({ error: 'Failed to save progress' });
    }
  });

  // Auth routes (simplified for desktop - single user)
  server.post('/api/auth/register', async (req, res) => {
    // Desktop app doesn't need real auth - return dummy token
    res.json({
      user: { id: '1', email: 'user@localhost', name: 'Desktop User' },
      token: 'desktop-user-token'
    });
  });

  server.post('/api/auth/login', async (req, res) => {
    // Desktop app doesn't need real auth - return dummy token
    res.json({
      user: { id: '1', email: 'user@localhost', name: 'Desktop User' },
      token: 'desktop-user-token'
    });
  });

  server.get('/api/auth/verify', async (req, res) => {
    // Always valid for desktop app
    res.json({
      valid: true,
      user: { id: '1', email: 'user@localhost', name: 'Desktop User' }
    });
  });

  console.log('API routes registered');
  console.log(`Progress file: ${progressFilePath}`);
}
