using System.Collections.Generic;
using LiteFlow.Core.Compiler;

namespace LiteFlow.Core.Model
{
	public class ForkNode : Node
	{
		private List<NodeList> m_branches = new List<NodeList>();

		public ForkNode(params NodeList[] branches)
		{
			m_branches.AddRange(branches);				
		}

		public ForkNode(IEnumerable<NodeList> branches)
		{
			m_branches.AddRange(branches);
		}

		public void AddBranch(params Node[] nodes)
		{
			var branch = new NodeList(nodes);
			m_branches.Add(branch);
		}

		public IList<NodeList> Branches
		{
			get { return m_branches.AsReadOnly(); }
		}

		internal override void Accept(INodeVisitor visitor)
		{
			visitor.Visit(this);
		}
	}
}