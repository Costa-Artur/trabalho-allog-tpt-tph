namespace Univali.Api.Models;

public class PublisherDto
{
    public int PublisherId {get; set;}

   	public string Name {get; set;} = string.Empty;

   	public string CNPJ {get; set;} = string.Empty;

	public List<CourseDto> Courses {get; set;} = new();
}
