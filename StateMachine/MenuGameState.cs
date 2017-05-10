using System;
using Microsoft.Xna.Framework;

namespace Minimax
{
	public class MenuGameState : GameState
	{



		TextElement lblBoardSize;
		TextElement titleBoardSize;
		TextElement lblDepth;
		TextElement titleDepth;
		ButtonElement plusBoardSize;
		ButtonElement minusBoardSize;
		ButtonElement plusDepth;
		ButtonElement minusDepth;
		ButtonElement btDiagonalFromHell;
		ButtonElement save;
		ButtonElement quit;

		//TextElement lblAlphabeta;
		int size;
		int depth;
		bool diagonalFromHell;

		public MenuGameState(Game1 g, string n): base(g,n){

			titleBoardSize = new TextElement(game,"Board Size:",game.ff6_32);
			titleBoardSize.position="absolute";
			titleBoardSize.pos = new Vector2(30,30);

			titleDepth = new TextElement(game,"Search Depth:",game.ff6_32);
			titleDepth.position="absolute";
			titleDepth.pos = new Vector2(30,130);

			plusBoardSize = new ButtonElement(game,"+", game.ff6_32);
			minusBoardSize = new ButtonElement(game,"-", game.ff6_32);
			plusDepth = new ButtonElement(game,"+", game.ff6_32);
			minusDepth = new ButtonElement(game,"-",game.ff6_32);
			save = new ButtonElement(game, "SAVE",game.ff6_32);
			quit = new ButtonElement(game, "CANCEL",game.ff6_32);
			lblBoardSize = new TextElement(game, "a",game.ff6_32);
			lblDepth = new TextElement(game, "a",game.ff6_32);


			diagonalFromHell = game.board.superTicTacToeDiagonalFromHell;
			btDiagonalFromHell = new ButtonElement(game,"SuperTicTacToeDiagonalFromHell INACTIVE",game.ff6_32);
			btDiagonalFromHell.align="center";
			btDiagonalFromHell.Padding(40,0,40,0);
			btDiagonalFromHell.Margin(0,80,0,0);
			btDiagonalFromHell.setSound();
			btDiagonalFromHell.hover(game.hand);
			btDiagonalFromHell.AddEventListener("click",delegate(Event e) {
				if(diagonalFromHell){
					diagonalFromHell=false;
					btDiagonalFromHell.text="SuperTicTacToeDiagonalFromHell INACTIVE";
				} else {
					diagonalFromHell=true;
					btDiagonalFromHell.text="SuperTicTacToeDiagonalFromHell ACTIVE";
				}
			});


			minusBoardSize.position="absolute";
			minusBoardSize.pos = new Vector2(40, 80);
			minusBoardSize.setSound(false);
			lblBoardSize.position="absolute";
			lblBoardSize.pos = new Vector2(70, 80);
			plusBoardSize.position="absolute";
			plusBoardSize.pos = new Vector2(100, 80);
			plusBoardSize.setSound(false);

			minusDepth.position="absolute";
			minusDepth.pos = new Vector2(40, 180);
			minusDepth.setSound(false);
			lblDepth.position="absolute";
			lblDepth.pos = new Vector2(70, 180);
			plusDepth.position="absolute";
			plusDepth.pos = new Vector2(100, 180);
			plusDepth.setSound(false);


			plusBoardSize.AddEventListener("click",delegate(Event e) {
				if(size<11){
					size++;
				}
				lblBoardSize.text=size+"";
			});
			minusBoardSize.AddEventListener("click",delegate(Event e) {
				if(size>3){
					size--;
					if(depth>size*size){
						depth=size*size;
						lblDepth.text = depth+"";
					}
				}
				lblBoardSize.text=size+"";
			});



			plusDepth.AddEventListener("click",delegate(Event e) {
				if(depth<size*size){
					depth++;
				}
				lblDepth.text=depth+"";
			});
			minusDepth.AddEventListener("click",delegate(Event e) {
				if(depth>3){
					depth--;
				}
				lblDepth.text=depth+"";
			});
			quit.AddEventListener("click",delegate(Event e){
				game.GameMode.Change("title");
			});
			quit.setSound();
			quit.hover(game.hand);

			save.AddEventListener("click",delegate(Event e) {
				if(size!=game.board.size){
					game.board.UpdateSize(size);
					game.GameMode.Set("play", new PlayGameState(game,"play"));
					game.GameMode.Set("end", new EndGameState(game,"end"));
				}
				game.depth = depth;
				//game.alphabeta = alphabeta;
				game.board.superTicTacToeDiagonalFromHell = diagonalFromHell;
				game.GameMode.Change("title");
			});
			save.setSound();
			save.hover(game.hand);

			save.Align("left", "bottom");
			quit.Align("right", "bottom");

			save.Padding(40, 0, 40, 0);
			quit.Padding(40, 0, 40, 0);
			save.Margin(0, 0, 20, 20);
			quit.Margin(0, 0, 40, 20);

			view.Append(titleBoardSize);
			view.Append(minusBoardSize);
			view.Append(lblBoardSize);
			view.Append(plusBoardSize);
			view.Append(titleDepth);
			view.Append(minusDepth);
			view.Append(lblDepth);
			view.Append(plusDepth);
			view.Append(btDiagonalFromHell);
			view.Append(save);
			view.Append(quit);

			view.backgroundImage = game.menu3;

			Console.WriteLine(((TextElement) plusBoardSize.previousNode).calcPosition());
		}

		public override void Enter(string lastState){
			size = game.board.size;
			depth = game.depth;
			Console.WriteLine(game.board.superTicTacToeDiagonalFromHell);
			diagonalFromHell = game.board.superTicTacToeDiagonalFromHell;

			btDiagonalFromHell.text = "SuperTicTacToeDiagonalFromHell INACTIVE";
			if(diagonalFromHell) {
				btDiagonalFromHell.text = "SuperTicTacToeDiagonalFromHell ACTIVE";
			}
			lblBoardSize.text = size+"";
			lblDepth.text = depth+"";
		}

	}
}

