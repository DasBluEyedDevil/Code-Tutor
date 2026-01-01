package com.taskmanager.service;

import com.taskmanager.dto.CategoryRequest;
import com.taskmanager.dto.CategoryResponse;
import com.taskmanager.entity.Category;
import com.taskmanager.entity.User;
import com.taskmanager.exception.DuplicateResourceException;
import com.taskmanager.exception.ResourceNotFoundException;
import com.taskmanager.repository.CategoryRepository;
import com.taskmanager.repository.TaskRepository;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.junit.jupiter.MockitoExtension;

import java.util.List;
import java.util.Optional;

import static org.assertj.core.api.Assertions.*;
import static org.mockito.ArgumentMatchers.any;
import static org.mockito.Mockito.*;

@ExtendWith(MockitoExtension.class)
class CategoryServiceTest {

    @Mock
    private CategoryRepository categoryRepository;

    @Mock
    private TaskRepository taskRepository;

    @InjectMocks
    private CategoryService categoryService;

    private User testUser;
    private Category testCategory;
    private CategoryRequest validRequest;

    @BeforeEach
    void setUp() {
        testUser = new User();
        testUser.setId(1L);
        testUser.setEmail("test@example.com");

        testCategory = new Category();
        testCategory.setId(1L);
        testCategory.setName("Work");
        testCategory.setColor("#FF5733");
        testCategory.setUser(testUser);

        validRequest = new CategoryRequest();
        validRequest.setName("Work");
        validRequest.setColor("#FF5733");
    }

    @Test
    @DisplayName("createCategory should save and return category")
    void createCategory_Success() {
        when(categoryRepository.existsByNameAndUser("Work", testUser)).thenReturn(false);
        when(categoryRepository.save(any(Category.class))).thenAnswer(inv -> {
            Category cat = inv.getArgument(0);
            cat.setId(1L);
            return cat;
        });

        CategoryResponse response = categoryService.createCategory(validRequest, testUser);

        assertThat(response).isNotNull();
        assertThat(response.getName()).isEqualTo("Work");
        assertThat(response.getColor()).isEqualTo("#FF5733");
        verify(categoryRepository).save(any(Category.class));
    }

    @Test
    @DisplayName("createCategory should throw exception when name exists")
    void createCategory_DuplicateName_ThrowsException() {
        when(categoryRepository.existsByNameAndUser("Work", testUser)).thenReturn(true);

        assertThatThrownBy(() -> categoryService.createCategory(validRequest, testUser))
            .isInstanceOf(DuplicateResourceException.class)
            .hasMessageContaining("Category with name 'Work' already exists");

        verify(categoryRepository, never()).save(any());
    }

    @Test
    @DisplayName("getCategories should return all user categories")
    void getCategories_ReturnsUserCategories() {
        Category cat2 = new Category();
        cat2.setId(2L);
        cat2.setName("Personal");
        cat2.setUser(testUser);

        when(categoryRepository.findByUser(testUser)).thenReturn(List.of(testCategory, cat2));

        List<CategoryResponse> result = categoryService.getCategories(testUser);

        assertThat(result).hasSize(2);
        assertThat(result).extracting("name").containsExactly("Work", "Personal");
    }

    @Test
    @DisplayName("deleteCategory should unlink tasks and delete category")
    void deleteCategory_UnlinksTasks() {
        when(categoryRepository.findById(1L)).thenReturn(Optional.of(testCategory));
        doNothing().when(taskRepository).unlinkCategory(testCategory);
        doNothing().when(categoryRepository).delete(testCategory);

        categoryService.deleteCategory(1L, testUser);

        verify(taskRepository).unlinkCategory(testCategory);
        verify(categoryRepository).delete(testCategory);
    }

    @Test
    @DisplayName("deleteCategory should throw exception when not found")
    void deleteCategory_NotFound_ThrowsException() {
        when(categoryRepository.findById(999L)).thenReturn(Optional.empty());

        assertThatThrownBy(() -> categoryService.deleteCategory(999L, testUser))
            .isInstanceOf(ResourceNotFoundException.class);
    }
}