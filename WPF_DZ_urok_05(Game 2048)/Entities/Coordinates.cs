﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_2048.Entities;

public class Coordinates
{
	public int Row { get; set; }
	public int Column { get; set; }

	public Coordinates(int row, int column)
	{
		Row = row;
		Column = column;
	}

}
