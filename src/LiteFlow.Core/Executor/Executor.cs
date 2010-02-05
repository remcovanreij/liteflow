using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using LiteFlow.Core.Compiler;
using log4net;

namespace LiteFlow.Core.Executor
{
	public class Executor
	{
		private List<Instruction> m_instructions;
		private List<int> m_parallels;
		private IDebugger m_debugger = new NullDebugger();
		DebuggerContext m_dbgContext;
		List<int> m_stack = new List<int>();

		public Executor(IList<Instruction> instructions)
		{
			m_stack.Add(-1);
			m_dbgContext = new DebuggerContext(this, m_stack);
			m_instructions = new List<Instruction>(instructions); // todo: optimize copying
			m_parallels = new List<int>();
		}

		internal Executor(List<Instruction> instructions, List<int> stack)
		{
			m_stack = new List<int>(stack);
			m_stack.Add(-1);

			m_dbgContext = new DebuggerContext(this, m_stack);
			m_instructions = instructions;
			m_parallels = new List<int>();
		}

		public void Run(int idx)
		{
			Debugger.OnStart(this);
			do
			{
				int step = ExecuteInstruction(idx);
				idx += step;
			} while (m_instructions[idx].OpCode != OpCode.RET);
			SendToDebugger(idx);

			Debugger.OnEnd();
		}

		public void Run()
		{
			Run(0);
		}

		private ILog Logger
		{
			get { return LogManager.GetLogger(GetType()); }
		}

		public IDebugger Debugger
		{
			get { return m_debugger; }
			set
			{
				if (value == null) throw new ArgumentNullException("value");
				m_debugger = value;
			}
		}

		public ReadOnlyCollection<int> Stack
		{
			get { return m_stack.AsReadOnly(); }
		}

		internal ReadOnlyCollection<Instruction> Instructions
		{
			get { return m_instructions.AsReadOnly(); }
		}

		private int ExecuteInstruction(int idx)
		{
			Instruction curr = m_instructions[idx];
			SendToDebugger(idx);

			if (curr.OpCode == OpCode.FORK)
			{
				AddParallelBranch(idx);
			}

			if (curr.OpCode == OpCode.JOIN)
			{
				RunParallelBranches();
			}

			if (curr.OpCode == OpCode.EXEC)
			{
				// todo: call
			}

			switch (curr.OpCode)
			{
				case OpCode.JF:
				case OpCode.JT:
				case OpCode.JUMP:
				case OpCode.SUB:
				case OpCode.FORK:
					return (curr.Argument - idx);
				default:
					return 1;
			}
		}

		private void UpdateStack(int idx)
		{
			int last = m_stack.Count - 1;
			m_stack[last] = idx;
		}

		private void SendToDebugger(int idx)
		{
			UpdateStack(idx);
			m_dbgContext.Location = idx;
			m_dbgContext.CurrentInstruction = m_instructions[idx];
			Debugger.OnInstruction(m_dbgContext);
		}

		private void AddParallelBranch(int idx)
		{
			m_parallels.Add(idx);
		}

		private void RunParallelBranches()
		{
			List<Thread> threads = new List<Thread>();
			var parallels = m_parallels.ToArray();
			m_parallels.Clear();
			
			foreach (int index in parallels)
			{
				Thread thread = new Thread(ParallelBranchProc);
				threads.Add(thread);
				thread.Start(index);
			}

			foreach (Thread thread in threads)
			{
				thread.Join();
			}
		}

		private void ParallelBranchProc(object index)
		{
			int idx = (int) index + 1;
			Instruction curr = m_instructions[idx];
		
			Executor ex = ForkNewExecutor();  
			ex.Run(idx);
      }

		private Executor ForkNewExecutor()
		{
			var exec = new Executor(m_instructions, m_stack);
			exec.Debugger = Debugger;
			return exec;
		}
	}
}
