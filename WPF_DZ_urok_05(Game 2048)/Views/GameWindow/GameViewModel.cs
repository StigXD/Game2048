using System;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using Game_2048.Base;
using Game_2048.Entities;
using Game_2048.Enums;
using Game_2048.Messages;
using Game_2048.Models;
using Game_2048_.Logic;
using WPF_DZ_urok_05_Game_2048_.Logic;

namespace Game_2048.Views.GameWindow;

public class GameViewModel : ViewModel<GameWindow>
{
	public override object Header => "2048";
	private UserModel _user;
	private readonly IMessenger _messenger;
	private FieldModel _fieldModel;
	private IGameLogic _game;

	private bool isNotLeft;
	private bool isNotUp;
	private bool isNotRight;
	private bool isNotDown;

	private int _currentScore;

	private Random _rnd;

	public UserModel User
	{
		get => _user;
		set => Set(ref _user, value);
	}

	public IGameLogic Game
	{
		get => _game;
		private set => Set(ref _game, value);
    }
    public FieldModel FieldModel
	{
		get => _fieldModel;
		private set => Set(ref _fieldModel, value);
	}

	public int CurrentScore
	{
		get => _currentScore;
		private set => Set(ref _currentScore, value);
	}


	private ICommand _keyDownCommand;
	private ICommand _exitCommand;
	private ICommand _contentRenderedCommand;
	private ICommand _restartCommand;


	public ICommand ContentRenderedCommand => _contentRenderedCommand ??= new RelayCommand(OnContentRendered);
	public ICommand KeyDownCommand => _keyDownCommand ??= new RelayCommand<KeyEventArgs>(OnKeyboardArrow);
	public ICommand ExitCommand => _exitCommand ??= new RelayCommand(OnExit);
	public ICommand RestartCommand => _restartCommand ??= new RelayCommand(Restart);

	public GameViewModel(IMessenger messenger, UserModel user)
	{
		_messenger = messenger;
		User = user;
		FieldModel = new FieldModel();
		_rnd = new Random();
		_game = new GameLogic();
	}

	private void OnContentRendered()
	{
		Game.NewField();
		RandomTwoAndFour(); // Вызов метода для добавленеия числа на игровое поле
		RandomTwoAndFour();
	}

	private void RandomTwoAndFour() // метод IGameLogic для добавления числа на игровое поле
    {
		var newPosition = GetRandomEmptyPosition();

		FieldModel.Field[newPosition.Row][newPosition.Column].Value = GetRandomValue();
	}

	private CellValues GetRandomValue() => _rnd.Next(1, 100) >= 90 ? CellValues.Four : CellValues.Two;

	private Coordinates GetRandomEmptyPosition()
	{
		int row, col;

		do
		{
			row = _rnd.Next(0, FieldModel.Field.Count);
			col = _rnd.Next(0, FieldModel.Field.Count);
		} while (!FieldModel.Field[row][col].IsNone());

		return new Coordinates(row, col);
	}

	private void OnKeyboardArrow(KeyEventArgs args) // вызов метода для начала игры при нажатии клавиши
	{
		Game.PlayStep();

		switch (args.Key)
		{
			case Key.Left:
			case Key.A:
				isNotLeft = TryMoveLeft();
				break;
			case Key.Up:
			case Key.W:
				isNotUp = TryMoveUp();
				break;
			case Key.Right:
			case Key.D:
				isNotRight = TryMoveRight();
				break;
			case Key.Down:
			case Key.S:
				isNotDown = TryMoveDown();
				break;
		}

		for (var i = 0; i < Constants.FieldSize; i++)
		{
			for (var j = 0; j < Constants.FieldSize; j++)
			{
				if (FieldModel.Field[i][j].Value != CellValues.TwoThousandFortyEight)
					continue;

				MessageBox.Show("You win!", "Game over");
				if (User.HighScore < CurrentScore)
					User.HighScore = CurrentScore;
				RaisePropertyChanged(nameof(User));
				Restart();
				return;
			}
		}

		if (isNotLeft && isNotUp && isNotRight && isNotDown)
		{
			MessageBox.Show("You lose!", "Game over");
			if (User.HighScore < CurrentScore)
				User.HighScore = CurrentScore;
			RaisePropertyChanged(nameof(User));
			Restart();
			return;
		}

		for (var i = 0; i < Constants.FieldSize; i++)
		{
			for (var j = 0; j < Constants.FieldSize; j++)
			{
				if (!FieldModel.Field[i][j].IsNone())
					continue;

				RandomTwoAndFour();
				return;
			}
		}
	}

	public bool TryMoveRight()
	{
		var length = FieldModel.Field.Count;
		var isLose = true;

		for (var i = 0; i < length; i++)
		{
			var col = length - 1;

			for (var j = length - 2; j >= 0; j--)
			{
				if (FieldModel.Field[i][j].IsNone())
				{
					isLose = false;
					continue;
				}

				if (FieldModel.Field[i][col].IsNone())
				{
					FieldModel.Field[i][col].Value = FieldModel.Field[i][j].Value;
					if (j != col)
						FieldModel.Field[i][j].Value = CellValues.None;
					isLose = false;
				}
				else if (FieldModel.Field[i][col].Value == FieldModel.Field[i][j].Value)
				{
					var newValue = (CellValues) ((int) FieldModel.Field[i][col].Value * 2);
					FieldModel.Field[i][col].Value = newValue;
					FieldModel.Field[i][j].Value = CellValues.None;
					col--;
					CurrentScore += (int) newValue;
					isLose = false;
				}
				else
				{
					col--;
					if (col == j)
						continue;

					FieldModel.Field[i][col].Value = FieldModel.Field[i][j].Value;
					FieldModel.Field[i][j].Value = CellValues.None;
					isLose = false;
				}
			}
		}

		return isLose;
	}

	public bool TryMoveDown()
	{
		var length = FieldModel.Field.Count;
		var isLose = true;

		for (var j = 0; j < length; j++)
		{
			var row = length - 1;

			for (var i = length - 2; i >= 0; i--)
			{
				if (FieldModel.Field[i][j].IsNone())
				{
					isLose = false;
					continue;
				}

				if (FieldModel.Field[row][j].IsNone())
				{
					FieldModel.Field[row][j].Value = FieldModel.Field[i][j].Value;
					if (i != row)
						FieldModel.Field[i][j].Value = CellValues.None;
					isLose = false;
				}
				else if (FieldModel.Field[row][j].Value == FieldModel.Field[i][j].Value)
				{
					var newValue = (CellValues) ((int) FieldModel.Field[row][j].Value * 2);
					FieldModel.Field[row][j].Value = newValue;
					FieldModel.Field[i][j].Value = CellValues.None;
					row--;
					CurrentScore += (int) newValue;
					isLose = false;
				}
				else
				{
					row--;
					if (row == i)
						continue;

					FieldModel.Field[row][j].Value = FieldModel.Field[i][j].Value;
					FieldModel.Field[i][j].Value = CellValues.None;
					isLose = false;
				}
			}
		}

		return isLose;
	}

	public bool TryMoveLeft()
	{
		var length = FieldModel.Field.Count;
		var isLose = true;

		for (var i = 0; i < length; i++)
		{
			var col = 0;

			for (var j = 1; j < length; j++)
			{
				if (FieldModel.Field[i][j].IsNone())
				{
					isLose = false;
					continue;
				}

				if (FieldModel.Field[i][col].IsNone())
				{
					FieldModel.Field[i][col].Value = FieldModel.Field[i][j].Value;
					if (j != col)
						FieldModel.Field[i][j].Value = CellValues.None;
					isLose = false;
				}
				else if (FieldModel.Field[i][col].Value == FieldModel.Field[i][j].Value)
				{
					var newValue = (CellValues) ((int) FieldModel.Field[i][col].Value * 2);
					FieldModel.Field[i][col].Value = newValue;
					FieldModel.Field[i][j].Value = CellValues.None;
					col++;
					CurrentScore += (int) newValue;
					isLose = false;
				}
				else
				{
					col++;
					if (col == j)
						continue;

					FieldModel.Field[i][col].Value = FieldModel.Field[i][j].Value;
					FieldModel.Field[i][j].Value = CellValues.None;
					isLose = false;
				}
			}
		}

		return isLose;
	}

	public bool TryMoveUp()
	{
		var length = FieldModel.Field.Count;
		var isLose = true;

		for (var j = 0; j < length; j++)
		{
			var row = 0;

			for (var i = 1; i < length; i++)
			{
				if (FieldModel.Field[i][j].IsNone())
				{
					isLose = false;
					continue;
				}

				if (FieldModel.Field[row][j].IsNone())
				{
					FieldModel.Field[row][j].Value = FieldModel.Field[i][j].Value;
					if (i != row)
						FieldModel.Field[i][j].Value = CellValues.None;
					isLose = false;
				}
				else if (FieldModel.Field[row][j].Value == FieldModel.Field[i][j].Value)
				{
					var newValue = (CellValues) ((int) FieldModel.Field[row][j].Value * 2);
					FieldModel.Field[row][j].Value = newValue;
					FieldModel.Field[i][j].Value = CellValues.None;
					row++;
					CurrentScore += (int) newValue;
					isLose = false;
				}
				else
				{
					row++;
					if (row == i)
						continue;

					FieldModel.Field[row][j].Value = FieldModel.Field[i][j].Value;
					FieldModel.Field[i][j].Value = CellValues.None;
					isLose = false;
				}
			}
		}

		return isLose;
	}

	private void OnExit() // вызов методя для выхода из игры
	{
		Game.Exit();
		if (User.HighScore < CurrentScore)
			User.HighScore = CurrentScore;
		_messenger.Send(new RequestCloseMessage(this, null));
	}
	private void Restart() // Вызов метода для рестарта игры
	{
		Game.Restart();

		CurrentScore = 0;
		FieldModel.Clear();
		OnContentRendered();
	}

	public interface IFactory
	{
		GameViewModel Create(UserModel user);
	}
}