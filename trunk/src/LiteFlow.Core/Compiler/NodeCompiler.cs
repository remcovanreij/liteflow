using System;
using System.Collections.Generic;
using System.Linq;
using LiteFlow.Core.Model;
using log4net;

namespace LiteFlow.Core.Compiler
{
	class NodeCompiler : INodeVisitor
	{
		List<Instruction> m_instructions = new List<Instruction>();
		Dictionary<int, int> m_subAddresses = new Dictionary<int, int>();
		List<Workflow> m_subs = new List<Workflow>();

		public void Visit(Workflow node)
		{
			m_subAddresses[node.GetHashCode()] = NextLoc;

			foreach (var child in node.Nodes)
			{
				child.Accept(this);
			}

			AddInstruction(OpCode.RET, 0);
		}

		public void Visit(ForkNode node)
		{
			foreach (var branch in node.Branches)
			{
				var fk = AddInstruction(OpCode.FORK, 1);

				foreach (var child in branch)
					child.Accept(this);

				AddInstruction(OpCode.RET, 0);

				fk.Argument = NextLoc;
			}
			AddInstruction(OpCode.JOIN, 0);
		}

		public void Visit(LoopNode node)
		{
			AddInstruction(OpCode.EVAL, 1);
			int loc = CurrentLoc;
			var jf = AddInstruction(OpCode.JF, 1);

			foreach (var child in node.Nodes)
				child.Accept(this);

			AddInstruction(OpCode.JUMP, loc);
			jf.Argument = NextLoc;
		}

		public void Visit(SubCallNode node)
		{
			m_subs.Add(node.Subroutine);
			int nodeHash = node.Subroutine.GetHashCode();
			AddInstruction(OpCode.SUB, nodeHash);
		}

		public void Visit(IfNode node)
		{
			List<Instruction> jumps = new List<Instruction>();
			foreach (var branch in node.Branches)
			{
				AddInstruction(OpCode.EVAL, 1);
				var jf = AddInstruction(OpCode.JF, 1);

				foreach (var child in branch.Nodes)
					child.Accept(this);

				jf.Argument = NextLoc;

				var jump = AddInstruction(OpCode.JUMP, 1);
				jumps.Add(jump);
			}

			foreach (var jump in jumps)
			{
				jump.Argument = NextLoc;
			}
		}

		public void Visit(CallNode node)
		{
			AddInstruction(OpCode.EXEC, 1);
		}

		internal Instruction AddInstruction(OpCode opcode, int arg)
		{
			var ins = new Instruction(opcode, arg);
			m_instructions.Add(ins);
			return ins;
		}

		private ILog Logger
		{
			get { return LogManager.GetLogger(GetType()); }
		}

		public IList<Instruction> Instructions
		{
			get { return m_instructions.AsReadOnly(); }
		}

		protected int CurrentLoc
		{
			get { return m_instructions.Count - 1; }
		}

		protected int NextLoc
		{
			get { return CurrentLoc + 1; }
		}

		private int LocationOf(Instruction instruction)
		{
			return m_instructions.IndexOf(instruction);
		}

		public void Compile(Workflow workflow)
		{
			workflow.Accept(this);
			m_subs.ForEach(Visit);
			FixSubroutineCalls();
		}

		private void FixSubroutineCalls()
		{
			m_instructions
				.FindAll(i => i.OpCode == OpCode.SUB)
				.ForEach(i => i.Argument = m_subAddresses[i.Argument]);
		}
	}
}