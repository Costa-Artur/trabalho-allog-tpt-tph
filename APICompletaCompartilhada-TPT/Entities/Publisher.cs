namespace Univali.Api.Entities;

public class Publisher
{
    public int PublisherId {get; set;}

    public string Name {get; set;} = string.Empty;
    
    public List<Course> Courses {get; set;} = new();
}