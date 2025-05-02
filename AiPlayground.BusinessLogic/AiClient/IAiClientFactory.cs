using AiPlayground.DataAccess.Entities;

namespace AiPlayground.BusinessLogic.AiClient;

public interface IAiClientFactory
{
    IAiClient GenerateClient(Model model);
}