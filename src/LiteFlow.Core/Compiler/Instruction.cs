namespace LiteFlow.Core.Compiler
{
	public class Instruction
	{
		private OpCode m_opCode;
		private int m_argument;

		public Instruction(OpCode opCode, int argument)
		{
			m_opCode = opCode;
			m_argument = argument;
		}

		public OpCode OpCode
		{
			get { return m_opCode; }
		}

		public int Argument
		{
			get { return m_argument; }
			internal set { m_argument = value; }
		}
	}
}