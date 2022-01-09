using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using OSS.Domain.Entities;
using System.Reflection;
using System.Security.Claims;

namespace OSS.App.Data;

public class AppIdentityDbContext : IdentityDbContext<ApplicationUser>
{
    public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options)
        : base(options)
    {
    }

    public DbSet<ShiftType> ShiftTypes { get; set; }
    public DbSet<ShiftRole> ShiftRoles { get; set; }
    public DbSet<ShiftSkill> ShiftSkills { get; set; }
    public DbSet<EmployeeShiftSkill> EmployeeShiftSkills { get; set; }
    public DbSet<Gender> Genders { get; set; }
    public DbSet<ShiftGroup> ShiftGroups { get; set; }
    public DbSet<Shift> Shifts { get; set; }
    public DbSet<ShiftParticipation> ShiftParticipations { get; set; }
    public DbSet<ShiftParticipationType> ShiftParticipationTypes { get; set; }
    public DbSet<ShiftCycleItem> ShiftCycleItems { get; set; }
    public DbSet<LeaveRequest> LeaveRequests { get; set; }
    public DbSet<LeaveRequestComment> LeaveRequestComments { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        foreach (var property in builder.Model.GetEntityTypes()
                 .SelectMany(t => t.GetProperties())
                 .Where(p => p.ClrType == typeof(DateTime) || p.ClrType == typeof(DateTime?)))
            property.SetColumnType("timestamp without time zone");
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        // https://stackoverflow.com/questions/56799520/aspnetcore-2-1-identitydbcontext-how-to-get-current-username
        var httpContextAccessor = this.GetService<IHttpContextAccessor>();
        var userId = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedById = userId;
                    entry.Entity.Created = DateTime.Now;
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModifiedById = userId;
                    entry.Entity.LastModified = DateTime.Now;
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
