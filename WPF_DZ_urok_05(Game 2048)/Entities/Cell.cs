using Game_2048.Enums;

namespace Game_2048.Entities;

public class Cell
{
	public CellValues Value { get; set; }

	public Cell()
	{
		Value = CellValues.None;
	}
}