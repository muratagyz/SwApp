using System.Reflection;
using Microsoft.EntityFrameworkCore;
using SwAppData.Entity;

namespace SwAppData.EntityFramework;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<About> Abouts { get; set; }
    public DbSet<AboutOurWho> AboutOurWhos { get; set; }
    public DbSet<AboutSubTable> AboutSubTables { get; set; }
    public DbSet<ContactInfo> ContactInfos { get; set; }
    public DbSet<HomeProjectWork> HomeProjectWorks { get; set; }
    public DbSet<Log> Logs { get; set; }
    public DbSet<HomeSlider> HomeSliders { get; set; }
    public DbSet<HomeWhySorsware> HomeWhySorswares { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Personal> Personals { get; set; }
    public DbSet<ContactInformation> ContactInformations { get; set; }
    public DbSet<HomeWhatAreWeDoing> HomeWhatAreWeDoings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}