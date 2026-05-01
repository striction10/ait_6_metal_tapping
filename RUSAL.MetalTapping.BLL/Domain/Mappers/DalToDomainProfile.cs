using AutoMapper;
using RUSAL.MetalTapping.DAL.Entities;
using RUSAL.MetalTapping.BLL.Domain.Enums;
using RUSAL.MetalTapping.BLL.Domain.DTOs;

namespace RUSAL.MetalTapping.DAL.Mappers;

public class DalToDomainProfile : Profile
{
    public DalToDomainProfile()
    {
        CreateMap<BuildingDto, Building>();
        CreateMap<Building, BuildingDto>();

        CreateMap<CalculatedTaskDto, CalculatedTask>();
        CreateMap<CalculatedTask, CalculatedTaskDto>();

        CreateMap<ChemicalElemDto, ChemicalElem>();
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
        CreateMap<MetalMarkAnalysisValueDto, MetalMarkAnalysisValue>();

        CreateMap<MetalMark, MetalMarkDto>();
        CreateMap<MetalMarkDto, MetalMark>();

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
        CreateMap<ShiftTask, ShiftTaskDto>();

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