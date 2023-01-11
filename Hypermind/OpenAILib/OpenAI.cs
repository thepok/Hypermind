using OpenAI_API;
using HypermindLib;

namespace OpenAILib
{
    public class OpenAI : LLM
    {

        CompletionEndpoint api;
       
        public HttpClient? HtmlClient;

        public OpenAI()
        {
            var OPEN_AI_API_KEY = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
            var oai = new OpenAIAPI(new APIAuthentication(OPEN_AI_API_KEY));
            oai.UsingEngine = new Engine("text-davinci-003");

            api = oai.Completions;
            api.DefaultCompletionRequestArgs.MaxTokens = 250;
        }
        public override LLM_Output Process(LLM_Input promp)
        {
            
            var callTask = api.CreateCompletionAsync(promp.Prompt, HtmlClient);
            callTask.Wait();
            if (callTask.IsCompleted)
            {
                var result = callTask.Result;
                return new LLM_Output(result.Completions[0].Text);
            }
            else
            {
                return new LLM_Output(OutputState.Error);
            }
        }

        public async Task<LLM_Output> ProcessAsync(LLM_Input promp)
        {

            return new LLM_Output((await api.CreateCompletionAsync(promp.Prompt, HtmlClient)).Completions[0].Text);
        }
    }
}