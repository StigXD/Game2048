using System;
using GalaSoft.MvvmLight;
using Game_2048.Enums;

namespace Game_2048.Models;

public class CellModel : ObservableObject, ICloneable
{
	public CellValues _value;

	public CellValues Value
	{
		get => _value;
		set => Set(ref _value, value);
	}

	public bool IsNone() => Value == CellValues.None;
	public object Clone()
	{
		var clone = (CellModel) MemberwiseClone();
		return clone;
	}
}