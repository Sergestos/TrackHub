using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Models;
using TrackHub.AiCrawler.PromptModels;

namespace TrackHub.AiCrawler.OpenAI;

public abstract class AbstractConversation
{
    private static Model OpenAIMode = Model.GPT4;
    private static double Temperature = 0;

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
        var apiToken = "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";
        var api = new OpenAIAPI(new APIAuthentication(apiToken));

        Conversation chat = api.Chat.CreateConversation();
        chat.Model = OpenAIMode;
        chat.RequestParameters.Temperature = Temperature;

        return chat;
    }
}
