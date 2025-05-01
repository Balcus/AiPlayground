using AiPlayground.BusinessLogic.Dto;
using AiPlayground.DataAccess.Entities;

namespace AiPlayground.BusinessLogic.Interfaces;

public interface IRunService
{
    Task<List<RunDto>> CreateRunsAsync(RunCreateDto runCreateDto);
}