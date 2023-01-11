using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HypermindLib;

namespace HypermindLib
{
    public class AnswerQuestionWithText:Chain
    {
        PrompTemplate QuestionPromp = new PrompTemplate(@"""Read the text between >>> and <<< and answer the question following the text.
>>>
{text}
<<<

Question:{question}
If you can'tfind the answer in the text, say 'no answer found'.

Answer:""");

        Chain SimpleQuestionAnswerer;

        public AnswerQuestionWithText(LLM llm)
        {
            SimpleQuestionAnswerer = new ModelWithPromp(llm, QuestionPromp);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text">Text possibly containing answer</param>
        /// <param name="question">Question about text</param>
        /// <returns>Answer or "not found"</returns>
        public string AskQuestion(string text, string question)
        {
            var input = new ChainInput("text", text, "question", question);
            var output = SimpleQuestionAnswerer.Process(input);
            return output.Result[0].Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input">expects "text" param and "question" param</param>
        /// <returns></returns>
        public override ChainOutput Process(ChainInput input)
        {
            if (input.AssertNeededParamsSet("text", "question"))
                return SimpleQuestionAnswerer.Process(input);
            else
                return ChainOutput.GetFailed();
        }
    }
}
