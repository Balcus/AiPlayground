using AiPlayground.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace NetRomApp.Controllers;

[Route("api")]
[ApiController]
public class PromptsController : Controller
{
    private readonly IPromptService _promptsService;

    public PromptsController(IPromptService promptsService)
    {
        _promptsService = promptsService;
    }

}