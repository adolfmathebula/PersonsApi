using Microsoft.EntityFrameworkCore;
using PersonsAPI.Models;

namespace PersonsAPI.Data;

public class PersonsDbContext : DbContext
{
    public PersonsDbContext(DbContextOptions<PersonsDbContext> options)
        : base(options)
    {
    }

    public DbSet<Person> Persons { get; set; }

    public DbSet<Gender> Genders { get; set; }

    public DbSet<AdultPerson> AdultPersons { get; set; }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>(entity =>
        {
            entity.ToTable("person");

            entity.HasKey(p => p.PersonId);

            entity.Property(p => p.PersonId)
                .HasColumnName("person_id");

            entity.Property(p => p.FirstName)
                .HasColumnName("first_name");

            entity.Property(p => p.LastName)
                .HasColumnName("last_name");

            entity.Property(p => p.DateOfBirth)
                .HasColumnName("date_of_birth");

            entity.Property(p => p.Email)
                .HasColumnName("email");

            entity.Property(p => p.Phone)
                .HasColumnName("phone");

            entity.Property(p => p.GenderId)
                .HasColumnName("gender_id");

            entity.HasOne(p => p.Gender)
                .WithMany(g => g.Persons)
                .HasForeignKey(p => p.GenderId);
        });

        modelBuilder.Entity<Gender>(entity =>
        {
            entity.ToTable("gender");

            entity.HasKey(g => g.GenderId);

            entity.Property(g => g.GenderId)
                .HasColumnName("gender_id");

            entity.Property(g => g.Name)
                .HasColumnName("name");
        });

        modelBuilder.Entity<AdultPerson>(entity =>
        {
            entity.HasNoKey();

            entity.ToView("adult_persons");

            entity.Property(p => p.PersonId)
                .HasColumnName("person_id");

            entity.Property(p => p.FirstName)
                .HasColumnName("first_name");

            entity.Property(p => p.LastName)
                .HasColumnName("last_name");

            entity.Property(p => p.DateOfBirth)
                .HasColumnName("date_of_birth");

            entity.Property(p => p.Email)
                .HasColumnName("email");

            entity.Property(p => p.Phone)
                .HasColumnName("phone");

            entity.Property(p => p.GenderId)
                .HasColumnName("gender_id");

            entity.Property(p => p.Gender)
                .HasColumnName("gender");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("app_user");

            entity.HasKey(u => u.UserId);

            entity.Property(u => u.UserId)
                .HasColumnName("user_id");

            entity.Property(u => u.Username)
                .HasColumnName("username");

            entity.Property(u => u.PasswordHash)
                .HasColumnName("password_hash");

            entity.Property(u => u.Role)
                .HasColumnName("role");

            entity.HasIndex(u => u.Username)
                .IsUnique();
        });
    }
}