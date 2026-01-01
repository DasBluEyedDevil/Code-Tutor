from rest_framework import serializers, viewsets
from rest_framework.decorators import action
from rest_framework.response import Response
from rest_framework.permissions import IsAuthenticated
from rest_framework.routers import DefaultRouter

# Assume these models exist:
# class Project(models.Model):
#     name = models.CharField(max_length=100)
#     user = models.ForeignKey(User, on_delete=models.CASCADE)
#
# class Task(models.Model):
#     title = models.CharField(max_length=200)
#     completed = models.BooleanField(default=False)
#     project = models.ForeignKey(Project, on_delete=models.CASCADE)
#     user = models.ForeignKey(User, on_delete=models.CASCADE)
#     created_at = models.DateTimeField(auto_now_add=True)

# TODO: Create TaskSerializer
class TaskSerializer(serializers.ModelSerializer):
    pass

# TODO: Create TaskViewSet
class TaskViewSet(viewsets.ModelViewSet):
    pass

# TODO: Setup router
# router = ...