namespace Univali.Api.Models;

public class PublisherWithoutCoursesDto
{
    public int PublisherId {get; set;}

   	public string Name {get; set;} = string.Empty;

   	public string CNPJ {get; set;} = string.Empty;
}
