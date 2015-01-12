namespace Scopes.Engine.Nodes
{
    using System.Collections.Generic;

    public interface IFunctionNode : IGepNode
    {
        IList<IGepNode> Children { get; }
    }
}
