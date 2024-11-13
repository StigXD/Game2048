using System.IO;
using System.Windows;
using Game_2048.Database;
using Game_2048.Views.AuthenticationWindow.Logic;


namespace Game_2048;

/// <summary>
/// Логика взаимодействия для App.xaml
/// </summary>
public partial class App
{
	private readonly IUsersDB _usersDB;
	private readonly IAuthWindowProvider _authWindowProvider;

	public App(IUsersDB usersDB, IAuthWindowProvider authWindowProvider)
	{
		_usersDB = usersDB;
		_authWindowProvider = authWindowProvider;

		InitializeComponent();
	}

	protected override void OnStartup(StartupEventArgs e)
	{
		base.OnStartup(e);
		_usersDB.Read();
		_authWindowProvider.Show();
	}

	protected override void OnExit(ExitEventArgs e)
	{
		_usersDB.Write();
		base.OnExit(e);
	}
}