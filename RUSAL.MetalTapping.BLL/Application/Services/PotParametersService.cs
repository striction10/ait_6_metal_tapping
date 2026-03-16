using RUSAL.MetalTapping.BLL.Domain.Entities;
using RUSAL.MetalTapping.BLL.Domain.Enums;

namespace RUSAL.MetalTapping.BLL.Application.Services
{
    public class PotParametersService
    {
        public double GetParameter(IEnumerable<PotParameters> parameters, PotParametersType type)
        {
            var param = parameters.FirstOrDefault(p => p.Type == type);

            if (param == null)
            {
                throw new Exception($"Pot parameter {type} was not found");
            }

            return param.Value;
        }
    }
}