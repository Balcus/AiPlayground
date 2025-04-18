using AiPlayground.BusinessLogic.Dtos;
using AiPlayground.BusinessLogic.Interfaces;
using AiPlayground.DataAccess.Entities;
using AiPlayground.DataAccess.Repositories;

namespace AiPlayground.BusinessLogic.Services;

public class PromptService : IPromptService
{
    private readonly IRepository<Prompt> _promptRepository;
    public PromptService(IRepository<Prompt> promptRepository)
    {
        _promptRepository = promptRepository;
    }
    public async Task<IEnumerable<PromptDto>> GetAllPromptsAsync()
    {
        var prompts = await this._promptRepository.GetAllAsync();

        return prompts.Select(p => new PromptDto
        {
            Id = p.Id,
            Name = p.Name,
            UserMessage = p.UserMessage,
            ScopeId = p.ScopeId,
            ExpectedResponse = p.ExpectedResponse,
            SystemMsg = p.SystemMsg,
        });
    }
    
    public async Task<IEnumerable<PromptDto>> GetPromptsByScopeIdAsync(int scopeId)
    {
        var prompts = await _promptRepository.GetAllAsync();
        var promptsForScope = prompts.Where(p => p.ScopeId == scopeId);
    
        return promptsForScope.Select(p => new PromptDto
        {
            Id = p.Id,
            Name = p.Name,
            UserMessage = p.UserMessage,
            ScopeId = p.ScopeId,
            ExpectedResponse = p.ExpectedResponse,
            SystemMsg = p.SystemMsg,
        });
    }

    public async Task<PromptDto?> GetPromptByIdAsync(int id)
    {
        var prompt = await this._promptRepository.GetByIdAsync(id);

        if (prompt == null)
        {
            return null;
        }

        return new PromptDto
        {
            Id = prompt.Id,
            Name = prompt.Name,
            UserMessage = prompt.UserMessage,
            ScopeId = prompt.ScopeId,
            ExpectedResponse = prompt.ExpectedResponse,
            SystemMsg = prompt.SystemMsg,
        };
    }

    public async Task<PromptDto> CreatePromptAsync(PromptCreateDto promptCreateDto)
    {
        var prompt = new Prompt
        {
            Name = promptCreateDto.Name,
            UserMessage = promptCreateDto.UserMessage,
            ScopeId = promptCreateDto.ScopeId,
            ExpectedResponse = promptCreateDto.ExpectedResponse,
            SystemMsg = promptCreateDto.SystemMsg,
        };
        var createdPrompt = await _promptRepository.AddAsync(prompt);
        
        return new PromptDto
        {
            Id = createdPrompt.Id,
            Name = prompt.Name,
            UserMessage = prompt.UserMessage,
            ScopeId = prompt.ScopeId,
            ExpectedResponse = prompt.ExpectedResponse,
            SystemMsg = prompt.SystemMsg,
        };
    }

    public Task<PromptDto> UpdatePromptAsync(int id, PromptCreateDto promptCreateDto)
    {
        throw new NotImplementedException();
    }

    public async Task DeletePromptAsync(int id)
    {
        await _promptRepository.DeleteAsync(id);
    }
}