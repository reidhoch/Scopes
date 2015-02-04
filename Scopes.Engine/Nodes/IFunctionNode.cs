namespace Scopes.Engine.Nodes
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    [ContractClass(typeof(FunctionNodeContract))]
    public interface IFunctionNode : IGepNode
    {
        IList<IGepNode> Children { get; }
    }

    [ContractClassFor(typeof(IFunctionNode))]
    internal abstract class FunctionNodeContract : IFunctionNode
    {
        public IList<IGepNode> Children
        {
            get
            {
                Contract.Ensures(Contract.Result<IList<IGepNode>>().Count <= 2);
                return default(IList<IGepNode>);
            }
        }

        public int Arity { get; private set; }

        public IGepNode Clone()
        {
            return default(IGepNode);
        }

        public double Evaluate(double[] parameters)
        {
            return 0.0;
        }
    }
}
