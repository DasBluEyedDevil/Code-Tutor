import React, { useState, useEffect } from 'react';

interface Task {
  id: string;
  title: string;
  description: string;
  completed: boolean;
  createdAt: string;
}

type StatusFilter = 'all' | 'active' | 'completed';
type SortOption = 'newest' | 'oldest' | 'status';

interface FilteredTaskListProps {
  userId: string;
}

// TODO: Implement filter functions
const filterBySearch = (tasks: Task[], searchTerm: string): Task[] => {
  // Your code here
};

const filterByStatus = (tasks: Task[], status: StatusFilter): Task[] => {
  // Your code here
};

const sortTasks = (tasks: Task[], sortBy: SortOption): Task[] => {
  // Your code here
};

const FilteredTaskList: React.FC<FilteredTaskListProps> = ({ userId }) => {
  const [tasks, setTasks] = useState<Task[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  
  // TODO: Add state for search, status filter, and sort option
  
  useEffect(() => {
    // Fetch tasks (already implemented)
    const fetchTasks = async () => {
      setLoading(true);
      const response = await fetch(`/api/tasks?userId=${userId}`);
      const data = await response.json();
      setTasks(data.tasks);
      setLoading(false);
    };
    fetchTasks();
  }, [userId]);

  // TODO: Apply filters and sorting to tasks
  const displayedTasks = tasks;

  return (
    <div className="filtered-task-list">
      {/* TODO: Add search input */}
      {/* TODO: Add status filter buttons */}
      {/* TODO: Add sort dropdown */}
      
      <ul>
        {displayedTasks.map(task => (
          <li key={task.id}>{task.title}</li>
        ))}
      </ul>
    </div>
  );
};

export default FilteredTaskList;