using AutoMapper;
using FluentValidation;
using MediatR;
using Univali.Api.Entities;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Authors.Commands.CreateAuthor;

public class CreateAuthorCommandHandler : 
    IRequestHandler<CreateAuthorCommand, CreateAuthorCommandResponse>
{
    private readonly IPublisherRepository _publisherRepository;

    private readonly IMapper _mapper;

    private readonly IValidator<CreateAuthorCommand> _validator;

    public CreateAuthorCommandHandler(
        IPublisherRepository publisherRepository, 
        IMapper mapper,
        IValidator<CreateAuthorCommand> validator
    ) {
        _publisherRepository = publisherRepository;

        _mapper = mapper;

        _validator = validator;
    }

    public async Task<CreateAuthorCommandResponse> Handle(
        CreateAuthorCommand request, 
        CancellationToken cancellationToken
    ) {

        CreateAuthorCommandResponse createAuthorCommandResponse = new();

        var validationResult = _validator.Validate(request);

        if(validationResult.IsValid == false)
        {
            foreach (var error in validationResult.ToDictionary())
            {
                createAuthorCommandResponse.Errors.Add(error.Key, error.Value);
            }

            createAuthorCommandResponse.IsSuccessful = false;

            return createAuthorCommandResponse;
        }

        var authorEntity = _mapper.Map<Author>(request);

        _publisherRepository.AddAuthor(authorEntity);

        await _publisherRepository.SaveChangesAsync();

        createAuthorCommandResponse.Author = _mapper.Map<CreateAuthorDto>(authorEntity);
        
        return createAuthorCommandResponse;
    }
}

