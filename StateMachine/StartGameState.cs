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
		ButtonElement btIconPlayer1; // O
		ButtonElement btIconPlayer2; // X
		TextElement btTipoPlayer1;
		TextElement btTipoPlayer2;
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
			btIconPlayer1 = new ButtonElement(game, "", new Vector2(game.spriteP1.Width, game.spriteP1.Height), new Vector2(0, 0), null, game.spriteP1);
			btIconPlayer2 = new ButtonElement(game, "", new Vector2(game.spriteP2.Width, game.spriteP2.Height), new Vector2(0, 0), null, game.spriteP2);
			btTipoPlayer1 = new ButtonElement(game, "player 1");
			btTipoPlayer2 = new ButtonElement(game, "player 2");
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

			btTipoPlayer1.align = "center";
			btTipoPlayer1.textAlign = "center";
			btTipoPlayer2.align = "center";
			btTipoPlayer2.textAlign = "center";

			btIconPlayer1.Margin(0,50,0,0);
			btIconPlayer1.align="center";
			btIconPlayer2.Margin(0,50,0,0);
			btIconPlayer2.align="center";
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

			btIconPlayer1.AddEventListener("click",delegate (Event e){
				if(game.spriteP1==game.cellO){
					game.spriteP1=game.cellX;
					game.spriteP2=game.cellO;
				} else {
					game.spriteP1=game.cellO;
					game.spriteP2=game.cellX;
				}
				btIconPlayer1.backgroundImage=game.spriteP1;
				btIconPlayer2.backgroundImage=game.spriteP2;
			});
				

			btTipoPlayer1.AddEventListener("click",delegate(Event e) {
				if(game.player1.npc){
					game.player1.npc = false;
					btTipoPlayer1.text = "player 1";	
					btDifPlayer1.display="none";
				} else {
					game.player1.npc = true;
					btTipoPlayer1.text = "player 1 (npc)";
					btDifPlayer1.display="block";
				}
			});

			btIconPlayer2.AddEventListener("click",delegate (Event e){
				if(game.spriteP1==game.cellO){
					game.spriteP1=game.cellX;
					game.spriteP2=game.cellO;
				} else {
					game.spriteP1=game.cellO;
					game.spriteP2=game.cellX;
				}
				btIconPlayer1.backgroundImage=game.spriteP1;
				btIconPlayer2.backgroundImage=game.spriteP2;
			});



            btTipoPlayer2.AddEventListener("click",delegate(Event e) {
				if(game.player2.npc){
					game.player2.npc = false;
					btTipoPlayer2.text = "player 2";
					btDifPlayer2.display="none";
				} else {
					game.player2.npc = true;
					btTipoPlayer2.text = "player 2 (npc)";
					btDifPlayer2.display="block";
				}
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
				
			btIconPlayer2.hoverFgColor(Color.Red);
			btTipoPlayer2.hoverFgColor(Color.Red);
			btDifPlayer1.hoverFgColor(Color.Red);

			btIconPlayer2.hoverFgColor(Color.Purple);
			btTipoPlayer2.hoverFgColor(Color.Purple);
			btDifPlayer2.hoverFgColor(Color.Purple);

			btStartGame.hoverFgColor(Color.Green);
			btOptions.hoverFgColor(Color.Green);
				
			divPlayer1.Append(btIconPlayer1);
			divPlayer1.Append(btTipoPlayer1);
			divPlayer1.Append(btDifPlayer1);
			divPlayer2.Append(btIconPlayer2);
			divPlayer2.Append(btTipoPlayer2);
			divPlayer2.Append(btDifPlayer2);

			view.Append(divPlayer1);
			view.Append(divPlayer2);
			view.Append(btOptions);
			view.Append(btStartGame);
		}

		public override void Enter(string lastState){
			if(game.player1.npc) {
				btDifPlayer1.display = "block";
				btTipoPlayer1.text = "player 1 (npc)";
			} else {
				btDifPlayer1.display = "none";
				btTipoPlayer1.text = "player 1";
			}
			if(game.player2.npc) {
				btDifPlayer2.display = "block";
				btTipoPlayer2.text = "player 2 (npc)";
			} else {
				btDifPlayer2.display = "none";
				btTipoPlayer2.text = "player 2";
			}

		}
			
	}
}

