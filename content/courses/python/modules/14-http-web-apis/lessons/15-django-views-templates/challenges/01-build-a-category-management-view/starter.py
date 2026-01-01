from django.views.generic import ListView, CreateView, UpdateView, DeleteView
from django.contrib.auth.mixins import LoginRequiredMixin
from django.urls import reverse_lazy
from django.db.models import Count

# Assume Category model exists:
# class Category(models.Model):
#     name = models.CharField(max_length=50)
#     description = models.TextField(blank=True)
#     user = models.ForeignKey(User, on_delete=models.CASCADE)

# TODO: Create CategoryListView
class CategoryListView(LoginRequiredMixin, ListView):
    pass

# TODO: Create CategoryCreateView
class CategoryCreateView(LoginRequiredMixin, CreateView):
    pass

# TODO: Create CategoryUpdateView
class CategoryUpdateView(LoginRequiredMixin, UpdateView):
    pass

# TODO: Create CategoryDeleteView
class CategoryDeleteView(LoginRequiredMixin, DeleteView):
    pass

# TODO: URL patterns
# urlpatterns = [
#     ...
# ]