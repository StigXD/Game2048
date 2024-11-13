using System.Collections.Generic;
using System.Windows.Media;
using Game_2048.Enums;

namespace Game_2048;

public static class Utile
{
	public static readonly Dictionary<CellValues, Brush> stateValueColor = new()
	{
		{ CellValues.None, Brushes.AntiqueWhite },
		{ CellValues.Two, Brushes.LimeGreen },
		{ CellValues.Four, Brushes.DarkRed },
		{ CellValues.Eight, Brushes.MediumVioletRed },
		{ CellValues.Sixteen, Brushes.DarkViolet },
		{ CellValues.ThirtyTwo, Brushes.CornflowerBlue },
		{ CellValues.SixtyFour, Brushes.GreenYellow },
		{ CellValues.OneHundredTwentyEight, Brushes.DodgerBlue },
		{ CellValues.TwoHundredFiftySix, Brushes.MediumSeaGreen },
		{ CellValues.FiveHundredTwelve, Brushes.PaleVioletRed },
		{ CellValues.OneThousandTwentyFour, Brushes.SaddleBrown },
		{ CellValues.TwoThousandFortyEight, Brushes.BlueViolet },
	};
}