using RUSAL.MetalTapping.BLL.Application.Contracts;
using RUSAL.MetalTapping.BLL.Application.Services;
using RUSAL.MetalTapping.BLL.Domain.DTOs;

namespace RUSAL.MetalTapping.BLL.Application.UseCases;

/// <summary>
/// Оркестратор сохранения значения округлённого расчётного задания (ЗПР) для электролизёра.
/// </summary>
/// <param name="calculatedTaskService">Сервис для работы с расчётными заданиями.</param>
public class ProcessRoundTaskUseCase(CalculatedTaskService calculatedTaskRepository)
{
    /// <summary>
    /// Создание расчетного задания для конкретных параметров электролизёра.
    /// </summary>
    /// <param name="model"> Данные для создания расчётного задания. </param>
    public async Task ExecuteAsync(ProcessRoundTaskRequest model)
    {
        var existingTask = await calculatedTaskRepository.GetByPotIdAsync(model.potId);

        if (existingTask == null)
        {
            var newTask = new CalculatedTaskDto
            {
                Id = Guid.NewGuid(),
                PotId = model.potId,
                RoundCalculatedTaskForPot = model.roundTask,
                CreatedAt = DateTime.UtcNow,
            };

            await calculatedTaskRepository.CreateAsync(newTask);

            return;
        }

        existingTask.RoundCalculatedTaskForPot = model.roundTask;
        existingTask.CreatedAt = DateTime.UtcNow;

        await calculatedTaskRepository.UpdateAsync(existingTask);
    }
}
