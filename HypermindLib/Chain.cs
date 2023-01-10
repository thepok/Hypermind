using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HypermindLib
{
    public class NamedParameter
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public NamedParameter(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public override string ToString()
        {
            return $"[{Name}:{Value}]";
        }
    }

    public class NamedParameters : List<NamedParameter>
    {

        public NamedParameters(params ValueTuple<string, string>[] inputs)
        {
            foreach (var pair in inputs)
            {
                Add(new NamedParameter(pair.Item1, pair.Item2));
            }
        }

        public NamedParameters(params string[] inputs)
        {
            for (int i = 0; i < inputs.Length - 1; i++)
            {
                Add(new NamedParameter(inputs[i], inputs[i + 1]));
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var pair in this)
            {
                sb.Append(pair.ToString());
            }
            return sb.ToString();
        }
    }
    public class ChainInput : AbstractInput<NamedParameters>
    {
        public ChainInput(params ValueTuple<string, string>[] nameValuePairs) : base(new NamedParameters(nameValuePairs))
        {
        }
        public ChainInput(params string[] NameValuePairs) : base(new NamedParameters(NameValuePairs)) { }
        public ChainInput(NamedParameters input) : base(input)
        {
        }

        public bool AssertNeededParamsSet(params string[] names)
        {
            var namesInInput = (from a in this.Input select a.Name).ToList();
            //test if every name is in input
            foreach (var name in names)
            {
                if (namesInInput.Contains(name) == false)
                    return false;
            }
            return true;
        }

        public override string ToString()
        {
            return this.Input.ToString();
        }
    }

    public class ChainOutput : AbstractOutput<NamedParameters>
    {
        public static ChainOutput GetFailed()
        {
            var failedResult = new ChainOutput();
            failedResult.State = OutputState.Error;
            return failedResult;
        }
        public ChainOutput(params string[] NameVarPairs) : base(new NamedParameters(NameVarPairs))
        {
        }
        public ChainOutput(NamedParameters result) : base(result)
        {
        }

        public override string ToString()
        {
            if (this.State == OutputState.Error)
                return "Error";
            else
            {
                return this.Result.ToString();
            }
        }
    }

    public abstract class Chain : AbstractMind<ChainInput, ChainOutput>
    {
        /// <summary>
        /// Simple chains only have one Input
        /// </summary>
        public bool Simple = false;


        public ChainOutput Process(params ValueTuple<string, string>[] nameValuePairs)
        {
            return Process(new ChainInput(nameValuePairs));
        }
    }
}
