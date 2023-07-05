namespace Univali.Api.Models;

public class CourseWithoutAuthorDto
{
    public int CourseId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }
}
