using AutoMapper;
using FluentValidation;
using MediatR;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Authors.Commands.UpdateAuthor;

public class UpdateAuthorCommandHandler : 
    IRequestHandler<UpdateAuthorCommand, UpdateAuthorCommandResponse>
{
    private readonly IPublisherRepository _publisherRepository;

    private readonly IMapper _mapper;

    private readonly IValidator<UpdateAuthorCommand> _validator;

    public UpdateAuthorCommandHandler(
        IPublisherRepository publisherRepository, 
        IMapper mapper,
        IValidator<UpdateAuthorCommand> validator
    ) {
        _publisherRepository = publisherRepository;

        _mapper = mapper;

        _validator = validator;
    }

    public async Task<UpdateAuthorCommandResponse> Handle(
        UpdateAuthorCommand request, 
        CancellationToken cancellationToken
    ) {
        UpdateAuthorCommandResponse updateAuthorCommandResponse = new();

        var validationResult = _validator.Validate(request);

        if(!validationResult.IsValid)
        {
            foreach (var error in validationResult.ToDictionary())
            {
                updateAuthorCommandResponse.Errors.Add(error.Key, error.Value);
            }

            updateAuthorCommandResponse.IsSuccessful = false;
            return updateAuthorCommandResponse;
        }

        var authorFromDatabase = await _publisherRepository
            .GetAuthorByIdAsync(request.AuthorId);
            
        _mapper.Map(request, authorFromDatabase);

        await _publisherRepository.SaveChangesAsync();

        return updateAuthorCommandResponse;
    }
}
