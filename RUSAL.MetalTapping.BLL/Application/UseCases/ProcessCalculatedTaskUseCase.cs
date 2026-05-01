using RUSAL.MetalTapping.BLL.Application.Contracts;
using RUSAL.MetalTapping.BLL.Application.Services;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
namespace RUSAL.MetalTapping.BLL.Application.UseCases;

public class ProcessCalculatedTaskUseCase(CalculatedTaskService calculatedTaskService)
{
    private readonly CalculatedTaskService _calculatedTaskService = calculatedTaskService;

    /// <summary>
    /// Запись значения расчетного задания для электролизёра
    /// </summary>
    /// <param name="model"> Данные для создания расчётного задания </param>
    public async Task ExecuteAsync(ProcessCalculatedTaskRequest model)
    {
        var existingTask = await _calculatedTaskService.GetByPotIdAsync(model.potId);

        if (existingTask == null)
        {
            var newTask = new CalculatedTaskDto
            {
                Id = Guid.NewGuid(),
                PotId = model.potId,
                CalculatedTaskForPot = model.calculatedTask,
                CreatedAt = DateTime.UtcNow
            };

            await _calculatedTaskService.CreateAsync(newTask);

            return;
        }

        existingTask.CalculatedTaskForPot = model.calculatedTask;
        existingTask.CreatedAt = DateTime.UtcNow;

        await _calculatedTaskService.UpdateAsync(existingTask);
    }
}