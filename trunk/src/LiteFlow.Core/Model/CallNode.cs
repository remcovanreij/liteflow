using LiteFlow.Core.Compiler;

namespace LiteFlow.Core.Model
{
	public class CallNode : Node
	{
		private string m_callExpression;

		public CallNode(string callExpression)
		{
			m_callExpression = callExpression;
		}

		public string CallExpression
		{
			get { return m_callExpression; }
		}

		internal override void Accept(INodeVisitor visitor)
		{
			visitor.Visit(this);
		}
	}
}