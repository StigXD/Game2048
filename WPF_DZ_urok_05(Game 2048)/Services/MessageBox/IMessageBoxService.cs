﻿using System.Windows;

namespace Game_2048.Services.MessageBox;

public interface IMessageBoxService
{
	public void Show(string text);
	public void Show(string text, string caption);
	public void Show(string text, string caption, MessageBoxButton button);
	public void Show(string text, string caption, MessageBoxButton button, MessageBoxImage image);
	public void Show(string text, string caption, MessageBoxButton button, MessageBoxImage image, MessageBoxResult defaultResult);
	public void Show(string text, string caption, MessageBoxButton button, MessageBoxImage image, MessageBoxResult defaultResult, MessageBoxOptions options);
}