using LiteFlow.Core.Compiler;

namespace LiteFlow.Core.Executor
{
	class NullDebugger : IDebugger
	{
		public void OnStart(Executor executor)
		{
		}

		public void OnEnd()
		{
		}

		public void OnInstruction(DebuggerContext context)
		{
		}
	}
}