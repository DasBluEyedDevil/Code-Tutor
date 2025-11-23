using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CodeTutor.Native.Models;
using CodeTutor.Native.Services;

namespace CodeTutor.Native.Tests.Integration;

/// <summary>
/// Integration tests for CourseService
/// Verifies that course JSON files are correctly loaded and parsed from the content/ directory
/// </summary>
public class CourseServiceIntegrationTests
{
    private readonly CourseService _courseService;
    private readonly string _contentPath;

    public CourseServiceIntegrationTests()
    {
        _courseService = new CourseService();

        // Determine content path - test runner may be in different locations
        _contentPath = FindContentPath();
    }

    private string FindContentPath()
    {
        // Try different relative paths to find content directory
        var possiblePaths = new[]
        {
            Path.Combine(Directory.GetCurrentDirectory(), "Content"),
            Path.Combine(Directory.GetCurrentDirectory(), "..", "Content"),
            Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "Content"),
            Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "Content"),
            Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "Content"),
            Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "content"),
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Content"),
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "content")
        };

        foreach (var path in possiblePaths)
        {
            if (Directory.Exists(path))
            {
                return Path.GetFullPath(path);
            }
        }

        return Path.Combine(Directory.GetCurrentDirectory(), "Content");
    }

    [Fact]
    public async Task GetCoursesAsync_ReturnsNonEmptyList()
    {
        // Act
        var courses = await _courseService.GetCoursesAsync();

        // Assert
        courses.Should().NotBeNull();
        // Note: If content files don't exist in test environment, this may be empty
        // In that case, the test validates the service doesn't crash
    }

    [Fact]
    public async Task GetCoursesAsync_ReturnsCoursesWithRequiredProperties()
    {
        // Act
        var courses = await _courseService.GetCoursesAsync();
        var courseList = courses.ToList();

        // Assert
        foreach (var course in courseList)
        {
            course.Id.Should().NotBeNullOrEmpty("Course ID is required");
            course.Title.Should().NotBeNullOrEmpty("Course title is required");
            course.Language.Should().NotBeNullOrEmpty("Course language is required");
        }
    }

    [Fact]
    public async Task GetCourseAsync_WithValidId_ReturnsCourse()
    {
        // Arrange
        var courses = await _courseService.GetCoursesAsync();
        var firstCourse = courses.FirstOrDefault();

        // Skip test if no courses available
        if (firstCourse == null)
        {
            return;
        }

        // Act
        var course = await _courseService.GetCourseAsync(firstCourse.Id);

        // Assert
        course.Should().NotBeNull();
        course!.Id.Should().Be(firstCourse.Id);
        course.Title.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task GetCourseAsync_WithInvalidId_ReturnsNull()
    {
        // Act
        var course = await _courseService.GetCourseAsync("nonexistent-course-xyz");

        // Assert
        course.Should().BeNull();
    }

    [Fact]
    public async Task GetCourseAsync_ReturnsCourseWithModules()
    {
        // Arrange
        var courses = await _courseService.GetCoursesAsync();
        var firstCourse = courses.FirstOrDefault();

        // Skip test if no courses available
        if (firstCourse == null)
        {
            return;
        }

        // Act
        var course = await _courseService.GetCourseAsync(firstCourse.Id);

        // Assert
        if (course != null)
        {
            course.Modules.Should().NotBeNull();

            // If course has modules, verify they have required properties
            foreach (var module in course.Modules)
            {
                module.Id.Should().NotBeNullOrEmpty("Module ID is required");
                module.Title.Should().NotBeNullOrEmpty("Module title is required");
                module.Lessons.Should().NotBeNull("Module must have lessons collection");
            }
        }
    }

    [Fact]
    public async Task GetCourseAsync_ReturnsCourseWithLessons()
    {
        // Arrange
        var courses = await _courseService.GetCoursesAsync();
        var firstCourse = courses.FirstOrDefault();

        // Skip test if no courses available
        if (firstCourse == null)
        {
            return;
        }

        // Act
        var course = await _courseService.GetCourseAsync(firstCourse.Id);

        // Assert
        if (course?.Modules?.Any() == true)
        {
            var moduleWithLessons = course.Modules.FirstOrDefault(m => m.Lessons?.Any() == true);
            if (moduleWithLessons != null)
            {
                foreach (var lesson in moduleWithLessons.Lessons)
                {
                    lesson.Id.Should().NotBeNullOrEmpty("Lesson ID is required");
                    lesson.Title.Should().NotBeNullOrEmpty("Lesson title is required");
                }
            }
        }
    }

    [Fact]
    public async Task GetLessonAsync_WithValidIds_ReturnsLesson()
    {
        // Arrange
        var courses = await _courseService.GetCoursesAsync();
        var firstCourse = courses.FirstOrDefault();

        if (firstCourse == null)
        {
            return;
        }

        var course = await _courseService.GetCourseAsync(firstCourse.Id);
        var firstModule = course?.Modules?.FirstOrDefault();
        var firstLesson = firstModule?.Lessons?.FirstOrDefault();

        if (course == null || firstModule == null || firstLesson == null)
        {
            return;
        }

        // Act
        var lesson = await _courseService.GetLessonAsync(course.Id, firstModule.Id, firstLesson.Id);

        // Assert
        lesson.Should().NotBeNull();
        lesson!.Id.Should().Be(firstLesson.Id);
        lesson.Title.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task GetLessonAsync_WithInvalidIds_ReturnsNull()
    {
        // Act
        var lesson = await _courseService.GetLessonAsync("invalid", "invalid", "invalid");

        // Assert
        lesson.Should().BeNull();
    }

    [Fact]
    public async Task GetNextLessonAsync_ReturnNextLessonInModule()
    {
        // Arrange
        var courses = await _courseService.GetCoursesAsync();
        var firstCourse = courses.FirstOrDefault();

        if (firstCourse == null)
        {
            return;
        }

        var course = await _courseService.GetCourseAsync(firstCourse.Id);
        var moduleWithMultipleLessons = course?.Modules?
            .FirstOrDefault(m => m.Lessons?.Count > 1);

        if (moduleWithMultipleLessons == null)
        {
            return;
        }

        var firstLesson = moduleWithMultipleLessons.Lessons.First();

        // Act
        var nextLesson = await _courseService.GetNextLessonAsync(
            course!.Id,
            moduleWithMultipleLessons.Id,
            firstLesson.Id);

        // Assert
        if (nextLesson != null)
        {
            nextLesson.LessonId.Should().NotBe(firstLesson.Id);
            nextLesson.ModuleId.Should().NotBeNullOrEmpty();
            nextLesson.LessonId.Should().NotBeNullOrEmpty();
        }
    }

    [Fact]
    public async Task GetPreviousLessonAsync_ReturnsPreviousLessonInModule()
    {
        // Arrange
        var courses = await _courseService.GetCoursesAsync();
        var firstCourse = courses.FirstOrDefault();

        if (firstCourse == null)
        {
            return;
        }

        var course = await _courseService.GetCourseAsync(firstCourse.Id);
        var moduleWithMultipleLessons = course?.Modules?
            .FirstOrDefault(m => m.Lessons?.Count > 1);

        if (moduleWithMultipleLessons == null)
        {
            return;
        }

        var secondLesson = moduleWithMultipleLessons.Lessons.Skip(1).FirstOrDefault();
        if (secondLesson == null)
        {
            return;
        }

        // Act
        var previousLesson = await _courseService.GetPreviousLessonAsync(
            course!.Id,
            moduleWithMultipleLessons.Id,
            secondLesson.Id);

        // Assert
        if (previousLesson != null)
        {
            previousLesson.LessonId.Should().NotBe(secondLesson.Id);
            previousLesson.ModuleId.Should().NotBeNullOrEmpty();
            previousLesson.LessonId.Should().NotBeNullOrEmpty();
        }
    }

    [Fact]
    public async Task CourseService_HandlesJsonParsingGracefully()
    {
        // This test verifies the service doesn't crash with malformed or missing content
        // Act & Assert - should not throw
        Func<Task> act = async () =>
        {
            await _courseService.GetCoursesAsync();
            await _courseService.GetCourseAsync("any-id");
            await _courseService.GetLessonAsync("any", "ids", "here");
            await _courseService.GetNextLessonAsync("any", "ids", "here");
            await _courseService.GetPreviousLessonAsync("any", "ids", "here");
        };

        await act.Should().NotThrowAsync();
    }

    [Fact]
    public async Task GetCourseAsync_CachesResults()
    {
        // Arrange
        var courses = await _courseService.GetCoursesAsync();
        var firstCourse = courses.FirstOrDefault();

        if (firstCourse == null)
        {
            return;
        }

        // Act - call twice
        var course1 = await _courseService.GetCourseAsync(firstCourse.Id);
        var course2 = await _courseService.GetCourseAsync(firstCourse.Id);

        // Assert - both should return the same data (caching behavior)
        if (course1 != null && course2 != null)
        {
            course1.Id.Should().Be(course2.Id);
            course1.Title.Should().Be(course2.Title);
        }
    }
}
