using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HypermindLib;

namespace HypermindLib
{

    public class ModelWithPromp : Chain
    {
        LLM Model;
        PrompTemplate Promp;

        public ModelWithPromp(LLM model, PrompTemplate promp)
        {
            this.Model = model;
            this.Promp = promp;
        }

        public override ChainOutput Process(ChainInput input)
        {
            var filledPromp = Promp.Process(input);
            var prediction = Model.Process(filledPromp.Result[0].Value);
            var chainOutput = new ChainOutput("prediction", prediction.Result);
            return chainOutput;
        }
    }
    
}
