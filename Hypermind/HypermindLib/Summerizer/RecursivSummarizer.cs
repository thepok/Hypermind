using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HypermindLib
{
    public class RecursivSummarizer : Chain
    {
        PrompTemplate SummarizerPromp = new PrompTemplate(
@"""Summerize the following text between >>> and <<< 

>>>
{text}
<<<

Summery:"""
        );

        ModelWithPromp Sumerizer;
        public RecursivSummarizer(LLM llm)
        {
            Sumerizer = new ModelWithPromp(llm, SummarizerPromp);
        }

        /// <summary>
        /// Simplified Call to Summarizer
        /// </summary>
        /// <param name="text">Text we want summerized</param>
        /// <returns></returns>
        public string Summarize(string text)
        {
            var splits = Textsplitter.SmartStringSplit(text, 8000);

            if (splits.Length == 1)
            {
                var input = new ChainInput("text", text);
                var output = Sumerizer.Process(input);
                return output.Result[0].Value;
            }
            else
            {
                var concatsummeries = splits.Select(s => Summarize(s)).Aggregate("", (current, next) => { return current +" "+ next; });
                

                return concatsummeries;
            }
        }

        public override ChainOutput Process(ChainInput input)
        {
            return Sumerizer.Process(input);
        }
    }
}