using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Minimax
{
	public class StartGameState : GameState
	{

		ButtonElement btStartGame;
		ButtonElement btOptions;
		DivElement divPlayer1;
		DivElement divPlayer2;
		ButtonElement btPlayer1; // O
		ButtonElement btPlayer2; // X
		TextElement tipoPlayer1;
		TextElement tipoPlayer2;
		TextElement labelDifPlayer1;
		TextElement labelDifPlayer2;
		ButtonElement btDifPlayer1;
		ButtonElement btDifPlayer2;

		enum difficulty {easy=0,medium=50,hard=100};


		public StartGameState(Game1 g, string n): base(g,n)
		{
			
			btStartGame = new ButtonElement(game,"start");
			btOptions = new ButtonElement(game,"options");
			divPlayer1 = new DivElement(game,new Vector2(200,300)/*,game.cellEmpty*/);
			divPlayer2 = new DivElement(game,new Vector2(200,300)/*,game.cellEmpty*/);
			btPlayer1 = new ButtonElement(game, "", new Vector2(game.cellO.Width, game.cellO.Height), new Vector2(0, 0), null, game.cellO);
			btPlayer2 = new ButtonElement(game, "", new Vector2(game.cellX.Width, game.cellX.Height), new Vector2(0, 0), null, game.cellX);
			tipoPlayer1 = new TextElement(game, "player 1");
			tipoPlayer2 = new TextElement(game, "player 2");
			labelDifPlayer1 = new TextElement(game, "dificuldade:");
			labelDifPlayer2 = new TextElement(game, "dificuldade:");
			btDifPlayer1 = new ButtonElement(game, "hard");
			btDifPlayer2 = new ButtonElement(game, "hard");


			btStartGame.Align("center","bottom");
			btOptions.Align("center","middle");
			divPlayer1.align = "left";
			divPlayer1.vAlign = "top";
			divPlayer1.Margin(60, 60, 0, 0);
			divPlayer2.align = "right";
			divPlayer2.Margin(0, 60, 60, 0);
			divPlayer2.vAlign = "top";

			tipoPlayer1.align = "center";
			tipoPlayer1.textAlign = "center";
			tipoPlayer2.align = "center";
			tipoPlayer2.textAlign = "center";

			btPlayer1.Margin(0,50,0,0);
			btPlayer1.align="center";
			btPlayer2.Margin(0,50,0,0);
			btPlayer2.align="center";
			labelDifPlayer1.align = "center";
			labelDifPlayer1.textAlign = "center";
			labelDifPlayer2.align = "center";
			labelDifPlayer2.textAlign = "center";

			btDifPlayer1.align="center";
			btDifPlayer1.textAlign="center";
			btDifPlayer1.display = "none";
			btDifPlayer2.align="center";
			btDifPlayer2.textAlign="center";
			btDifPlayer1.display = "none";

			btStartGame.AddEventListener("click",delegate(Event e) {
				game.GameMode.Change("play");
			});

			btOptions.AddEventListener("click",delegate (Event e){
				game.GameMode.Change("menu");
			});

            btPlayer1.AddEventListener("click",delegate(Event e) {
				if(game.player1.npc){
					game.player1.npc = false;
					tipoPlayer1.text = "player 1";	
					btDifPlayer1.display="none";
				} else {
					game.player1.npc = true;
					tipoPlayer1.text = "player 1 (npc)";
					btDifPlayer1.display="block";
				}
			});

            btPlayer1.AddEventListener("mouseover", delegate (Event e){
				e.target.foregroundColor = Color.Red;
            });

            btPlayer1.AddEventListener("mouseout", delegate (Event e) {
				e.target.foregroundColor = Color.White;
            });

            btPlayer2.AddEventListener("click",delegate(Event e) {
				if(game.player2.npc){
					game.player2.npc = false;
					tipoPlayer2.text = "player 2";
					btDifPlayer2.display="none";
				} else {
					game.player2.npc = true;
					tipoPlayer2.text = "player 2 (npc)";
					btDifPlayer2.display="block";
				}
			});

			btPlayer2.AddEventListener("mouseover", delegate (Event e){
				e.target.foregroundColor = Color.Purple;
			});

			btPlayer2.AddEventListener("mouseout", delegate (Event e) {
				e.target.foregroundColor = Color.White;
			});
				
			btDifPlayer1.AddEventListener("click",delegate(Event e) {
				if(game.player1.difficulty==100){
					game.player1.difficulty = 50;
					btDifPlayer1.text="medium";
				} else if(game.player1.difficulty==50){
					game.player1.difficulty = 100;
					btDifPlayer1.text="hard";
				} 
			});

			btDifPlayer2.AddEventListener("click",delegate(Event e) {				
				if(game.player2.difficulty==100){
					game.player2.difficulty = 50;
					btDifPlayer2.text="medium";
				} else if(game.player2.difficulty==50){
					game.player2.difficulty = 100;
					btDifPlayer2.text="hard";
				} 
			});


			btDifPlayer1.hoverFgColor(Color.Red);
			btDifPlayer2.hoverFgColor(Color.Purple);
			btStartGame.hoverFgColor(Color.Green);
				
			divPlayer1.Append(btPlayer1);
			divPlayer1.Append(tipoPlayer1);
			divPlayer1.Append(btDifPlayer1);
			divPlayer2.Append(btPlayer2);
			divPlayer2.Append(tipoPlayer2);
			divPlayer2.Append(btDifPlayer2);

			view.Append(divPlayer1);
			view.Append(divPlayer2);
			view.Append(btOptions);
			view.Append(btStartGame);

		}

		public override void Enter(string lastState){
			if(game.player1.npc) {
				btDifPlayer1.display = "block";
				tipoPlayer1.text = "player 1 (npc)";
			} else {
				btDifPlayer1.display = "none";
				tipoPlayer1.text = "player 1";
			}
			if(game.player2.npc) {
				btDifPlayer2.display = "block";
				tipoPlayer2.text = "player 2 (npc)";
			} else {
				btDifPlayer2.display = "none";
				tipoPlayer2.text = "player 2";
			}

		}
			
	}
}

