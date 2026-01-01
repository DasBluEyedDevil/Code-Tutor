---
type: "THEORY"
title: "What is Django REST Framework?"
---

**Django REST Framework (DRF) - Build APIs Fast**

Django REST Framework is a powerful toolkit for building Web APIs on top of Django.

**Key Components:**

**1. Serializers - Like Pydantic for Django**
Convert Django models to JSON and validate incoming data:
```python
class BookSerializer(serializers.ModelSerializer):
    class Meta:
        model = Book
        fields = ['id', 'title', 'author']
```

**2. ViewSets - CRUD in One Class**
Handle all standard operations (list, create, retrieve, update, delete):
```python
class BookViewSet(viewsets.ModelViewSet):
    queryset = Book.objects.all()
    serializer_class = BookSerializer
```

**3. Routers - Auto-Generate URLs**
No manual URL patterns needed:
```python
router = DefaultRouter()
router.register('books', BookViewSet)
# Creates: /books/, /books/{id}/, etc.
```

**4. Browsable API**
Built-in web interface for testing your API - no Postman needed!

**Why DRF over FastAPI?**

| Feature | DRF | FastAPI |
|---------|-----|----------|
| Ecosystem | Mature, huge plugin library | Growing rapidly |
| ORM | Django ORM built-in | Bring your own (SQLAlchemy) |
| Admin | Django Admin included | Build your own |
| Async | Limited | Native async |
| Best for | Full-featured web apps | Microservices, high-performance APIs |