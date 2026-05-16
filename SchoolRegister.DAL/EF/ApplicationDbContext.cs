using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolRegister.Model.DataModels;

namespace SchoolRegister.DAL.EF;

public class ApplicationDbContext : IdentityDbContext<User, Role, int>
{
    public virtual DbSet<Grade> Grades { get; set; }
    public virtual DbSet<Group> Groups { get; set; }
    public virtual DbSet<Subject> Subjects { get; set; }
    public virtual DbSet<SubjectGroup> SubjectGroups { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseLazyLoadingProxies();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .ToTable("AspNetUsers")
            .HasDiscriminator<int>("UserType")
            .HasValue<User>((int)RoleValue.User)
            .HasValue<Student>((int)RoleValue.Student)
            .HasValue<Parent>((int)RoleValue.Parent)
            .HasValue<Teacher>((int)RoleValue.Teacher);

        modelBuilder.Entity<SubjectGroup>()
            .HasKey(sg => new { sg.GroupId, sg.SubjectId });

        modelBuilder.Entity<SubjectGroup>()
            .HasOne(sg => sg.Group)
            .WithMany(g => g.SubjectGroups)
            .HasForeignKey(sg => sg.GroupId);

        modelBuilder.Entity<SubjectGroup>()
            .HasOne(sg => sg.Subject)
            .WithMany(s => s.SubjectGroups)
            .HasForeignKey(sg => sg.SubjectId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Grade>()
            .HasKey(g => new { g.StudentId, g.SubjectId, g.DateOfIssue });
    }
}
