using LiteFlow.Core.Compiler;

namespace LiteFlow.Core.Model
{
	public class SubCallNode : Node
	{
		private Workflow m_sub;

		public SubCallNode(Workflow sub)
		{
			m_sub = sub;
		}

		public Workflow Subroutine
		{
			get { return m_sub; }
		}

		internal override void Accept(INodeVisitor visitor)
		{
			visitor.Visit(this);
		}
	}
}