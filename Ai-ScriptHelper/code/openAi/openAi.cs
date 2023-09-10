using OpenAI_API;

public class OpenAi
{
    private OpenAIAPI openAiApi;
    public OpenAi(string _openAiToken) 
    {
        openAiApi = new OpenAIAPI(_openAiToken);
    }
}