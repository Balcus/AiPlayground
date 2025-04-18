using AiPlayground.BusinessLogic.Dtos;

namespace AiPlayground.BusinessLogic.Interfaces;

public interface IModelService
{
    Task<IEnumerable<ModelDto>> GetAllModelsAsync();
    Task<ModelDto?> GetModelByIdAsync(int id);
}