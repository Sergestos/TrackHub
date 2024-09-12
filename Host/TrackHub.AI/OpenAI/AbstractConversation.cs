using Microsoft.Extensions.Configuration;
using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Models;
using TrackHub.AiCrawler.PromptModels;

namespace TrackHub.AiCrawler.OpenAI;

public abstract class AbstractConversation
{
    private static Model OpenAIMode = Model.GPT4;
    private static double Temperature = 0;

    private readonly IConfiguration _configuration;

    public AbstractConversation(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected Conversation GetConversation(GeneralPromptArgs generalArgs)
    {
        var conversation = BuildConversation();

        conversation.AppendSystemMessage(Prompts.ConversationTopic);
        conversation.AppendSystemMessage(Prompts.ResponseFormat);
        conversation.AppendSystemMessage(Prompts.PopularAssets);

        conversation.AppendSystemMessage($"Limit result in a list with {generalArgs.ExpectedLength} items.");
        conversation.AppendSystemMessage($"This is a search text pattern: {generalArgs.SearchPattern}." +
            $" You will be provided with instructions what to do with it further");

        return conversation;
    }

    protected async Task<IEnumerable<string>> GetAiResponse(Conversation conversation)
    {
        IEnumerable<string>? result;

        try
        {
            var chatReponse = await conversation.GetResponseFromChatbotAsync();
            result = chatReponse.Split(",").ToList();

        }
        catch (Exception ex)
        {
            // TODO log any exceptions here

            result = null;
        }
  
        return result!;
    }

    private Conversation BuildConversation()
    {
        var apiToken = _configuration["OpenAI:ApiToken"];
        var api = new OpenAIAPI(new APIAuthentication(apiToken));

        Conversation chat = api.Chat.CreateConversation();
        chat.Model = OpenAIMode;
        chat.RequestParameters.Temperature = Temperature;

        return chat;
    }
}
