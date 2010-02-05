using System;

namespace LiteFlow.Core.Executor
{
	public interface IDebugger
	{
		void OnStart(Executor executor);
		void OnEnd();
		void OnInstruction(DebuggerContext context);
	}
}