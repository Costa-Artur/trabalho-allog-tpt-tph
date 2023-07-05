using AutoMapper;
using FluentValidation;
using MediatR;
using Univali.Api.Entities;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Publishers.Commands.CreatePublisher;

public class CreatePublisherCommandHandler : 
    IRequestHandler<CreatePublisherCommand, CreatePublisherCommandResponse>
{
    private readonly IPublisherRepository _publisherRepository;

    private readonly IMapper _mapper;

    public readonly IValidator<CreatePublisherCommand> _validator;

    public CreatePublisherCommandHandler(
        IPublisherRepository publisherRepository, 
        IMapper mapper,
        IValidator<CreatePublisherCommand> validator
    ) {
        _publisherRepository = publisherRepository;

        _mapper = mapper;
       
        _validator = validator;
    }

    public async Task<CreatePublisherCommandResponse> Handle(
        CreatePublisherCommand createPublisherCommand, 
        CancellationToken cancellationToken
    ) {
        CreatePublisherCommandResponse createPublisherCommandResponse = new();

        var validationResult = _validator.Validate(createPublisherCommand);

        if (validationResult.IsValid == false) 
        {
            foreach (var error in validationResult.ToDictionary()) 
            {
                createPublisherCommandResponse.Errors
                    .Add(error.Key, error.Value);
            }

            createPublisherCommandResponse.IsSuccessful = false;

            return createPublisherCommandResponse;
        }

        var publisherEntity = _mapper.Map<Publisher>(createPublisherCommand);

        _publisherRepository.AddPublisher(publisherEntity);

        await _publisherRepository.SaveChangesAsync();

        createPublisherCommandResponse.Publisher = _mapper.Map<CreatePublisherDto>(publisherEntity);
    
        return createPublisherCommandResponse;
    }
}

