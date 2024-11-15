﻿using System;
using System.Windows;
using Game_2048.Base;

namespace Game_2048.Services.View;

public interface IViewService
{
	[Obsolete("Use ViewModelExtensions")]
	void OpenWindow<TViewModel>(WindowMode windowMode = WindowMode.LastActiveOwner) where TViewModel : IViewModel;
	[Obsolete("Use ViewModelExtensions")]
	void OpenWindow(IViewModel viewModel, WindowMode windowMode = WindowMode.LastActiveOwner);

	[Obsolete("Use ViewModelExtensions")]
	bool? OpenDialog<TViewModel>(WindowMode windowMode = WindowMode.LastActiveOwner) where TViewModel : IViewModel;
	[Obsolete("Use ViewModelExtensions")]
	bool? OpenDialog(IViewModel viewModel, WindowMode windowMode = WindowMode.LastActiveOwner);

	Window CreateWindow<TViewModel>(WindowMode windowMode) where TViewModel : IViewModel;
	Window CreateWindow(IViewModel viewModel, WindowMode windowMode);

	int GetOpenedWindowsCount();
}