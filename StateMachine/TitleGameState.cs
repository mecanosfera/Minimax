using System;
using Microsoft.Xna.Framework;

namespace Minimax{
	
	public class TitleGameState : GameState{


		DivElement gameTitle;
		ButtonElement newGame;
		ButtonElement options;
		ButtonElement quit;


		public TitleGameState(Game1 g, string n): base(g,n){

			gameTitle = new DivElement(game, new Vector2(424,154), game.spriteGameTitle);
			newGame = new ButtonElement(game, "New Game", game.ff6_32);
			options = new ButtonElement(game, "Options", game.ff6_32);
			quit = new ButtonElement(game, "Quit", game.ff6_32);

			gameTitle.align="center";
			gameTitle.Margin(20);

			newGame.align="center";
			newGame.Margin(0,200,0,10);
			newGame.Padding(40, 0, 40, 0);
			options.align="center";
			options.Margin(10);
			options.Padding(40, 0, 40, 0);
			quit.align="center";
			quit.Margin(10);
			quit.Padding(40, 0, 40, 0);

			view.backgroundImage = game.menu3;

			view.Append(gameTitle);
			view.Append(newGame);
			view.Append(options);
			view.Append(quit);

			newGame.AddEventListener("click",delegate(Event e) {
				game.GameMode.Change("start");
			});
				
			newGame.hover(game.hand);
			newGame.setSound();

			options.AddEventListener("click",delegate(Event e) {
				game.GameMode.Change("menu");
			});

			options.setSound();

			options.hover(game.hand);

			quit.AddEventListener("click",delegate(Event e) {
				Exit();
			});

			quit.setSound();

			quit.hover(game.hand);

		}

		public override void Enter(string lastState=null){
			game.victoryPlay.Stop();
			game.battlePlay.Stop();
			game.bossPlay.Stop();
			game.menuPlay.Play();
		}
	}
}

