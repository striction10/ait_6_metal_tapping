using AutoMapper;
using RUSAL.MetalTapping.BLL.DTOs;
using RUSAL.MetalTapping.DAL.Models;

namespace RUSAL.MetalTapping.BLL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<Building, BuildingDto>().ReverseMap();
            CreateMap<ChemicalElem, ChemicalElemDto>().ReverseMap();
            CreateMap<Deviation, DeviationDto>().ReverseMap();
            CreateMap<DeviationValues, DeviationValuesDto>().ReverseMap();
            CreateMap<ExternalData, ExternalDataDto>().ReverseMap();
            CreateMap<MetalMark, MetalMarkDto>().ReverseMap();
            CreateMap<MetalMarkAnalysis, MetalMarkAnalysisDto>().ReverseMap();
            CreateMap<MetalMarkAnalysisValue, MetalMarkAnalysisValueDto>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<Pot, PotDto>().ReverseMap();
            CreateMap<PotParameter, PotParametersDto>().ReverseMap();
            CreateMap<PotReglament, PotReglamentDto>().ReverseMap();
            CreateMap<PotState, PotStateDto>().ReverseMap();
            CreateMap<Reglament, ReglamentDto>().ReverseMap();
            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<Scoop, ScoopDto>().ReverseMap();
            CreateMap<ScoopState, ScoopStateDto>().ReverseMap();
            CreateMap<Shift, ShiftDto>().ReverseMap();
            CreateMap<ShiftTask, ShiftTaskDto>().ReverseMap();
            CreateMap<TapTask, TapTaskDto>().ReverseMap();
            CreateMap<TapTaskPot, TapTaskPotDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<UserRoleMembers, UserRoleMembersDto>().ReverseMap();
            CreateMap<WorkGroup, WorkGroupDto>().ReverseMap();
            CreateMap<WorkGroupMembers, WorkGroupMembersDto>().ReverseMap();
            CreateMap<CalculatedTask, CalculatedTaskDto>().ReverseMap();
        }
    }
}