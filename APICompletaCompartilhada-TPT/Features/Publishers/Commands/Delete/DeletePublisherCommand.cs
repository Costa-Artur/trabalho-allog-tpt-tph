using MediatR;

namespace Univali.Api.Features.Publishers.Commands.DeletePublisher;

public class DeletePublisherCommand : IRequest<DeletePublisherCommandResponse>
{
    public int PublisherId { get; set; }
}

