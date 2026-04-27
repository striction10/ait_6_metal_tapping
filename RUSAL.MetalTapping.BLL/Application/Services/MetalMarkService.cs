using RUSAL.MetalTapping.BLL.Domain.DTOs;

namespace RUSAL.MetalTapping.BLL.Application.Services;

public class MetalMarkService
{
    /// <summary>
    /// Получить имя марки металла
    /// </summary>
    /// <param name="metalMark"> Марка металла </param>
    /// <returns> Имя марки металла, иначе стандартное значение для отображения на клиенте </returns>
    public string ResolveMetalMarkName(MetalMarkDto? metalMark)
        => metalMark?.Name ?? "N/A";
}