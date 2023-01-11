using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HypermindLib;

namespace HypermindLib
{
    public class PrompTemplate : Chain
    {
        string Promp;

        public PrompTemplate(string promp)
        {
            this.Promp = promp;

            if (Regex.Matches(Promp, @"\{[^}]*\}").Count == 1)
            {
                this.Simple = true;
            }
        }
        
        public override ChainOutput Process(ChainInput input)
        {
            var filledPromp = new StringBuilder(Promp);
            
            foreach (var param in input.Input)
            {
                filledPromp.Replace($"{{{param.Name}}}", param.Value);
            }

            return new ChainOutput("Result", filledPromp.ToString());
        }

    }
}
