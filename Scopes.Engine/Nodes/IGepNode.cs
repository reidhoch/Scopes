namespace Scopes.Engine.Nodes
{
    public interface IGepNode
    {
        int Arity { get; }

        double Evaluate(double[] parameters);
    }
}
