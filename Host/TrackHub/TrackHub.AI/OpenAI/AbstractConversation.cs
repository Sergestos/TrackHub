using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Models;
using TrackHub.AiCrawler.PromtModels;

namespace TrackHub.AiCrawler.OpenAI;

internal abstract class AbstractConversation
{
    private static Model OpenAIMode = Model.ChatGPTTurbo;
    private static double Temperature = 0;

    protected Conversation GetConversation(GeneralPromptArgs generalArgs)
    {
        var conversation = BuildConversation();

        conversation.AppendSystemMessage(Prompts.ConversationTopic);
        conversation.AppendSystemMessage(Prompts.ResponseFormat);

        conversation.AppendSystemMessage($"Limit result with {generalArgs.ExpectedLength} items.");
        conversation.AppendSystemMessage($"Items should start with next pattern: {generalArgs.SearchPattern}.");

        return conversation;
    }

    protected async Task<IEnumerable<string>> GetAiResponse(Conversation conversation)
    {
        IEnumerable<string>? aiResponse = null;
        await conversation.StreamResponseFromChatbotAsync(streamResponse =>
        {
            try
            {
                aiResponse = streamResponse.Split(",").ToList();
            }
            catch (Exception exception)
            {
                // TODO log exceptions with default Logger

                aiResponse = Enumerable.Empty<string>();
            }
        });

        return aiResponse!;
    }

    private Conversation BuildConversation()
    {
        var apiToken = "";
        var api = new OpenAIAPI(new APIAuthentication(apiToken));

        Conversation chat = api.Chat.CreateConversation();
        chat.Model = OpenAIMode;
        chat.RequestParameters.Temperature = Temperature;

        return chat;
    }
}
