using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using log4net.Repository.Hierarchy;

namespace LiteFlow.UI
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			log4net.Config.XmlConfigurator.Configure();
			Application.Run(new Form1());
		}
	}
}
