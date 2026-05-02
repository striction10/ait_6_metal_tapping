using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.DAL.Entities;

namespace RUSAL.MetalTapping.DAL.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options){}
    public DbSet<Building> Buildings { get; set; }
    public DbSet<ChemicalElem> ChemicalElems { get; set; }
    public DbSet<Deviation> Deviations { get; set; }
    public DbSet<DeviationValues> DeviationValues { get; set; }
    public DbSet<ExternalData> ExternalDatas { get; set; }
    public DbSet<MetalMark> MetalMarks { get; set; }
    public DbSet<MetalMarkAnalysis> MetalMarkAnalyses { get; set; }
    public DbSet<MetalMarkAnalysisValue> MetalMarkAnalysisValues { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Pot> Pots { get; set; }
    public DbSet<PotParameter> PotParameters { get; set; }
    public DbSet<PotParametersGroup> PotParametersGroups { get; set; }
    public DbSet<PotReglament> PotReglaments { get; set; }
    public DbSet<PotState> PotStates { get; set; }
    public DbSet<Reglament> Reglaments { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Scoop> Scoops { get; set; }
    public DbSet<ScoopState> ScoopStates { get; set; }
    public DbSet<Shift> Shifts { get; set; }
    public DbSet<TapTask> TapTasks { get; set; }
    public DbSet<TapTaskPot> TapTaskPots { get; set; }
    public DbSet<ShiftTask> Tasks { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserRoleMembers> UserRoleMembers { get; set; }
    public DbSet<WorkGroup> WorkGroups { get; set; }
    public DbSet<WorkGroupMembers> WorkGroupMembers { get; set; }
    public DbSet<CalculatedTask> CalculatedTasks { get; set; }
    public DbSet<ScoopUsage> ScoopUsages { get; set; }
    public DbSet<PotGroup> PotGroupModels { get; set; }
    public DbSet<PotGroupsHistory> PotGroupsHistoryModels { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
