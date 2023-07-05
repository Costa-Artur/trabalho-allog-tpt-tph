using Microsoft.EntityFrameworkCore;
using Univali.Api.Entities;

namespace Univali.Api.DbContexts;

public class PublisherContext : DbContext
{
    public DbSet<Publisher> Publishers { get; set; } = null!;
    public DbSet<NaturalPublisher> NaturalPublishers { get; set; } = null!;
    public DbSet<LegalPublisher> LegalPublishers { get; set; } = null!;
    public DbSet<Author> Authors { get; set; } = null!;
    public DbSet<Course> Courses { get; set; } = null!;

    public PublisherContext(DbContextOptions<PublisherContext> options)
    : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var publisher = modelBuilder.Entity<Publisher>();
        var naturalPublisher = modelBuilder.Entity<NaturalPublisher>();
        var legalPublisher = modelBuilder.Entity<LegalPublisher>();


        //discriminator é uma coluna que indica a hierarquia de cada entity

        publisher
            .ToTable("Publishers")
            .HasDiscriminator<int>("PublisherType")
            .HasValue<Publisher>(1)
            .HasValue<NaturalPublisher>(2)
            .HasValue<LegalPublisher>(3);

        //======================================================================================
        //caso duas entidades na mesma hierarquia tenha um atributo repetente,
        //é possível adiciona-las na mesma coluna(tem de ser feito manualmente),
        //não é o nosso caso, pois as subclasses criadas estão herdando os atributos e não os repetem
        //exemplo de como seria feito abaixo:

        // modelBuilder.Entity<NaturalPublisher>()
        //     .Property(p => p.Name)
        //     .HasColumnName("Name");

        // modelBuilder.Entity<LegalPublisher>()
        //     .Property(p => p.Name)
        //     .HasColumnName("Name");
        //======================================================================================

        //======================================================================================
        //também é possível transformar o discriminator em uma propriedade na entity,
        //porém acho que não vamos usar
        //exemplo:

        // publisher
        //     .HasDiscriminator(p=>p.PublisherType);

        // publisher
        // .Property(e => e.PublisherType)
        // .HasMaxLength(200)
        // .HasColumnName("PublisherType");
        //======================================================================================

        publisher
            .HasMany(a => a.Courses)
            .WithOne(c => c.Publisher)
            .HasForeignKey(c => c.PublisherId);

        publisher
            .Property(a => a.Name)
            .HasMaxLength(100)
            .IsRequired();

        naturalPublisher
            .Property(p => p.CPF)
            .HasMaxLength(11)
            .IsRequired();

        legalPublisher
            .Property(p => p.CNPJ)
            .HasMaxLength(14)
            .IsRequired();

        // publisher.HasData(
        //     new Publisher
        //     {
        //         PublisherId = 1,
        //         Name = "Steven Spielberg Production Company",
        //         // CNPJ = "14698277000144",
        //     },
        //     new Publisher
        //     {
        //         PublisherId = 2,
        //         Name = "James Cameron Corporation",
        //         // CNPJ = "12135618000148",
        //     },
        //     new Publisher
        //     {
        //         PublisherId = 3,
        //         Name = "Quentin Tarantino Production",
        //         // CNPJ = "64167199000120",
        //     }
        // );

        legalPublisher
            .HasData(
                new LegalPublisher
                {
                    PublisherId = 1,
                    Name = "LegalPublisher1",
                    CNPJ = "64167199000120"
                },
                new LegalPublisher
                {
                    PublisherId = 2,
                    Name = "LegalPublisher2",
                    CNPJ = "94333690000144"
                }
            );

        naturalPublisher
            .HasData(
                new NaturalPublisher
                {
                    PublisherId = 3,
                    Name = "NaturalPublisher1",
                    CPF = "07382817946"
                },
                new NaturalPublisher
                {
                    PublisherId = 4,
                    Name = "NaturalPublisher2",
                    CPF = "12345678912"
                }
            );
        var author = modelBuilder.Entity<Author>();

        author
            .HasMany(a => a.Courses)
            .WithMany(c => c.Authors)
            .UsingEntity<AuthorCourse>(
                ac => ac.ToTable("PublishersCourses")
               .Property(ac => ac.CreatedOn).HasDefaultValueSql("NOW()")
            );

        author
            .Property(a => a.FirstName)
            .HasMaxLength(100)
            .IsRequired();

        author
            .Property(a => a.LastName)
            .HasMaxLength(100)
            .IsRequired();

        author.HasData(
            new Author
            {
                AuthorId = 1,
                FirstName = "Grace",
                LastName = "Hopper",
            },
            new Author
            {
                AuthorId = 2,
                FirstName = "John",
                LastName = "Backus",
            },
            new Author
            {
                AuthorId = 3,
                FirstName = "Bill",
                LastName = "Gates",
            },
            new Author
            {
                AuthorId = 4,
                FirstName = "Jim",
                LastName = " Berners-Lee",
            },
            new Author
            {
                AuthorId = 5,
                FirstName = "Linus",
                LastName = "Torvalds",
            }
        );

        var course = modelBuilder.Entity<Course>();

        course
            .Property(c => c.Title)
            .HasMaxLength(100)
            .IsRequired();

        course
            .Property(c => c.Price)
            .HasPrecision(5, 2);

        course
            .Property(c => c.Description)
            .IsRequired(false);

        // aula0307
        // Aplicar migration após modificações
        course.HasData(
            new Course
            {
                CourseId = 1,
                Title = "ASP.NET Core Web Api",
                Price = 97.00m,
                Description = "In this course, you'll learn how to build an API with ASP.NET Core that connects to a database via Entity Framework Core from scratch.",
                Category = "Backend",
                PublisherId = 1
            },
            new Course
            {
                CourseId = 2,
                Title = "Entity Framework Fundamentals",
                Price = 197.00m,
                Description = "In this course, Entity Framework Core 6 Fundamentals, you’ll learn to work with data in your .NET applications.",
                Category = "Backend",
                PublisherId = 1
            },
            new Course
            {
                CourseId = 3,
                Title = "Getting Started with Linux",
                Price = 47.00m,
                Description = "You've heard that Linux is the future of enterprise computing and you're looking for a way in.",
                Category = "Operating Systems",
                PublisherId = 2
            }
        );

        var authorCourse = modelBuilder.Entity<AuthorCourse>();

        authorCourse.HasData(
            new AuthorCourse { AuthorId = 1, CourseId = 1 },
            new AuthorCourse { AuthorId = 1, CourseId = 3 },
            new AuthorCourse { AuthorId = 2, CourseId = 1 },
            new AuthorCourse { AuthorId = 2, CourseId = 2 },
            new AuthorCourse { AuthorId = 4, CourseId = 1 },
            new AuthorCourse { AuthorId = 5, CourseId = 3 }
        );

        base.OnModelCreating(modelBuilder);
    }
}