using RUSAL.MetalTapping.BLL.Application.Contracts;
using RUSAL.MetalTapping.BLL.Domain.Entities;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;
namespace RUSAL.MetalTapping.BLL.Application.UseCases;

public class ProcessCalculatedTaskUseCase(ICalculatedTaskRepository calculatedTaskRepository)
{
    private readonly ICalculatedTaskRepository _calculatedTaskRepository = calculatedTaskRepository;

    /// <summary>
    /// Запись значения расчетного задания для электролизёра
    /// </summary>
    /// <param name="model"> Данные для создания расчётного задания </param>
    public async Task ExecuteAsync(ProcessCalculatedTaskRequest model)
    {
        var existingTask = await _calculatedTaskRepository.GetCalculatedTaskWithPotIdAsync(model.potId);

        if (existingTask == null)
        {
            var newTask = new CalculatedTask
            {
                Id = Guid.NewGuid(),
                PotId = model.potId,
                CalculatedTaskForPot = model.calculatedTask,
                CreatedAt = DateTime.UtcNow
            };

            await _calculatedTaskRepository.CreateAsync(newTask);

            return;
        }

        existingTask.CalculatedTaskForPot = model.calculatedTask;
        existingTask.CreatedAt = DateTime.UtcNow;

        await _calculatedTaskRepository.UpdateAsync(existingTask);
    }
}