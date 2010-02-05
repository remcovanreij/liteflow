using System.Threading;
using LiteFlow.Core.Executor;
using log4net;

namespace LiteFlow.UI
{
	class TestDebugger : IDebugger
	{
		public void OnStart(Executor executor)
		{
			
		}

		public void OnEnd()
		{
		}

		public void OnInstruction(DebuggerContext context)
		{
			var instr = context.CurrentInstruction;
			LogManager.GetLogger("DBG").InfoFormat("{0}", context);
			Thread.Sleep(10);
		}
	}
}