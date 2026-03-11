using Microsoft.Extensions.AI;
using OpenAI;
using System.ClientModel;

namespace BlazorChatIA.Services;

public static class AIServiceCollectionExtensions
{
    public static IServiceCollection AddGitHubModels(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var apiKey = configuration["OpenAI_Key"]
            ?? throw new InvalidOperationException("OpenAI_Key não configurada");

        var openAIClient = new OpenAIClient(
            new ApiKeyCredential(apiKey),
            new OpenAIClientOptions
            {
                Endpoint = new Uri("https://models.inference.ai.azure.com")
            });

        services.AddSingleton<IChatClient>(
            openAIClient.GetChatClient("gpt-4o").AsIChatClient());

        return services;
    }
}

