using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HypermindLib
{
    public class SimpleSummarizer:Chain
    {
        PrompTemplate SummarizerPromp = new PrompTemplate(
@"""Summerize the following text between >>> and <<< 

>>>
{text}
<<<

Summery:"""
        );

        ModelWithPromp Sumerizer;
        public SimpleSummarizer(LLM llm)
        {
            Sumerizer = new ModelWithPromp(llm, SummarizerPromp);
        }

        /// <summary>
        /// Simplified Call to Summarizer
        /// </summary>
        /// <param name="text">Text we want summerized</param>
        /// <returns></returns>
        public string Summerize(string text)
        {
            var input = new ChainInput("text", text);
            var output = Sumerizer.Process(input);
            return output.Result[0].Value;
        }
        
        public override ChainOutput Process(ChainInput input)
        {
            return Sumerizer.Process(input);
        }
    }
}
