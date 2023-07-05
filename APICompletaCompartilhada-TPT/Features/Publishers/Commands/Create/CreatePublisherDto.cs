namespace Univali.Api.Features.Publishers.Commands.CreatePublisher;

public class CreatePublisherDto
{
    public int PublisherId {get; set;}

    public string Name {get; set;} = string.Empty;

    public string CNPJ {get; set;} = string.Empty;
}

