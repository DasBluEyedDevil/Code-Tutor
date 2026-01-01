export default function TaskCard({ task, onEdit, onDelete }) {
  const priorityColors = {
    URGENT: 'bg-red-100 border-red-500',
    HIGH: 'bg-orange-100 border-orange-500',
    MEDIUM: 'bg-yellow-100 border-yellow-500',
    LOW: 'bg-green-100 border-green-500',
  };

  const isOverdue = task.dueDate && 
    new Date(task.dueDate) < new Date() && 
    task.status !== 'COMPLETED';

  return (
    <div className={`border-l-4 rounded-lg p-4 shadow-sm ${priorityColors[task.priority]}`}>
      <div className="flex justify-between items-start">
        <h3 className="font-semibold text-lg">{task.title}</h3>
        <span className={`text-xs px-2 py-1 rounded ${
          task.status === 'COMPLETED' ? 'bg-green-500 text-white' :
          task.status === 'IN_PROGRESS' ? 'bg-blue-500 text-white' :
          'bg-gray-500 text-white'
        }`}>
          {task.status.replace('_', ' ')}
        </span>
      </div>

      {task.description && (
        <p className="text-gray-600 text-sm mt-2 line-clamp-2">
          {task.description}
        </p>
      )}

      <div className="flex justify-between items-center mt-4">
        <div className="text-sm text-gray-500">
          {task.dueDate && (
            <span className={isOverdue ? 'text-red-600 font-medium' : ''}>
              {isOverdue && '⚠️ '}
              Due: {new Date(task.dueDate).toLocaleDateString()}
            </span>
          )}
        </div>

        <div className="flex gap-2">
          <button
            onClick={() => onEdit(task.id)}
            className="text-blue-600 hover:text-blue-800 text-sm"
          >
            Edit
          </button>
          <button
            onClick={() => onDelete(task.id)}
            className="text-red-600 hover:text-red-800 text-sm"
          >
            Delete
          </button>
        </div>
      </div>
    </div>
  );
}