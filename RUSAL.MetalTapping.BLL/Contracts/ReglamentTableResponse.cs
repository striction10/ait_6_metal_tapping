using RUSAL.MetalTapping.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RUSAL.MetalTapping.BLL.Contracts
{
    public record ReglamentTableResponse(BuildingDto building, ReglamentDto reglament, List<PotDeviationDto> pots);
}
