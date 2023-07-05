using AutoMapper;
using FluentValidation;
using MediatR;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Publishers.Commands.UpdatePublisher;

public class UpdatePublisherCommandHandler : 
    IRequestHandler<UpdatePublisherCommand, UpdatePublisherCommandResponse>
{
    private readonly IPublisherRepository _publisherRepository;

    private readonly IMapper _mapper;

    public readonly IValidator<UpdatePublisherCommand> _validator;

    public UpdatePublisherCommandHandler(
        IPublisherRepository publisherRepository, 
        IMapper mapper,
        IValidator<UpdatePublisherCommand> validator
    ) {
        _publisherRepository = publisherRepository;

        _mapper = mapper;

        _validator = validator;
    }

    public async Task<UpdatePublisherCommandResponse> Handle(
        UpdatePublisherCommand updatePublisherCommand, 
        CancellationToken cancellationToken
    ) {
        UpdatePublisherCommandResponse updatePublisherCommandResponse = new ();

        var validationResult = _validator.Validate(updatePublisherCommand);

        if (validationResult.IsValid == false) 
        {
            foreach (var error in validationResult.ToDictionary()) 
            {
                updatePublisherCommandResponse.Errors
                    .Add(error.Key, error.Value);
            }

            updatePublisherCommandResponse.IsSuccessful = false;

            return updatePublisherCommandResponse;
        } 

        var publisherFromDatabase = await _publisherRepository
            .GetPublisherByIdAsync(updatePublisherCommand.PublisherId);

        if (publisherFromDatabase == null) 
        { 
            updatePublisherCommandResponse.IsSuccessful = false; 

            return updatePublisherCommandResponse;
        }
            
        _mapper.Map(updatePublisherCommand, publisherFromDatabase);

        await _publisherRepository.SaveChangesAsync();

        return updatePublisherCommandResponse;
    }
}
