﻿using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using Game_2048.Base;
using Game_2048.Database;
using Game_2048.Entities;
using Game_2048.Messages;
using Game_2048.Models;
using Game_2048.Views.GameWindow.Logic;

namespace Game_2048.Views.AuthenticationWindow;

public class AuthViewModel : ViewModel<AuthWindow>
{
	public override object Header => "Log In";
	private UserModel _inputtedUser;
	private IUsersDB _usersDB;

	private readonly IMessenger _messenger;
	private readonly IGameWindowProvider _gameWindowProvider;

	public UserModel InputtedUser
	{
		get => _inputtedUser;
		private set => Set(ref _inputtedUser, value);
	}

	private ICommand _logInCommand;
	private ICommand _cancelCommand;
	private ICommand _contentRenderedCommand;

	public ICommand ContentRenderedCommand => _contentRenderedCommand ??= new RelayCommand(OnContentRendered);
	public ICommand LogInCommand => _logInCommand ??= new RelayCommand(LogIn);
	public ICommand CancelCommand => _cancelCommand ??= new RelayCommand(Cancel);
	public AuthViewModel(IMessenger messenger, IGameWindowProvider gameWindowProvider, IUsersDB users)
	{
		_messenger = messenger;
		_gameWindowProvider = gameWindowProvider;
		_messenger = messenger;
		_usersDB = users;
		_inputtedUser = new();
	}

	private void OnContentRendered()
	{
		if (_usersDB?.Users.Find(user => user.IsRememberUser) == null)
			return;

		InputtedUser.Username = _usersDB.Users.Find(user => user.IsRememberUser).Username;
		InputtedUser.IsRememberUser = _usersDB.Users.Find(user => user.IsRememberUser).IsRememberUser;
	}
	private void LogIn()
	{
		if (InputtedUser.Username == string.Empty)
			return;

		if (InputtedUser.IsRememberUser)
			foreach (var u in _usersDB.Users)
				u.IsRememberUser = false;

		var user = _usersDB.Users.Find(u => u.Username == InputtedUser.Username);

		if (user == null)
		{
			_usersDB.Add(InputtedUser);
			_gameWindowProvider.Show(InputtedUser);
		}
		else
		{
			user.IsRememberUser = InputtedUser.IsRememberUser;
			_gameWindowProvider.Show(user);
		}

		_messenger.Send(new RequestCloseMessage(this, null));
	}
	private void Cancel()
	{
		_messenger.Send(new RequestCloseMessage(this, null));
	}

	public interface IFactory
	{
		AuthViewModel Create(UserModel user);
	}
}