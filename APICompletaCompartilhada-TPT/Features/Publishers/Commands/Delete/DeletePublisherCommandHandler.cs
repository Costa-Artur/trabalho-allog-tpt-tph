using AutoMapper;
using FluentValidation;
using MediatR;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Publishers.Commands.DeletePublisher;

public class DeletePublisherCommandHandler : 
    IRequestHandler<DeletePublisherCommand, DeletePublisherCommandResponse>
{
    private readonly IPublisherRepository _publisherRepository;

    public readonly IValidator<DeletePublisherCommand> _validator;

    public DeletePublisherCommandHandler(
        IPublisherRepository publisherRepository, 
        IMapper mapper,
        IValidator<DeletePublisherCommand> validator
    ) {
        _publisherRepository = publisherRepository;

        _validator = validator;
    }

    public async Task<DeletePublisherCommandResponse> Handle(
        DeletePublisherCommand deletePublisherCommand, 
        CancellationToken cancellationToken
    ) {
        DeletePublisherCommandResponse deletePublisherCommandResponse = new ();

        var validationResult = _validator.Validate(deletePublisherCommand);

        if (validationResult.IsValid == false) 
        {
            foreach (var error in validationResult.ToDictionary()) 
            {
                deletePublisherCommandResponse.Errors
                    .Add(error.Key, error.Value);
            }

            deletePublisherCommandResponse.IsSuccessful = false;

            return deletePublisherCommandResponse;
        } 

        var publisherFromDatabase = await _publisherRepository
            .GetPublisherByIdAsync(deletePublisherCommand.PublisherId);

        if (publisherFromDatabase == null) 
        { 
            deletePublisherCommandResponse.IsSuccessful = false; 

            return deletePublisherCommandResponse;
        }

        _publisherRepository.RemovePublisher(publisherFromDatabase);

        await _publisherRepository.SaveChangesAsync();

        return deletePublisherCommandResponse;
    }
}
