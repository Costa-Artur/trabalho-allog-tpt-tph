namespace Univali.Api.Entities;

public class Course 
{
    public int CourseId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public List<Author> Authors {get; set;} = new();

    public int PublisherId {get; set;}

    public Publisher? Publisher {get; set;}

// aula0307
    public string Category {get; set;} = string.Empty;
}
