using AutoMapper;
using RUSAL.MetalTapping.BLL.Domain.Entities;
using RUSAL.MetalTapping.BLL.Domain.Enums;
using RUSAL.MetalTapping.DAL.Models;

namespace RUSAL.MetalTapping.DAL.Mappers
{
    public class DalToDomainProfile : Profile
    {
        public DalToDomainProfile()
        {
            CreateMap<BuildingModel, Building>();

            CreateMap<CalculatedTaskModel, CalculatedTask>();
            CreateMap<CalculatedTask, CalculatedTaskModel>();

            CreateMap<ChemicalElemModel, ChemicalElem>();

            CreateMap<Deviation, DeviationModel>();
            CreateMap<DeviationModel, Deviation>()
                .ForMember(dest => dest.Values,
                           opt => opt.MapFrom(src => src.DeviationValues));

            CreateMap<DeviationValuesModel, DeviationValues>();

            CreateMap<ExternalDataModel, ExternalData>();

            CreateMap<MetalMarkAnalysisModel, MetalMarkAnalysis>()
                .ForMember(dest => dest.Values,
                           opt => opt.MapFrom(src => src.Values));

            CreateMap<MetalMarkAnalysisValueModel, MetalMarkAnalysisValue>();

            CreateMap<MetalMarkModel, MetalMark>();

            CreateMap<Order, OrderModel>();
            CreateMap<OrderModel, Order>();

            CreateMap<PotModel, Pot>();

            CreateMap<PotParameterModel, PotParameters>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src =>
                    Enum.Parse<PotParametersType>(src.Name)));

            CreateMap<PotParameters, PotParameterModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Type.ToString()));

            CreateMap<PotReglamentModel, PotReglament>()
                .ForMember(dest => dest.Deviations,
                           opt => opt.MapFrom(src => src.Deviations));

            CreateMap<PotStateModel, PotState>();
            CreateMap<PotState, PotStateModel>();

            CreateMap<ScoopModel, Scoop>();
            CreateMap<ScoopStateModel, ScoopState>();

            CreateMap<ShiftModel, Shift>();
            CreateMap<TaskModel, ShiftTask>();

            CreateMap<TapTaskModel, TapTask>();
            CreateMap<TapTaskPotModel, TapTaskPot>();

            CreateMap<UserModel, User>();
            CreateMap<User, UserModel>();

            CreateMap<UserRoleMembersModel, UserRoleMembers>();
            CreateMap<UserRoleMembers, UserRoleMembersModel>();

            CreateMap<WorkGroupModel, WorkGroup>();
            CreateMap<WorkGroupMembersModel, WorkGroupMembers>();

            CreateMap<ReglamentModel, Reglament>();
            CreateMap<Reglament, ReglamentModel>();

            CreateMap<RoleModel, Role>();
            CreateMap<Role, RoleModel>();

            CreateMap<ScoopUsageModel, ScoopUsage>();
            CreateMap<ScoopUsage, ScoopUsageModel>();

            CreateMap<PotGroup, PotGroupModel>();
            CreateMap<PotGroupModel, PotGroup>();

            CreateMap<PotGroupsHistoryModel, PotGroupsHistory>();
            CreateMap<PotGroupsHistory, PotGroupsHistoryModel>();

            CreateMap<TapTask, TapTaskModel>();
            CreateMap<TapTaskModel, TapTask>();

            CreateMap<TapTaskPot, TapTaskPotModel>();
            CreateMap<TapTaskPotModel, TapTaskPot>();

            CreateMap<ShiftTask, TaskModel>();
            CreateMap<TaskModel, ShiftTask>();
        }
    }
}