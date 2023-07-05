using Univali.Api.Entities;

namespace Univali.Api.Repositories;

public interface IPublisherRepository 
{
    Task<bool> SaveChangesAsync();

// Author   
    void AddAuthor(Author AuthorEntity);

    void RemoveAuthor(Author AuthorEntity);

    Task<Author?> GetAuthorByIdAsync(int authorId);
    
    Task<Author?> GetAuthorWithCoursesAsync(int authorId);

    Task<IEnumerable<Author>?> GetAllAuthorsWithCoursesAsync();

// Course    
    void AddCourse(Course courseEntity);

    void RemoveCourse(Course courseEntity);

    Task<Course?> GetCourseByIdAsync(int courseId);

    Course? GetCourseById(int courseId);

    Task<Course?> GetCourseWithAuthorsAsync(int publisherId, int courseId);

    Task<IEnumerable<Course>?> GetAllCoursesWithAuthorsAsync(int publisherId);

// aula0307
    Task<IEnumerable<Course>> GetCoursesAsync(string? category, string? searchQuery);

    Task<IEnumerable<Course>> GetCoursesAsync();

// Publisher    
    void AddPublisher(Publisher publisherEntity);

    void RemovePublisher(Publisher publisherEntity);

    Task<Publisher?> GetPublisherByIdAsync(int publisherId);

    Task<Publisher?> GetPublisherWithCoursesAsync(int publisherId);

    Task<IEnumerable<Publisher>?> GetAllPublishersWithCoursesAsync();
}