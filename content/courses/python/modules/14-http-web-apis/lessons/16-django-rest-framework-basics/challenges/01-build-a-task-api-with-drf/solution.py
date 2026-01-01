from rest_framework import serializers, viewsets, status
from rest_framework.decorators import action
from rest_framework.response import Response
from rest_framework.permissions import IsAuthenticated
from rest_framework.routers import DefaultRouter

# TaskSerializer - handles data transformation
class TaskSerializer(serializers.ModelSerializer):
    """Serializer for Task model."""
    project_name = serializers.CharField(
        source='project.name',
        read_only=True
    )
    
    class Meta:
        model = Task
        fields = [
            'id',
            'title',
            'completed',
            'project',
            'project_name',
            'created_at'
        ]
        read_only_fields = ['created_at']


# TaskViewSet - handles CRUD operations
class TaskViewSet(viewsets.ModelViewSet):
    """API endpoint for tasks."""
    serializer_class = TaskSerializer
    permission_classes = [IsAuthenticated]
    
    def get_queryset(self):
        """Return only current user's tasks."""
        return Task.objects.filter(
            user=self.request.user
        ).select_related('project')
    
    def perform_create(self, serializer):
        """Set user when creating task."""
        serializer.save(user=self.request.user)
    
    @action(detail=True, methods=['post'])
    def complete(self, request, pk=None):
        """Mark a task as complete: POST /tasks/{id}/complete/"""
        task = self.get_object()
        task.completed = True
        task.save()
        serializer = self.get_serializer(task)
        return Response(serializer.data)
    
    @action(detail=False, methods=['get'])
    def pending(self, request):
        """Get all pending tasks: GET /tasks/pending/"""
        pending_tasks = self.get_queryset().filter(completed=False)
        serializer = self.get_serializer(pending_tasks, many=True)
        return Response(serializer.data)


# Router setup
router = DefaultRouter()
router.register('tasks', TaskViewSet, basename='task')
urlpatterns = router.urls


print("=== Task API with DRF ===")

print("\nEndpoints Created:")
print("  GET    /tasks/              - List user's tasks")
print("  POST   /tasks/              - Create task")
print("  GET    /tasks/{id}/         - Get task")
print("  PUT    /tasks/{id}/         - Update task")
print("  DELETE /tasks/{id}/         - Delete task")
print("  POST   /tasks/{id}/complete/- Mark complete")
print("  GET    /tasks/pending/      - List pending")

print("\nSerializer Features:")
print("  - project_name computed from related model")
print("  - created_at is read-only")

print("\nViewSet Features:")
print("  - Filtered by current user")
print("  - Auto-sets user on create")
print("  - Custom complete action")