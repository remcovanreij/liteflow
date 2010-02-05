using LiteFlow.Core.Compiler;

namespace LiteFlow.Core.Model
{
	public abstract class Node
	{
		internal abstract void Accept(INodeVisitor visitor);
	}
}