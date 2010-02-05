using LiteFlow.Core.Model;

namespace LiteFlow.Core.Compiler
{
	internal interface INodeVisitor
	{
		void Visit(Workflow node);
		void Visit(CallNode node);
		void Visit(ForkNode node);
		void Visit(LoopNode node);
		void Visit(IfNode node);
		void Visit(SubCallNode node);
	}
}