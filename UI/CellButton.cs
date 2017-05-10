using System;
using Microsoft.Xna.Framework;

namespace Minimax
{
	public class CellButton : ButtonElement
	{

		public int[] cell = new int[2]; 


		public CellButton(Game1 g, Vector2 s, Vector2 p, int[] c): base(g,"",s,p){
			cell = c;
		}

		public CellButton (Game1 g, int[] c) : base(g,"",null,null){
			cell = c;
		}

		/*public override bool OnClick(Event e){
			if (detectInteracion(e.vVector)) {
				if (!active) {
					if (game.board.cell [cell[0], cell[1]] == 0) {
						updateCell (game.actualPlayer.playerNumber);
						return true;
					}
				}
			}
			return false;
		}*/

		public void updateCell(int player){
			game.board.cell[cell[0], cell[1]] = player;
			active = true;
			if (game.actualPlayer.playerNumber == 1) {
				backgroundImage = game.spriteP1;
				game.actualPlayer = game.player2;
			} else {
				backgroundImage = game.spriteP2;
				game.actualPlayer = game.player1;
			}
			Console.WriteLine(cell[0]+","+cell[1]+" = "+game.board.cell[cell[0], cell[1]]);
		}


		public void Clear(){
			backgroundImage = null;
			backgroundColor = Color.Transparent;
			active = false;
		}

	}
}

