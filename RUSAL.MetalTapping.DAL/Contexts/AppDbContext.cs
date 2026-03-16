using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.DAL.Models;

namespace RUSAL.MetalTapping.DAL.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options){}
        public DbSet<BuildingModel> Buildings { get; set; }
        public DbSet<ChemicalElemModel> ChemicalElems { get; set; }
        public DbSet<DeviationModel> Deviations { get; set; }
        public DbSet<DeviationValuesModel> DeviationValues { get; set; }
        public DbSet<ExternalDataModel> ExternalDatas { get; set; }
        public DbSet<MetalMarkModel> MetalMarks { get; set; }
        public DbSet<MetalMarkAnalysisModel> MetalMarkAnalyses { get; set; }
        public DbSet<MetalMarkAnalysisValueModel> MetalMarkAnalysisValues { get; set; }
        public DbSet<OrderModel> Orders { get; set; }
        public DbSet<PotModel> Pots { get; set; }
        public DbSet<PotParameterModel> PotParameters { get; set; }
        public DbSet<PotParametersGroupModel> PotParametersGroups { get; set; }
        public DbSet<PotReglamentModel> PotReglaments { get; set; }
        public DbSet<PotStateModel> PotStates { get; set; }
        public DbSet<ReglamentModel> Reglaments { get; set; }
        public DbSet<RoleModel> Roles { get; set; }
        public DbSet<ScoopModel> Scoops { get; set; }
        public DbSet<ScoopStateModel> ScoopStates { get; set; }
        public DbSet<ShiftModel> Shifts { get; set; }
        public DbSet<TapTaskModel> TapTasks { get; set; }
        public DbSet<TapTaskPotModel> TapTaskPots { get; set; }
        public DbSet<ShiftTaskModel> Tasks { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<UserRoleMembersModel> UserRoleMembers { get; set; }
        public DbSet<WorkGroupModel> WorkGroups { get; set; }
        public DbSet<WorkGroupMembersModel> WorkGroupMembers { get; set; }
        public DbSet<CalculatedTaskModel> CalculatedTasks { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}