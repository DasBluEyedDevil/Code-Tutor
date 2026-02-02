from typing import List, Optional, Dict
from dataclasses import dataclass
from datetime import datetime


# Layer 1: Data Model
@dataclass
class Task:
    """Task data model representing a single task.
    
    Attributes:
        id: Unique identifier for the task
        title: Brief title describing the task
        description: Detailed description of the task
        completed: Whether the task has been completed
        user_id: ID of the user who owns this task
        created_at: Timestamp when task was created
    """
    id: int
    title: str
    description: str
    completed: bool
    user_id: int
    created_at: datetime


# Layer 2: Repository (Data Access Layer)
class TaskRepository:
    """Handles all database operations for tasks.
    
    This class provides CRUD operations for Task objects,
    abstracting away the storage mechanism.
    """
    
    def __init__(self):
        # In-memory storage (would be database in production)
        self._tasks: Dict[int, Task] = {}
        self._next_id = 1
    
    def create(self, title: str, description: str, user_id: int) -> Task:
        """Create a new task and store it."""
        task = Task(
            id=self._next_id,
            title=title,
            description=description,
            completed=False,
            user_id=user_id,
            created_at=datetime.now()
        )
        self._tasks[task.id] = task
        self._next_id += 1
        return task
    
    def find_by_id(self, task_id: int) -> Optional[Task]:
        """Find a task by its unique ID."""
        return self._tasks.get(task_id)
    
    def find_by_user(self, user_id: int) -> List[Task]:
        """Find all tasks belonging to a specific user."""
        return [
            task for task in self._tasks.values()
            if task.user_id == user_id
        ]
    
    def list_all(self) -> List[Task]:
        """List all tasks in the repository."""
        return list(self._tasks.values())
    
    def update_status(self, task_id: int, completed: bool) -> Optional[Task]:
        """Update the completion status of a task."""
        task = self._tasks.get(task_id)
        if task:
            task.completed = completed
            return task
        return None
    
    def delete(self, task_id: int) -> bool:
        """Delete a task by ID. Returns True if deleted."""
        if task_id in self._tasks:
            del self._tasks[task_id]
            return True
        return False


# Layer 3: Service (Business Logic Layer)
class TaskService:
    """Business logic for task operations.
    
    This class contains validation rules and business logic,
    using the repository for data persistence.
    """
    
    def __init__(self, task_repo: TaskRepository):
        self.task_repo = task_repo
    
    def create_task(self, title: str, description: str, user_id: int) -> Dict:
        """Create a new task with validation.
        
        Args:
            title: Task title (min 3 characters)
            description: Task description (min 5 characters)
            user_id: ID of the task owner
        
        Returns:
            Dict with success status and task details or error message
        """
        # Validate title length
        if len(title) < 3:
            return {'error': 'Title must be at least 3 characters'}
        
        # Validate description length
        if len(description) < 5:
            return {'error': 'Description must be at least 5 characters'}
        
        # Validate user_id is positive
        if user_id <= 0:
            return {'error': 'Invalid user ID'}
        
        # Create the task
        task = self.task_repo.create(title, description, user_id)
        
        return {
            'success': True,
            'task': {
                'id': task.id,
                'title': task.title,
                'description': task.description,
                'completed': task.completed,
                'created_at': task.created_at.isoformat()
            }
        }
    
    def get_user_tasks(self, user_id: int) -> List[Dict]:
        """Get all tasks for a specific user."""
        tasks = self.task_repo.find_by_user(user_id)
        return [
            {
                'id': task.id,
                'title': task.title,
                'description': task.description,
                'completed': task.completed,
                'created_at': task.created_at.isoformat()
            }
            for task in tasks
        ]
    
    def mark_completed(self, task_id: int, user_id: int) -> Dict:
        """Mark a task as completed.
        
        Args:
            task_id: ID of the task to complete
            user_id: ID of the user (for authorization)
        
        Returns:
            Dict with success status or error message
        """
        task = self.task_repo.find_by_id(task_id)
        
        if not task:
            return {'error': 'Task not found'}
        
        # Check authorization
        if task.user_id != user_id:
            return {'error': 'Not authorized to modify this task'}
        
        updated_task = self.task_repo.update_status(task_id, True)
        
        return {
            'success': True,
            'message': f"Task '{updated_task.title}' marked as completed"
        }
    
    def get_task_stats(self, user_id: int) -> Dict:
        """Get task statistics for a user."""
        tasks = self.task_repo.find_by_user(user_id)
        completed = sum(1 for t in tasks if t.completed)
        pending = len(tasks) - completed
        
        return {
            'total': len(tasks),
            'completed': completed,
            'pending': pending,
            'completion_rate': round(completed / len(tasks) * 100, 1) if tasks else 0
        }


# Test the implementation
print('=== Task Management System ===')

# Initialize layers
repo = TaskRepository()
service = TaskService(repo)

# Create tasks
print('\n1. Creating tasks...')
result = service.create_task(
    title='Learn Python',
    description='Complete Module 14',
    user_id=1
)
print(f"   Task 1: {result}")

result = service.create_task(
    title='Build API',
    description='Create REST API with Flask',
    user_id=1
)
print(f"   Task 2: {result}")

result = service.create_task(
    title='Write Tests',
    description='Add pytest tests for all endpoints',
    user_id=1
)
print(f"   Task 3: {result}")

# Test validation
print('\n2. Testing validation...')
result = service.create_task('Hi', 'Short', 1)  # Title too short
print(f"   Invalid title: {result}")

# List tasks
print('\n3. User tasks:')
tasks = service.get_user_tasks(1)
for task in tasks:
    status = 'Done' if task['completed'] else 'Pending'
    print(f"   - [{status}] {task['title']}")

# Mark task as completed
print('\n4. Completing a task...')
result = service.mark_completed(1, 1)
print(f"   {result}")

# Get statistics
print('\n5. Task statistics:')
stats = service.get_task_stats(1)
print(f"   Total: {stats['total']}, Completed: {stats['completed']}, Pending: {stats['pending']}")
print(f"   Completion rate: {stats['completion_rate']}%")