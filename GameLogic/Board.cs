using System;
using System.Collections.Generic;

namespace Minimax
{
	public class Board
	{
		public int[,] cell = new int[3, 3];

        public Board()
		{
			Clear();
		}
			
		public void Clear()
		{
			for (int y = 0; y < 3; y++)
			{
				for (int x = 0; x < 3; x++)
				{
					cell[x, y] = 0;
                    //left = 9;
				}
			}
		}


        public int Victory()
        {
            return CheckState(cell);
        }


        public int getLeft()
        {
            int left = 0;
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    if (cell[x, y] == 0)
                    {
                        left++;
                    }
                }
            }
            return left;
        }

       
		public int CheckState(int[,] cells) {

			for (int y = 0; y < 3; y++)
			{
				if (cells[0, y] == cells[1, y] && cells[1, y] == cells[2, y])
				{
					return cells[0, y];
				}
			}
			for (int x = 0; x < 3; x++)
			{
				if (cells[x, 0] == cells[x, 1] && cells[x, 1] == cells[x, 2])
				{
					return cells[x, 0];
				}
			}
			if ((cells[0, 0] == cells[1, 1] && cells[1, 1] == cells[2, 2]) || (cells[0, 2] == cells[1, 1] && cells[1, 1] == cells[2, 0]))
			{
				return cells[1, 1];
			}

			if (getLeft() == 0)
			{
				return 3;
			}
			return 0;
		}

        public bool IsGameOver()
        {
            if (Victory()>0) {
                return true;
            }
            return false;
        }


		public int GetScore(Player player)
        {
            int winner = Victory();

			if (winner == player.playerNumber)
                return 1;
			else if (winner == player.opponentNumber)
                return -1;
            else
                return 0;
        }


        public Board GetCopy()
        {
            Board myCopy = new Board();

            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    myCopy.cell[x, y] = cell[x, y];
                }
            }

            return myCopy;
        }
			

		public List<Board> GetPossibilities(int player)
        {
            List<Board> result = new List<Board>();

            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    if (cell[x, y] == 0)
                    {
                        Board myCopy = GetCopy();
						myCopy.cell[x, y] = player;
                        result.Add(myCopy);
                    }
                }
            }
            return result;
        }



	}
}
