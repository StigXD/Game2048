using System.Windows;

namespace Game_2048.Base;

public interface IViewModel
{
	object Header { get; }
	FrameworkElement View { get; }
}