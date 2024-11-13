using Game_2048.Models;

namespace Game_2048.Views.GameWindow.Logic;

public interface IGameWindowProvider
{
	void Show(UserModel user);
	void CloseIfCreated();
}