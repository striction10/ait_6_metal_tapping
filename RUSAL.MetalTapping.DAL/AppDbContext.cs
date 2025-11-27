using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.DAL.Entities;

namespace RUSAL.MetalTapping.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options){}
        public DbSet<Building> buildings { get; set; }
        public DbSet<ChemicalElem> chemicalElems { get; set; }
        public DbSet<Deviation> deviations { get; set; }
        public DbSet<DeviationValues> deviationValues { get; set; }
        public DbSet<ExternalData> externalDatas { get; set; }
        public DbSet<MetalMark> metalMarks { get; set; }
        public DbSet<MetalMarkAnalysis> metalMarkAnalyses { get; set; }
        public DbSet<MetalMarkAnalysisValue> metalMarkAnalysisValues { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<Pot> pots { get; set; }
        public DbSet<PotParameters> potParameters { get; set; }
        public DbSet<PotReglament> potReglaments { get; set; }
        public DbSet<PotState> potStates { get; set; }
        public DbSet<Reglament> reglaments { get; set; }
        public DbSet<Role> roles { get; set; }
        public DbSet<Scoop> scoops { get; set; }
        public DbSet<ScoopState> scoopStates { get; set; }
        public DbSet<Shift> shifts { get; set; }
        public DbSet<TapTask> tapTasks { get; set; }
        public DbSet<TapTaskPot> tapTaskPots { get; set; }
        public DbSet<ShiftTask> tasks { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<UserRoleMembers> UserRoleMembers { get; set; }
        public DbSet<WorkGroup> workGroups { get; set; }
        public DbSet<WorkGroupMembers> workGroupMembers { get; set; }
    }
}
