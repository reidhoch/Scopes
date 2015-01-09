namespace Scopes.Engine.Nodes
{
    public class ConstantNode : ITerminalNode
    {
        private readonly double constant;

        public ConstantNode(double constant)
        {
            this.constant = constant;
        }

        public int Arity
        {
            get { return 0; }
        }

        public double Evaluate()
        {
            return this.constant;
        }
    }
}
