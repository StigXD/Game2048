﻿using System;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Messaging;
using Game_2048.Database;
using Grace.DependencyInjection;
using Grace.Factory;
using Game_2048.Dispatcher;
using Game_2048.Services.MessageBox;
using Game_2048.Services.View;
using Game_2048.Views.GameWindow.Logic;
using Game_2048.Views.AuthenticationWindow.Logic;

namespace Game_2048;

public class Locator : ILocatorService
{
	private readonly DependencyInjectionContainer _container;
	private static readonly Lazy<Locator> Lazy = new(() => new Locator());

	public static Locator Current => Lazy.Value;
	public static string FactoryName => "IFactory";

	private Locator()
	{
		_container = new DependencyInjectionContainer();
		_container.Configure(RegisterDependencies);
	}

	private static void RegisterDependencies(IExportRegistrationBlock registration)
	{
		registration.ExportInterfaceFactories(type => type.Name == FactoryName);

		RegisterServices(registration);
	}

	private static void RegisterServices(IExportRegistrationBlock registration)
	{
		RegisterSingleton<IMessenger, Messenger>(registration);
		RegisterSingleton<IViewService, ViewService>(registration);
		RegisterSingleton<IDispatcherHelper, DispatcherHelper>(registration);
		RegisterSingleton<IMessageBoxService, MessageBoxService>(registration);
		RegisterSingleton<IUsersDB, UsersDB> (registration);
		RegisterSingleton<IGameWindowProvider, GameWindowProvider>(registration);
		RegisterSingleton<IAuthWindowProvider, AuthWindowProvider>(registration);
	}

	private static void RegisterSingleton<TFrom, TTo>(IExportRegistrationBlock registrationBlock) where TTo : TFrom
	{
		registrationBlock.Export<TTo>().As<TFrom>().Lifestyle.Singleton();
	}

	public object GetService(Type serviceType)
	{
		return _container.Locate(serviceType);
	}
	public bool CanLocate(Type type, ActivationStrategyFilter consider = null, object key = null)
	{
		return _container.CanLocate(type, consider, key);
	}
	public object Locate(Type type)
	{
		return _container.Locate(type);
	}
	public object LocateOrDefault(Type type, object defaultValue)
	{
		return _container.LocateOrDefault(type, defaultValue);
	}
	public T Locate<T>()
	{
		return _container.Locate<T>();
	}
	public T LocateOrDefault<T>(T defaultValue = default)
	{
		return _container.LocateOrDefault(defaultValue);
	}
	public List<object> LocateAll(Type type, object extraData = null, ActivationStrategyFilter consider = null, IComparer<object> comparer = null)
	{
		return _container.LocateAll(type, extraData, consider, comparer);
	}
	public List<T> LocateAll<T>(Type type = null, object extraData = null, ActivationStrategyFilter consider = null, IComparer<T> comparer = null)
	{
		return _container.LocateAll(type, extraData, consider, comparer);
	}
	public bool TryLocate<T>(out T value, object extraData = null, ActivationStrategyFilter consider = null, object withKey = null, bool isDynamic = false)
	{
		return _container.TryLocate(out value, extraData, consider, withKey, isDynamic);
	}
	public bool TryLocate(Type type, out object value, object extraData = null, ActivationStrategyFilter consider = null, object withKey = null, bool isDynamic = false)
	{
		return _container.TryLocate(type, out value, extraData, consider, withKey, isDynamic);
	}
	public object LocateByName(string name, object extraData = null, ActivationStrategyFilter consider = null)
	{
		return _container.LocateByName(name, extraData, consider);
	}
	public List<object> LocateAllByName(string name, object extraData = null, ActivationStrategyFilter consider = null)
	{
		return _container.LocateAllByName(name, extraData, consider);
	}
	public bool TryLocateByName(string name, out object value, object extraData = null, ActivationStrategyFilter consider = null)
	{
		return _container.TryLocateByName(name, out value, extraData, consider);
	}
	// ReSharper disable MethodOverloadWithOptionalParameter
	public object Locate(Type type, object extraData = null, ActivationStrategyFilter consider = null, object withKey = null, bool isDynamic = false)
	{
		return _container.Locate(type, extraData, consider, withKey, isDynamic);
	}
	public T Locate<T>(object extraData = null, ActivationStrategyFilter consider = null, object withKey = null, bool isDynamic = false)
	{
		return _container.Locate<T>(extraData, consider, withKey, isDynamic);
	}
	// ReSharper restore MethodOverloadWithOptionalParameter
}