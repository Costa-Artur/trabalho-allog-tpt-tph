// dotnet add package Microsoft.AspNetCore.Authentication
// dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
// dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection --version 12.0.1
// dotnet add package Microsoft.AspNetCore.JsonPatch --version 7.0.5
// dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL --version 7.0.4
// dotnet add package Microsoft.EntityFrameworkCore.Tools --version 7.0.4
// dotnet add package Microsoft.Extensions.Logging --version 7.0.0
// dotnet ef --version
// dotnet tool install --global dotnet-ef
// dotnet ef migrations add InitialMigration
// dotnet ef database update
// dotnet add package MediatR --version 12.0.1

// dotnet ef migrations add AddPublisherContext -o Migrations/Publisher --context PublisherContext
// dotnet ef migrations add AddCustomerContext -o Migrations/Customer --context CustomerContext
// dotnet ef database update --context PublisherContext
// dotnet ef database update --context CustomerContext

//  dotnet add package FluentValidation.DependencyInjectionExtensions --version 11.5.2
//  dotnet add package FluentValidation --version 11.5.2

using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Univali.Api.Configuration;
using Univali.Api.DbContexts;
using Univali.Api.Extensions;
using Univali.Api.Features.Addresses.Commands.CreateAddress;
using Univali.Api.Features.Addresses.Commands.UpdateAddresses;
using Univali.Api.Features.Addresses.Commands.UpdateCustomerWithAddress;
using Univali.Api.Features.Authors.Commands.CreateAuthor;
using Univali.Api.Features.Authors.Commands.UpdateAuthor;
using Univali.Api.Features.Courses.Commands;
using Univali.Api.Features.Courses.Commands.CreateCourse;
using Univali.Api.Features.Courses.Commands.UpdateCourse;
using Univali.Api.Features.Customers.Commands.CreateCustomer;
using Univali.Api.Features.Customers.Commands.CreateCustomerWithAddress;
using Univali.Api.Features.Customers.Commands.PartiallyUpdateCustomer;
using Univali.Api.Features.Customers.Commands.UpdateCustomer;
using Univali.Api.Features.Customers.Commands.UpdateCustomerWithAddress;
using Univali.Api.Features.Customers.Queries.GetCustomerDetail;
using Univali.Api.Features.Publishers.Commands.CreatePublisher;
using Univali.Api.Features.Publishers.Commands.DeletePublisher;
using Univali.Api.Features.Publishers.Commands.UpdatePublisher;
using Univali.Api.Features.Publishers.Queries.GetPublisher;
using Univali.Api.Repositories;
using Univali.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options => {
   options.ListenLocalhost(5000);
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSingleton<TokenService>();

builder.Services.AddScoped<CustomerRepository>();//NÃO TIRAR ISSO: importante para createCourse

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

builder.Services.AddScoped<IPublisherRepository, PublisherRepository>();

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped< IValidator<CreateCustomerCommand>, CreateCustomerCommandValidator>();

builder.Services.AddScoped< IValidator<UpdateCustomerCommand>, UpdateCustomerCommandValidator>();

builder.Services.AddScoped< IValidator<GetCustomerWithAddressByIdQuery>, GetCustomerWithAddressByIdQueryValidator>();

builder.Services.AddScoped< IValidator<CreatePublisherCommand>, CreatePublisherCommandValidator>();

builder.Services.AddScoped< IValidator<DeletePublisherCommand>, DeletePublisherCommandValidator>();

builder.Services.AddScoped< IValidator<UpdatePublisherCommand>, UpdatePublisherCommandValidator>();

builder.Services.AddScoped< IValidator<GetPublisherWithCoursesQuery>, GetPublisherWithCoursesQueryValidator>();

builder.Services.AddScoped< IValidator<CreateAddressCommand>, CreateAddressCommandValidator>();

builder.Services.AddScoped< IValidator<UpdateAddressCommand>, UpdateAddressCommandValidator>();

builder.Services.AddScoped< IValidator<CreateCustomerWithAddressCommand>, CreateCustomerWithAddressCommandValidator>();

builder.Services.AddScoped< IValidator<CreateCourseCommand>, CreateCourseCommandValidator>();

builder.Services.AddScoped< IValidator<UpdateCourseCommand>, UpdateCourseCommandValidator>();

builder.Services.AddScoped< IValidator<UpdateCustomerWithAddressCommand>, UpdateCustomerWithAddressCommandValidator>();

builder.Services.AddScoped<IValidator<PartiallyUpdateCustomerDto>, PartiallyUpdateCustomerCommandValidator>();

builder.Services.AddScoped<IValidator<CreateAuthorCommand>, CreateAuthorCommandValidator>();

builder.Services.AddScoped<IValidator<UpdateAuthorCommand>, UpdateAuthorCommandValidator>();

builder.Services.AddScoped< IValidator<DeleteCourseCommand>, DeleteCourseCommandValidator>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());

builder.Services.AddAuthentication("Bearer").AddJwtBearer( options => {
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = 
        new TokenValidationParameters {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Authentication:SecretKey"]!)
            ),
            ValidateIssuer = false,
            ValidateAudience = false
        };
});

builder.Services.AddDbContext<PublisherContext>(options => 
{
    options
    .UseNpgsql("Host=localhost;Database=Univali;Username=postgres;Password=Quintalmagico12#");
}
);

builder.Services.AddDbContext<CustomerContext>(options => 
{
    options
    .UseNpgsql("Host=localhost;Database=Univali;Username=postgres;Password=Quintalmagico12#");
}
);

builder.Services.AddCors();

builder.Services.AddControllers(options =>{
    options.InputFormatters.Insert(0, MyJPIF.GetJsonPatchInputFormatter());
}).ConfigureApiBehaviorOptions(setupAction => {
    setupAction.SuppressModelStateInvalidFilter = true;
    setupAction.InvalidModelStateResponseFactory = context => {
        var problemDetailsFactory = context.HttpContext.RequestServices
            .GetRequiredService<ProblemDetailsFactory>();

        var validationProblemDetails = problemDetailsFactory
            .CreateValidationProblemDetails(
                context.HttpContext,
                context.ModelState);

        validationProblemDetails.Detail =
            "See the errors field for details.";
        validationProblemDetails.Instance =
            context.HttpContext.Request.Path;

        validationProblemDetails.Type =
            "https://courseunivali.com/modelvalidationproblem";
        validationProblemDetails.Status =
            StatusCodes.Status422UnprocessableEntity;
        validationProblemDetails.Title =
            "One or more validation errors occurred.";

        return new UnprocessableEntityObjectResult(validationProblemDetails) { 
            ContentTypes = {"application/problem+json"} 
        };
    };
});

//opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(opt => opt
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

await app.ResetDatabaseAsync();

app.Run();
