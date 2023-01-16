using HypermindLib;
using static System.Net.Mime.MediaTypeNames;
using OpenAI.GPT3.Managers;
using OpenAI.GPT3.ObjectModels.RequestModels;
using OpenAI.GPT3.ObjectModels;
using OpenAI.GPT3;
using OpenAI.GPT3.Interfaces;

namespace HypermindLib
{
    
    public class OpenAI : LLM
    {

       
        public HttpClient? HtmlClient;

        public const string
            DAVINCI = "text-davinci-003",
            CURIE = "text-curie-001",
            BAGGAGE = "text-babbage-001",
            ADA = "text-ada-001",
            STRONGEST = DAVINCI;

        OpenAIService openAiService;

        int MaxTokens = 250;
        double Temperature = 0.5;
        string Model;

        /// <summary>
        /// Create a new wrapper to access OpenAI LLMs
        /// </summary>
        /// <param name="model">Name of Model, see OpenAIModels for possible Values</param>
        /// <param name="maxNewTokens">Maximum numbers of Tokens to generate</param>
        public OpenAI(string model = STRONGEST, int maxNewTokens=250, double temperature=0)
        {
            openAiService = new OpenAIService(new OpenAiOptions()
            {
                ApiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY"),
                Organization = "" //not yet needed
            });



            Model = model;
            MaxTokens = maxNewTokens;
            Temperature = temperature;

            UniqueID = $"OpenAI_{model}_{temperature}_{maxNewTokens}";
        }

        
        public override LLM_Output CallLLM(LLM_Input promp)
        {
            var completionResult = openAiService.Completions
            .CreateCompletion(new CompletionCreateRequest()
            {
                Prompt = promp.Input,
                MaxTokens = MaxTokens
            }, Model);

            completionResult.Wait();



            if (completionResult.IsCompleted)
            {
                var response = completionResult.Result
                .Choices.FirstOrDefault()?.Text ?? "";
                return new LLM_Output(response);
            }
            else
            {
                return new LLM_Output(OutputState.Error);
                //if (completionResult.Error == null)
                //{
                //    response = "Unknown Error";
                //}
                //response =
                //$"{completionResult.Error?.Code}: {completionResult.Error?.Message}";
            }
        }


        private string UniqueID;
        public override string GetUniqueID()
        {
            return UniqueID;
        }
    }
}