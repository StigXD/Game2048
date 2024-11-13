using System.ComponentModel;
using System.Windows;
using Game_2048.Dispatcher;
using Game_2048.Models;
using Game_2048.Services.View;

namespace Game_2048.Views.GameWindow.Logic;

public class GameWindowProvider : IGameWindowProvider
{
	private readonly IViewService _viewService;
	private readonly IDispatcherHelper _dispatcherHelper;
	private readonly GameViewModel.IFactory _gameViewModelFactory;
	
    private Window _gameWindow;

	public GameWindowProvider(
		IViewService viewService,
		IDispatcherHelper dispatcherHelper,
		GameViewModel.IFactory gameViewModelFactory)
	{
		_viewService = viewService;
		_dispatcherHelper = dispatcherHelper;
		_gameViewModelFactory = gameViewModelFactory;
	}

	public void Show(UserModel user)
	{
		_dispatcherHelper.CheckBeginInvokeOnUI(() =>
		{
			_gameWindow ??= CreateWindow(user);
			_gameWindow.Show();
		});
    }
	public void CloseIfCreated()
	{
		_dispatcherHelper.CheckBeginInvokeOnUI(() => _gameWindow?.Close());
	}

	private Window CreateWindow(UserModel user)
	{
		var viewModel = _gameViewModelFactory.Create(user);
		var window = _viewService.CreateWindow(viewModel, WindowMode.Other);
		
		window.Closing += OnWindowClosing;
		return window;
	}

    private void OnWindowClosing(object sender, CancelEventArgs e)
	{
		_gameWindow.Closing -= OnWindowClosing;
		_gameWindow = null;
	}
}