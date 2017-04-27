using System;

namespace Minimax
{
	public class MenuGameState : GameState
	{

		ButtonElement plusBoardSize;
		ButtonElement minusBoardSize;
		ButtonElement plusDepth;
		ButtonElement minusDepth;
		ButtonElement btAlphabeta;
		ButtonElement save;
		ButtonElement quit;
		TextElement lblBoardSize;
		TextElement lblDepth;
		//TextElement lblAlphabeta;
		int size;
		int depth;
		bool alphabeta;


		public MenuGameState(Game1 g, string n): base(g,n)
		{

			plusBoardSize = new ButtonElement(game,"+");
			minusBoardSize = new ButtonElement(game,"-");
			plusDepth = new ButtonElement(game,"+");
			minusDepth = new ButtonElement(game,"-");
			btAlphabeta = new ButtonElement(game,"a");
			save = new ButtonElement(game, "save");
			quit = new ButtonElement(game, "cancel");
			lblBoardSize = new TextElement(game, "a");
			lblDepth = new TextElement(game, "a");

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
				game.GameMode.Change("start");
			});
			save.AddEventListener("click",delegate(Event e) {
				if(size!=game.board.size){
					game.board.UpdateSize(size);
					game.GameMode.Set("play", new PlayGameState(game,"play"));
					game.GameMode.Set("end", new EndGameState(game,"end"));
				}
				game.depth = depth;
				game.alphabeta = alphabeta;
				game.GameMode.Change("start");
			});


			view.Append(minusBoardSize);
			view.Append(lblBoardSize);
			view.Append(plusBoardSize);
			view.Append(minusDepth);
			view.Append(lblDepth);
			view.Append(plusDepth);
			view.Append(btAlphabeta);
			view.Append(save);
			view.Append(quit);
		}

		public override void Enter(string lastState){
			size = game.board.size;
			depth = game.depth;
			alphabeta = game.alphabeta;
			string txtab = "alpha-beta active";
			if(!alphabeta) {
				txtab = "alpha-beta inactive";
			}
			btAlphabeta.text = txtab;
			lblBoardSize.text = size+"";
			lblDepth.text = depth+"";
		}

	}
}

