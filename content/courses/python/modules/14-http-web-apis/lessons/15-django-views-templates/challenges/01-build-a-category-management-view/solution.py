from django.views.generic import ListView, CreateView, UpdateView, DeleteView
from django.contrib.auth.mixins import LoginRequiredMixin
from django.urls import reverse_lazy, path
from django.db.models import Count
from .models import Category


class CategoryListView(LoginRequiredMixin, ListView):
    """Display all categories for the current user."""
    model = Category
    template_name = 'categories/list.html'
    context_object_name = 'categories'
    
    def get_queryset(self):
        """Get user's categories with transaction count."""
        return Category.objects.filter(
            user=self.request.user
        ).annotate(
            transaction_count=Count('transaction')
        ).order_by('name')
    
    def get_context_data(self, **kwargs):
        context = super().get_context_data(**kwargs)
        context['title'] = 'Categories'
        return context


class CategoryCreateView(LoginRequiredMixin, CreateView):
    """Create a new category."""
    model = Category
    fields = ['name', 'description']
    template_name = 'categories/form.html'
    success_url = reverse_lazy('category-list')
    
    def form_valid(self, form):
        """Set the user before saving."""
        form.instance.user = self.request.user
        return super().form_valid(form)
    
    def get_context_data(self, **kwargs):
        context = super().get_context_data(**kwargs)
        context['title'] = 'New Category'
        return context


class CategoryUpdateView(LoginRequiredMixin, UpdateView):
    """Update an existing category."""
    model = Category
    fields = ['name', 'description']
    template_name = 'categories/form.html'
    success_url = reverse_lazy('category-list')
    
    def get_queryset(self):
        """Only allow editing user's own categories."""
        return Category.objects.filter(user=self.request.user)
    
    def get_context_data(self, **kwargs):
        context = super().get_context_data(**kwargs)
        context['title'] = f'Edit {self.object.name}'
        return context


class CategoryDeleteView(LoginRequiredMixin, DeleteView):
    """Delete a category with confirmation."""
    model = Category
    template_name = 'categories/confirm_delete.html'
    success_url = reverse_lazy('category-list')
    
    def get_queryset(self):
        """Only allow deleting user's own categories."""
        return Category.objects.filter(user=self.request.user)


# URL patterns
urlpatterns = [
    path('categories/', 
         CategoryListView.as_view(), 
         name='category-list'),
    path('categories/new/', 
         CategoryCreateView.as_view(), 
         name='category-create'),
    path('categories/<int:pk>/edit/', 
         CategoryUpdateView.as_view(), 
         name='category-update'),
    path('categories/<int:pk>/delete/', 
         CategoryDeleteView.as_view(), 
         name='category-delete'),
]


# Template: categories/list.html
template = '''
{% extends 'base.html' %}

{% block title %}{{ title }} - Finance Tracker{% endblock %}

{% block content %}
<div class="max-w-4xl mx-auto">
    <div class="flex justify-between items-center mb-6">
        <h1 class="text-2xl font-bold">{{ title }}</h1>
        <a href="{% url 'category-create' %}" 
           class="bg-blue-500 text-white px-4 py-2 rounded">
            + New Category
        </a>
    </div>
    
    {% if categories %}
        <div class="bg-white rounded-lg shadow">
            {% for category in categories %}
                <div class="p-4 border-b flex justify-between items-center">
                    <div>
                        <span class="font-medium">{{ category.name }}</span>
                        <span class="text-gray-500 text-sm ml-2">
                            {{ category.transaction_count }} transactions
                        </span>
                        {% if category.description %}
                            <p class="text-sm text-gray-600 mt-1">
                                {{ category.description|truncatewords:15 }}
                            </p>
                        {% endif %}
                    </div>
                    <div class="flex gap-2">
                        <a href="{% url 'category-update' category.pk %}" 
                           class="text-blue-500">Edit</a>
                        <a href="{% url 'category-delete' category.pk %}" 
                           class="text-red-500">Delete</a>
                    </div>
                </div>
            {% endfor %}
        </div>
    {% else %}
        <div class="bg-white rounded-lg shadow p-8 text-center text-gray-500">
            <p>No categories yet.</p>
            <a href="{% url 'category-create' %}" class="text-blue-500">
                Create your first category
            </a>
        </div>
    {% endif %}
</div>
{% endblock %}
'''

print(template)

print("\n=== Category Management Views ===")
print("\nViews Created:")
print("  CategoryListView   - List with transaction counts")
print("  CategoryCreateView - Create with auto user")
print("  CategoryUpdateView - Edit user's categories")
print("  CategoryDeleteView - Delete with confirmation")

print("\nURL Patterns:")
print("  /categories/           - List")
print("  /categories/new/       - Create")
print("  /categories/<pk>/edit/ - Update")
print("  /categories/<pk>/delete/ - Delete")