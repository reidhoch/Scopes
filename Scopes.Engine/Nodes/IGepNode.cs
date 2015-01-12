namespace Scopes.Engine.Nodes
{
    using System.Collections.Generic;

    public interface IGepNode
    {
        int Arity { get; }

        double Evaluate();
    }
}
