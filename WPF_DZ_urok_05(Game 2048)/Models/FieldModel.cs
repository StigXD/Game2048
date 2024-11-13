using System.Collections.ObjectModel;
using System.Linq;
using Game_2048.Enums;

namespace Game_2048.Models;

public class FieldModel
{
	public ObservableCollection<ObservableCollection<CellModel>> Field { get; set; }

	public FieldModel()
	{
		Field = new ObservableCollection<ObservableCollection<CellModel>>();

		for (var i = 0; i < Constants.FieldSize; i++)
		{
			var row = new ObservableCollection<CellModel>();

			for (var j = 0; j < Constants.FieldSize; j++)
				row.Add(new CellModel());

			Field.Add(row);
		}
	}
	public void Clear()
	{
		foreach (var cell in Field.SelectMany(row => row))
			cell.Value = CellValues.None;
	}
}