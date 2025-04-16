using AiPlayground.BusinessLogic.Dtos;

namespace AiPlayground.BusinessLogic.Interfaces;

public interface IModelService
{
    Task<IEnumerable<ModelDto>> GetAllModelsAsync();
    Task<ModelDto?> GetModelByIdAsync(int id);
    Task<ModelDto> CreateModelAsync(ModelCreateDto modelCreateDto);
    Task<ModelDto> UpdateModelAsync(int id, ModelCreateDto modelCreateDto);
    Task DeleteModelAsync(int id);
}