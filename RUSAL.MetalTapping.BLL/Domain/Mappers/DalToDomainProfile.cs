using AutoMapper;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.BLL.Domain.Enums;
using RUSAL.MetalTapping.DAL.Entities;
using Task = RUSAL.MetalTapping.DAL.Entities.Task;

namespace RUSAL.MetalTapping.BLL.Domain.Mappers;

public class DalToBllProfile : Profile
{
    public DalToBllProfile()
    {
        CreateMap<Building, BuildingDto>();

        CreateMap<CalculatedTask, CalculatedTaskDto>();
        CreateMap<CalculatedTaskDto, CalculatedTask>();

        CreateMap<ChemicalElem, ChemicalElemDto>();

        CreateMap<DeviationDto, Deviation>();
        CreateMap<Deviation, DeviationDto>()
            .ForMember(dest => dest.Values,
                       opt => opt.MapFrom(src => src.DeviationValues));

        CreateMap<DeviationValues, DeviationValuesDto>();

        CreateMap<ExternalData, ExternalDataDto>();

        CreateMap<MetalMarkAnalysis, MetalMarkAnalysisDto>()
            .ForMember(dest => dest.Values,
                       opt => opt.MapFrom(src => src.Values));

        CreateMap<MetalMarkAnalysisValue, MetalMarkAnalysisValueDto>();

        CreateMap<MetalMark, MetalMarkDto>();

        CreateMap<OrderDto, Order>();
        CreateMap<Order, OrderDto>();

        CreateMap<Pot, PotDto>();

        CreateMap<PotParameter, PotParametersDto>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src =>
                Enum.Parse<PotParametersType>(src.Name)));

        CreateMap<PotParametersDto, PotParameter>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Type.ToString()));

        CreateMap<PotReglament, PotReglamentDto>()
            .ForMember(dest => dest.Deviations,
                       opt => opt.MapFrom(src => src.Deviations));

        CreateMap<PotState, PotStateDto>();
        CreateMap<PotStateDto, PotState>();

        CreateMap<Scoop, ScoopDto>();
        CreateMap<ScoopState, ScoopStateDto>();

        CreateMap<Shift, ShiftDto>();
        CreateMap<Task, ShiftTaskDto>();

        CreateMap<TapTask, TapTaskDto>();
        CreateMap<TapTaskPot, TapTaskPotDto>();

        CreateMap<User, UserDto>();
        CreateMap<UserDto, User>();

        CreateMap<UserRoleMembers, UserRoleMembersDto>();
        CreateMap<UserRoleMembersDto, UserRoleMembers>();

        CreateMap<WorkGroup, WorkGroupDto>();
        CreateMap<WorkGroupMembers, WorkGroupMembersDto>();

        CreateMap<Reglament, ReglamentDto>();
        CreateMap<ReglamentDto, Reglament>();

        CreateMap<Role, RoleDto>();
        CreateMap<RoleDto, Role>();

        CreateMap<ScoopUsage, ScoopUsageDto>();
        CreateMap<ScoopUsageDto, ScoopUsage>();

        CreateMap<PotGroupDto, PotGroup>();
        CreateMap<PotGroup, PotGroupDto>();

        CreateMap<PotGroupsHistory, PotGroupsHistoryDto>();
        CreateMap<PotGroupsHistoryDto, PotGroupsHistory>();

        CreateMap<TapTaskDto, TapTask>();
        CreateMap<TapTask, TapTaskDto>();

        CreateMap<TapTaskPotDto, TapTaskPot>();
        CreateMap<TapTaskPot, TapTaskPotDto>();

        CreateMap<ShiftTaskDto, Task>();
        CreateMap<Task, ShiftTaskDto>();
    }
}