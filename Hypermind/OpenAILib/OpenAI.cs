using OpenAI_API;
using HypermindLib;
using static System.Net.Mime.MediaTypeNames;

namespace OpenAILib
{
    public class OpenAIModels
    {

    }
    
    public class OpenAI : LLM
    {

        CompletionEndpoint api;
       
        public HttpClient? HtmlClient;

        public const string
            DAVINCI = "text-davinci-003",
            CURIE = "text-curie-001",
            BAGGAGE = "text-babbage-001",
            ADA = "text-ada-001",
            STRONGEST = DAVINCI;

        /// <summary>
        /// Create a new wrapper to access OpenAI LLMs
        /// </summary>
        /// <param name="model">Name of Model, see OpenAIModels for possible Values</param>
        /// <param name="maxNewTokens">Maximum numbers of Tokens to generate</param>
        public OpenAI(string model = STRONGEST, int maxNewTokens=250, double tempreature=0)
        {
            var OPEN_AI_API_KEY = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
            var oai = new OpenAIAPI(new APIAuthentication(OPEN_AI_API_KEY));
            oai.UsingEngine = new Engine(model);

            api = oai.Completions;
            api.DefaultCompletionRequestArgs.MaxTokens = maxNewTokens;
            api.DefaultCompletionRequestArgs.Temperature = tempreature;

            UniqueID = $"OpenAI_{model}_{tempreature}_{maxNewTokens}";
        }

        
        public override LLM_Output CallLLM(LLM_Input promp)
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

        public async Task<LLM_Output> CallLLMAsync(LLM_Input promp)
        {

            return new LLM_Output((await api.CreateCompletionAsync(promp.Prompt, HtmlClient)).Completions[0].Text);
        }

        private string UniqueID;
        public override string GetUniqueID()
        {
            return UniqueID;
        }
    }
}