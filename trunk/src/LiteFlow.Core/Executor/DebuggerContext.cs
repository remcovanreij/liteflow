using System.Collections.Generic;
using System.Collections.ObjectModel;
using LiteFlow.Core.Compiler;
using System.Linq;

namespace LiteFlow.Core.Executor
{
	public class DebuggerContext
	{
		private Instruction m_instruction;
		private int m_location;
		private readonly Executor m_owner;
		private readonly List<int> m_stack;
		// Stack
		public DebuggerContext(Executor owner, List<int> stack)
		{
			m_owner = owner;
			m_stack = stack;
		}

		public Instruction CurrentInstruction
		{
			get { return m_instruction; }
			set { m_instruction = value; }
		}

		public int Location
		{
			get { return m_location; }
			set { m_location = value; }
		}

		public ReadOnlyCollection<int> Stack
		{
			get { return m_stack.AsReadOnly(); }
		}

		public Executor Owner
		{
			get { return m_owner; }
		}

		public ReadOnlyCollection<Instruction> Instructions
		{
			get { return m_owner.Instructions; }
		}

		public override string ToString()
		{
			string stack = string.Join(" > ", Stack.Select(a => a.ToString()).ToArray());
			return string.Format("at {0}: {1}({2})", 
				stack, 
				CurrentInstruction.OpCode, 
				CurrentInstruction.Argument);
		}
	}
}