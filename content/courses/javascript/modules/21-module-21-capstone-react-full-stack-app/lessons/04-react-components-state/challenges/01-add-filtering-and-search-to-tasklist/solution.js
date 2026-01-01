import React, { useState, useEffect, useMemo } from 'react';

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

const filterBySearch = (tasks: Task[], searchTerm: string): Task[] => {
  if (!searchTerm.trim()) return tasks;
  const term = searchTerm.toLowerCase();
  return tasks.filter(task =>
    task.title.toLowerCase().includes(term) ||
    task.description.toLowerCase().includes(term)
  );
};

const filterByStatus = (tasks: Task[], status: StatusFilter): Task[] => {
  if (status === 'all') return tasks;
  return tasks.filter(task =>
    status === 'completed' ? task.completed : !task.completed
  );
};

const sortTasks = (tasks: Task[], sortBy: SortOption): Task[] => {
  const sorted = [...tasks];
  switch (sortBy) {
    case 'newest':
      return sorted.sort((a, b) =>
        new Date(b.createdAt).getTime() - new Date(a.createdAt).getTime()
      );
    case 'oldest':
      return sorted.sort((a, b) =>
        new Date(a.createdAt).getTime() - new Date(b.createdAt).getTime()
      );
    case 'status':
      return sorted.sort((a, b) =>
        Number(a.completed) - Number(b.completed)
      );
    default:
      return sorted;
  }
};

const FilteredTaskList: React.FC<FilteredTaskListProps> = ({ userId }) => {
  const [tasks, setTasks] = useState<Task[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [searchTerm, setSearchTerm] = useState<string>('');
  const [statusFilter, setStatusFilter] = useState<StatusFilter>('all');
  const [sortOption, setSortOption] = useState<SortOption>('newest');

  useEffect(() => {
    const fetchTasks = async () => {
      setLoading(true);
      const response = await fetch(`/api/tasks?userId=${userId}`);
      const data = await response.json();
      setTasks(data.tasks);
      setLoading(false);
    };
    fetchTasks();
  }, [userId]);

  const displayedTasks = useMemo(() => {
    let result = filterBySearch(tasks, searchTerm);
    result = filterByStatus(result, statusFilter);
    result = sortTasks(result, sortOption);
    return result;
  }, [tasks, searchTerm, statusFilter, sortOption]);

  if (loading) return <div>Loading...</div>;

  return (
    <div className="filtered-task-list">
      <div className="filters">
        <input
          type="text"
          placeholder="Search tasks..."
          value={searchTerm}
          onChange={(e) => setSearchTerm(e.target.value)}
          className="search-input"
        />
        
        <div className="status-filters">
          {(['all', 'active', 'completed'] as StatusFilter[]).map(status => (
            <button
              key={status}
              onClick={() => setStatusFilter(status)}
              className={statusFilter === status ? 'active' : ''}
            >
              {status.charAt(0).toUpperCase() + status.slice(1)}
            </button>
          ))}
        </div>
        
        <select
          value={sortOption}
          onChange={(e) => setSortOption(e.target.value as SortOption)}
          className="sort-select"
        >
          <option value="newest">Newest First</option>
          <option value="oldest">Oldest First</option>
          <option value="status">By Status</option>
        </select>
      </div>
      
      <p className="task-count">
        Showing {displayedTasks.length} of {tasks.length} tasks
      </p>
      
      <ul className="task-list">
        {displayedTasks.map(task => (
          <li key={task.id} className={task.completed ? 'completed' : ''}>
            <span className="task-title">{task.title}</span>
            <span className="task-status">
              {task.completed ? 'Done' : 'Pending'}
            </span>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default FilteredTaskList;