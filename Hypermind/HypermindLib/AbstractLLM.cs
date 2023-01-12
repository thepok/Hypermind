using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyCaching.Core;
using EasyCaching.SQLite;


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
        public abstract LLM_Output CallLLM(LLM_Input input);

        public abstract string GetUniqueID();
        public override LLM_Output Process(LLM_Input input)
        {
            if(HypermindCache.GlobalEnabled)
            {
                //see if Cachehit
                var cache = HypermindCache.Cache;
                var cacheKey = GetUniqueID() + input.Prompt;
                if (cache.Exists(cacheKey))
                {
                    var cacheResult = cache.Get<string>(cacheKey);
                    return new LLM_Output(cacheResult.Value);
                }
                else
                {
                    var result = CallLLM(input);
                    cache.Set(cacheKey, result.Completion, HypermindCache.CacheTime);
                    return result;
                }
                

            }
            return CallLLM(input);
        }
    }
}
