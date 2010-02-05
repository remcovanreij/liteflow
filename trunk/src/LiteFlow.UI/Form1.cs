using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LiteFlow.Core.Compiler;
using LiteFlow.Core.Executor;
using LiteFlow.Core.Model;
using log4net;

namespace LiteFlow.UI
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Workflow sub = new Workflow("sub");
			sub.Add(new CallNode("call sub.1"));
			sub.Add(new CallNode("call sub.2"));
			sub.Add(new CallNode("call sub.3"));
			sub.Add(new CallNode("call sub.4"));
			sub.Add(new CallNode("call sub.5"));
			sub.Add(new CallNode("call sub.6"));

			Workflow workflow = new Workflow("main");
			Compiler compiler = new Compiler();

			workflow.Add(new CallNode("call 1"));
			workflow.Add(new CallNode("call 2"));
			workflow.Add(new LoopNode("x = 0", "x++", "x < 10", new CallNode("call 3.1"), new CallNode("call 3.2")));
			
			var fork = new ForkNode();
			fork.AddBranch(new CallNode("call 4.1.1"), new CallNode("call 4.1.2"), new CallNode("call 4.1.3"));
			fork.AddBranch(new CallNode("call 4.2"));
			fork.AddBranch(new CallNode("call 4.3"));
			workflow.Add(fork);

			var ifnode = new IfNode();
			ifnode.AddBranch("y < 1", new CallNode("call 5.1.1"), new CallNode("call 5.1.2"), new CallNode("call 5.1.3"));
			ifnode.AddBranch("y > 1", new CallNode("call 5.2"));
			ifnode.AddBranch("else", new CallNode("call 5.3"));
			workflow.Add(ifnode);

			workflow.Add(new CallNode("call 6"));
			workflow.Add(new SubCallNode(sub));
			workflow.Add(new CallNode("call 7"));

			IList<Instruction> instructions = compiler.Compile(workflow);

			int i = 0;
			foreach (var instruction in instructions)
			{
				LogManager.GetLogger("SCRIPT").InfoFormat("{0}:  {1}({2})",
					i++, instruction.OpCode, instruction.Argument);
			}
			LogManager.GetLogger("SCRIPT").Info("====================================================");

			Executor executor = new Executor(instructions);
			executor.Debugger = new TestDebugger();
			executor.Run();
		}
	}
}
