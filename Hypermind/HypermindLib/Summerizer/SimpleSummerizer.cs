using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HypermindLib
{
    public class SimpleSummerizer:Chain
    {
        PrompTemplate SummerizerPromp = new PrompTemplate(
@"""Summerize the following text between >>> and <<< 

>>>
{text}
<<<

Summery:"""
        );

        ModelWithPromp Sumerizer;
        public SimpleSummerizer(LLM llm)
        {
            Sumerizer = new ModelWithPromp(llm, SummerizerPromp);
        }

        public override ChainOutput Process(ChainInput input)
        {
            return Sumerizer.Process(input);
        }
    }
}
