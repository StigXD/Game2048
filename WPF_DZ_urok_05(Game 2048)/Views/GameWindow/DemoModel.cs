using GalaSoft.MvvmLight.Messaging;

namespace Game_2048.Views.GameWindow;

public class DemoModel : GameViewModel
{
	public DemoModel() : base(Messenger.Default, null)
	{
	}
}