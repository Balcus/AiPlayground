using AiPlayground.BusinessLogic.Dtos;
using AiPlayground.BusinessLogic.Interfaces;
using AiPlayground.DataAccess.Entities;
using AiPlayground.DataAccess.Repositories;

namespace AiPlayground.BusinessLogic.Services;

public class ModelService: IModelService
{
    private readonly IRepository<Model> _modelRepository;

    public ModelService(IRepository<Model> modelRepository)
    {
        _modelRepository = modelRepository;
    }

    public async Task<IEnumerable<ModelDto>> GetAllModelsAsync()
    {
        var models = await _modelRepository.GetAllAsync();
        return models.Select(m => new ModelDto
        {
            Id = m.Id,
            Name = m.Name,
            PlatformId = m.PlatformId,
        });
    }

    public async Task<ModelDto?> GetModelByIdAsync(int id)
    {
        var model = await _modelRepository.GetByIdAsync(id);

        if (model == null)
        {
            return null;
        }

        return new ModelDto
        {
            Id = model.Id,
            Name = model.Name,
            PlatformId = model.PlatformId,
        };
    }

    public Task<ModelDto> CreateModelAsync(ModelCreateDto modelDto)
    {
        throw new NotImplementedException();
    }

    public Task<ModelDto> UpdateModelAsync(int id, ModelCreateDto modelCreateDto)
    {
        throw new NotImplementedException();
    }

    public Task DeleteModelAsync(int id)
    {
        throw new NotImplementedException();
    }
}