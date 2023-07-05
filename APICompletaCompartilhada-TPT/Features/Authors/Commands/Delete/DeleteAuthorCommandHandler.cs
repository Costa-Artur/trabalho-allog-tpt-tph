using MediatR;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Authors.Commands;

public class DeleteAuthorCommandHandler : 
    IRequestHandler<DeleteAuthorCommand, bool>
{
    private readonly IPublisherRepository _publisherRepository;

    public DeleteAuthorCommandHandler(
        IPublisherRepository publisherRepository
    ) {
        _publisherRepository = publisherRepository;
    }

    public async Task<bool> Handle(
        DeleteAuthorCommand deleteAuthorCommand, 
        CancellationToken cancellationToken
    ) {
        var authorFromDatabase = await _publisherRepository
            .GetAuthorByIdAsync(deleteAuthorCommand.AuthorId);

        if (authorFromDatabase == null) { return false; }

        _publisherRepository.RemoveAuthor(authorFromDatabase);

        await _publisherRepository.SaveChangesAsync();

        return true;
    }
}
