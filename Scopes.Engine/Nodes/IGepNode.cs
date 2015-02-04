namespace Scopes.Engine.Nodes
{
    using System.Diagnostics.Contracts;

    [ContractClass(typeof(GepNodeContract))]
    public interface IGepNode
    {
        int Arity { get; }

        IGepNode Clone();

        double Evaluate(double[] parameters);
    }

    [ContractClassFor(typeof(IGepNode))]
    internal abstract class GepNodeContract : IGepNode
    {
        public int Arity { get; private set; }

        public IGepNode Clone()
        {
            Contract.Ensures(Contract.Result<IGepNode>() != null);
            return default(IGepNode);
        }

        public double Evaluate(double[] parameters)
        {
            return 0.0;
        }
    }
}
