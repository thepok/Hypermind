using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HypermindLib
{
    public class AskLLM
    {
        LLM LLM;
        public AskLLM(LLM llm)
        {
            this.LLM = llm;
        }

        public string Ask(string question)
        {
            var answer = LLM.Process(question);
            return answer.Result;
        }
    }
}
