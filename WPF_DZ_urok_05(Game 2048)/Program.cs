using System;

namespace Game_2048;

public static class Program
{
	[STAThread]
	public static void Main()
	{
		Locator.Current.Locate<App>().Run();
	}
}