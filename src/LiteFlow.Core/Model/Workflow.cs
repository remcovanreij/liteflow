using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiteFlow.Core.Compiler;

namespace LiteFlow.Core.Model
{
	public class Workflow : Node	
	{
		List<Node> m_nodes = new List<Node>();
		private string m_name;

		public Workflow(string name)
		{
			m_name = name;
		}

		public string Name
		{
			get { return m_name; }
		}

		public IList<Node> Nodes
		{
			get { return m_nodes.AsReadOnly(); }
		}

		public void Add(Node node)
		{
			m_nodes.Add(node);
		}

		internal override void Accept(INodeVisitor visitor)
		{
			visitor.Visit(this);
		}
	}
}
