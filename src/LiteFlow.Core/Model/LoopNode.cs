using System.Collections.Generic;
using LiteFlow.Core.Compiler;

namespace LiteFlow.Core.Model
{
	public class LoopNode : Node
	{
		private string m_initExpression, m_iterExpression, m_testExpression;
		NodeList m_nodes = new NodeList();

		public LoopNode(string initExpression, string iterExpression, string testExpression, params Node[] nodes)
		{
			m_initExpression = initExpression;
			m_iterExpression = iterExpression;
			m_testExpression = testExpression;
			m_nodes.AddRange(nodes);
		}

		public LoopNode(string initExpression, string iterExpression, string testExpression, NodeList nodes)
		{
			m_initExpression = initExpression;
			m_iterExpression = iterExpression;
			m_testExpression = testExpression;
			m_nodes.AddRange(nodes);
		}

		internal override void Accept(INodeVisitor visitor)
		{
			visitor.Visit(this);
		}

		public string InitExpression
		{
			get { return m_initExpression; }
		}

		public string IterExpression
		{
			get { return m_iterExpression; }
		}

		public string TestExpression
		{
			get { return m_testExpression; }
		}

		public IList<Node> Nodes
		{
			get { return m_nodes.AsReadOnly(); }
		}
	}
}