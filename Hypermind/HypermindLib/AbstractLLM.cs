using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HypermindLib
{
    public class LLM_Input : AbstractInput<string>
    {
        public string Prompt;
        public LLM_Input(string promp) : base(promp)
        {
            this.Prompt = promp;
        }

        public static implicit operator LLM_Input(string value)
        {
            return new LLM_Input(value);
        }
    }

    public class LLM_Output : AbstractOutput<string>
    {
        public string Completion;
        public LLM_Output(string completion) : base(completion)
        {
            this.Completion = completion;
            this.State = OutputState.Success;
        }

        public LLM_Output(OutputState state) : base(state)
        {
        }
    }

    public abstract class LLM : AbstractMind<LLM_Input, LLM_Output>
    {

    }
}
