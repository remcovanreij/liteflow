using System.Collections.Generic;
using LiteFlow.Core.Compiler;

namespace LiteFlow.Core.Model
{
	public class IfNode : Node
	{
		private List<IfBranch> m_branches = new List<IfBranch>();

		public IfNode(IList<IfBranch> branches)
		{
			m_branches.AddRange(branches);
		}
		public IfNode(params IfBranch[] branches)
		{
			m_branches.AddRange(branches);
		}

		public void AddBranch(string condition, params Node[] nodes)
		{
			m_branches.Add(new IfBranch(condition, nodes));
		}

		public IList<IfBranch> Branches
		{
			get { return m_branches.AsReadOnly(); }
		}

		internal override void Accept(INodeVisitor visitor)
		{
			visitor.Visit(this);
		}
	}

	public class IfBranch
	{
		private string m_condition;
		private NodeList m_nodes;

		public IfBranch(string condition, NodeList nodes)
		{
			m_condition = condition;
			m_nodes = nodes;
		}

		public IfBranch(string condition, params Node[] nodes)
		{
			m_condition = condition;
			m_nodes = new NodeList(nodes);
		}

		public string Condition
		{
			get { return m_condition; }
		}

		internal NodeList Nodes
		{
			get { return m_nodes; }
		}
	}
}