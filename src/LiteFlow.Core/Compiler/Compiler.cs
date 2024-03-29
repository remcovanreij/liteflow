using System.Collections.Generic;
using LiteFlow.Core.Model;

namespace LiteFlow.Core.Compiler
{
	public class Compiler
	{
		public IList<Instruction> Compile(Workflow workflow)
		{
			NodeCompiler nc = new NodeCompiler();
			nc.Compile(workflow);
			return nc.Instructions;
		}
	}
}
