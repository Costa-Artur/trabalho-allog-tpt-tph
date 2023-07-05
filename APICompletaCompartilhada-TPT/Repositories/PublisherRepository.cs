using Microsoft.EntityFrameworkCore;
using Univali.Api.DbContexts;
using Univali.Api.Entities;

namespace Univali.Api.Repositories;

public class PublisherRepository : IPublisherRepository
{
    private readonly PublisherContext _publisherContext;

    public PublisherRepository(PublisherContext authorContext) 
    {
        _publisherContext = authorContext;
    }

    public async Task<bool> SaveChangesAsync() 
    {
        return (await _publisherContext.SaveChangesAsync() > 0);
    }

// Author
    public async Task<Author?> GetAuthorByIdAsync(int authorId)
    {
        return await _publisherContext.Authors
            .FirstOrDefaultAsync(c => c.AuthorId == authorId);
    }

    public void AddAuthor(Author authorEntity) 
    {  
        _publisherContext.Authors.Add(authorEntity);
    }

    public void RemoveAuthor(Author authorEntity)
    {
        _publisherContext.Authors.Remove(authorEntity);
    }

    public async Task<Author?> GetAuthorWithCoursesAsync(int authorId)
    {
        return await _publisherContext.Authors
            .Include(c => c.Courses)
            .FirstOrDefaultAsync(c => c.AuthorId == authorId);
    }

    public async Task<IEnumerable<Author>?> GetAllAuthorsWithCoursesAsync()
    {
        return await _publisherContext.Authors
            .Include(c => c.Courses)
            .ToListAsync();
    }

// Course
    public async Task<Course?> GetCourseByIdAsync(int courseId)
    {
        return await _publisherContext.Courses
            .FirstOrDefaultAsync(c => c.CourseId == courseId);
    }

    public Course? GetCourseById(int courseId)
    {
        return _publisherContext.Courses
            .FirstOrDefault(c => c.CourseId == courseId);
    }

    public void AddCourse(Course courseEntity) 
    {  
        _publisherContext.Courses.Add(courseEntity);
    }

    public void RemoveCourse(Course courseEntity)
    {
        _publisherContext.Courses.Remove(courseEntity);
    }

    public async Task<Course?> GetCourseWithAuthorsAsync(int publisherId, int courseId)
    {
        return await _publisherContext.Courses
            .Include(c => c.Authors)
            .Include(c => c.Publisher)
            .Where(p => p.PublisherId == publisherId)
            .FirstOrDefaultAsync(c => c.CourseId == courseId);
    }

    public async Task<IEnumerable<Course>?> GetAllCoursesWithAuthorsAsync(int publisherId)
    {
        return await _publisherContext.Courses
            .Include(c => c.Authors)
            .Include(c => c.Publisher)
            .Where(p => p.PublisherId == publisherId)
            .ToListAsync();
    }

// aula0307
    public async Task<IEnumerable<Course>> GetCoursesAsync()
    {
        return await _publisherContext.Courses.ToListAsync();
    }

    public async Task<IEnumerable<Course>> GetCoursesAsync(
        string? category, string? searchQuery
    ) {
        if (string.IsNullOrWhiteSpace(category) 
            && string.IsNullOrWhiteSpace(searchQuery))
        {
            return await GetCoursesAsync();
        }

        var collection = _publisherContext.Courses as IQueryable<Course>;

        // Filtro - exado
        if (!string.IsNullOrWhiteSpace(category))
        {
            category = category.Trim();

            collection = collection.Where(c => c.Category == category);
        }

        // Pesquisa - contem
        if (!string.IsNullOrWhiteSpace(searchQuery))
        {
            searchQuery = searchQuery.Trim();

            collection = collection.Where(
                c => c.Category.Contains(searchQuery) 
                || c.Title.Contains(searchQuery)
                || c.Description.Contains(searchQuery)
            );
        }

        return await collection.ToListAsync();
    }

// Publisher

    

    public void AddPublisher(Publisher publisherEntity)
    {
        _publisherContext.Publishers.Add(publisherEntity);
    }

    public void RemovePublisher(Publisher publisherEntity)
    {
        _publisherContext.Publishers.Remove(publisherEntity);
    }

    public async Task<Publisher?> GetPublisherByIdAsync(int publisherId)
    {
        return await _publisherContext.Publishers
            .FirstOrDefaultAsync(p => p.PublisherId == publisherId);
    }

    public async Task<Publisher?> GetPublisherWithCoursesAsync(int publisherId)
    {
        return await _publisherContext.Publishers
            .Include(p => p.Courses)
            .FirstOrDefaultAsync(p => p.PublisherId == publisherId);
    }

    public async Task<IEnumerable<Publisher>?> GetAllPublishersWithCoursesAsync()
    {
        return await _publisherContext.Publishers
            .Include(p => p.Courses)
            .ToListAsync();
    }
}
    
