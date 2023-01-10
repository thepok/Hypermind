using HypermindLib;
using System.Diagnostics;

namespace HypermindLib
{
    public abstract class AbstractInput<Type>
    {
        public Type Input { get; set; }
        public AbstractInput(Type input)
        {
            Input = input;
        }
    }

    public enum OutputState
    {
        Success,
        Error
    }

    public abstract class AbstractOutput<Type>
    {
        public Type? Result { get; set; }

        public OutputState State { get; set; }

        public AbstractOutput(Type result)
        {
            Result = result;
        }

        protected AbstractOutput(OutputState state)
        {
            State = state;
        }
    }

    public abstract class AbstractMind<TypeInput, TypeOutput>
    {
        public abstract TypeOutput Process(TypeInput input);
    }
}