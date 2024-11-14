using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_2048_.Logic
{
    public interface IGameLogic
	{
		public void NewField();
		public void GetNewValue();
		public void PlayStep();
        public void Restart();
        public void Exit();
	}
}
