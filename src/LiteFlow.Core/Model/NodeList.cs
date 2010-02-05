using System.Collections.Generic;
using LiteFlow.Core.Compiler;

namespace LiteFlow.Core.Model
{
	public class NodeList : List<Node>
	{
		public NodeList(IList<Node> nodes)
		{
			AddRange(nodes);
		}

		public NodeList(params Node[] nodes)
		{
			AddRange(nodes);
		}

		internal void Accept(INodeVisitor visitor)
		{
			foreach (Node node in this)
			{
				node.Accept(visitor);
			}
		}
	}
}