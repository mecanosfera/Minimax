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
		ButtonElement btPlayer1; // O
		ButtonElement btPlayer2; // X
		TextElement tipoPlayer1;
		TextElement tipoPlayer2;
		ButtonElement btAddDifPlayer1;
		ButtonElement btAddDifPlayer2;
		ButtonElement btSubDifPlayer1;
		ButtonElement btSubDifPlayer2;
		TextElement difPlayer1;
		TextElement difPlayer2;


		public StartGameState(Game1 g, string n): base(g,n)
		{
			btStartGame = new ButtonElement(game,"start");
			btStartGame.Align("center","bottom");
			btStartGame.AddEventListener("click",delegate(DivElement origin, Event e) {
				game.GameMode.Change("play");
			});

			int leftMargin = 50;
			int rightMargin = 50;
			int topMargin = 30;

			btPlayer1 = new ButtonElement(game, "", new Vector2(game.cellO.Width, game.cellO.Height), new Vector2(10, 10), null, game.cellO);
			btPlayer2 = new ButtonElement(game, "", new Vector2(game.cellX.Width, game.cellX.Height), new Vector2(10, 10), null, game.cellX);
			tipoPlayer1 = new TextElement(game, "player", new Vector2(game.cellEmpty.Width, 20), new Vector2(70, 70));
			tipoPlayer2 = new TextElement(game, "player", new Vector2(game.cellEmpty.Width, 20), new Vector2(70, 70));
			tipoPlayer1.align = "left";
			tipoPlayer2.align = "right";
			tipoPlayer1.textAlign = "center";
			tipoPlayer2.textAlign = "center";
			tipoPlayer1.Margin(leftMargin, topMargin, 0, 0);
			tipoPlayer2.Margin(0, topMargin, rightMargin, 0);
			btPlayer1.Margin(leftMargin, topMargin, 0, 0);
			btPlayer2.Margin(0, topMargin, rightMargin, 0);
				
			btSubDifPlayer1 = new ButtonElement(game,"-",new Vector2(20,20));
			difPlayer1 = new TextElement(game,game.player1.difficulty+"",new Vector2(30,20),new Vector2(30,0));
			btAddDifPlayer1 = new ButtonElement(game,"+",new Vector2(20,20),new Vector2(70,0));
			btSubDifPlayer1.align="left";
			btSubDifPlayer1.Margin(leftMargin,topMargin,0,0);
			difPlayer1.align="left";
			difPlayer1.Margin(leftMargin,topMargin,0,0);
			btAddDifPlayer1.align="left";

			btSubDifPlayer2 = new ButtonElement(game,"-",new Vector2(20,20));
			difPlayer2 = new TextElement(game,game.player2.difficulty+"",new Vector2(30,20),new Vector2(30,0));
			btAddDifPlayer2 = new ButtonElement(game,"+",new Vector2(20,20),new Vector2(70,0));
			btSubDifPlayer2.align="right";
			btSubDifPlayer2.Margin(0,topMargin,rightMargin,0);
			difPlayer2.align="right";
			difPlayer2.Margin(0,topMargin,rightMargin,0);
			btAddDifPlayer2.align="right";


			btPlayer1.AddEventListener("click",delegate(DivElement origin, Event e) {
				if(game.player1.npc){
					game.player1.npc = false;
					tipoPlayer1.text = "player";
					btAddDifPlayer1.display = false;
					btSubDifPlayer1.display = false;
					difPlayer1.display = false;
				} else {
					game.player1.npc = true;
					tipoPlayer1.text = "npc";
					btAddDifPlayer1.display = true;
					btSubDifPlayer1.display = true;
					difPlayer1.display = true;
				}
			});
			btPlayer2.AddEventListener("click",delegate(DivElement origin, Event e) {
				if(game.player2.npc){
					game.player2.npc = false;
					tipoPlayer2.text = "player";
					btAddDifPlayer2.display = false;
					btSubDifPlayer2.display = false;
					difPlayer2.display = false;
				} else {
					game.player2.npc = true;
					tipoPlayer2.text = "npc";
					btAddDifPlayer2.display = true;
					btSubDifPlayer2.display = true;
					difPlayer2.display = true;
				}
			});

			btSubDifPlayer1.AddEventListener("mousepressed",delegate(DivElement origin, Event e) {
				if(game.player1.difficulty>0){
					game.player1.difficulty--;
					difPlayer1.text = game.player1.difficulty+"";
				}
			});
			btSubDifPlayer2.AddEventListener("mousepressed",delegate(DivElement origin, Event e) {
				if(game.player2.difficulty>0){
					game.player2.difficulty--;
					difPlayer2.text = game.player2.difficulty+"";
				}
			});
			btAddDifPlayer1.AddEventListener("mousepressed",delegate(DivElement origin, Event e) {
				if(game.player1.difficulty<100){
					game.player1.difficulty++;
					difPlayer1.text = game.player1.difficulty+"";
				}
			});
			btAddDifPlayer2.AddEventListener("mousepressed",delegate(DivElement origin, Event e) {
				if(game.player2.difficulty<100){
					game.player2.difficulty++;
					difPlayer2.text = game.player2.difficulty+"";
				}
			});


			AddElement(btStartGame);
			AddElement(btPlayer1);
			AddElement(btPlayer2);
			AddElement(tipoPlayer1);
			AddElement(tipoPlayer2);
			AddElement(btSubDifPlayer1);
			AddElement(btSubDifPlayer2);
			AddElement(btAddDifPlayer1);
			AddElement(btAddDifPlayer2);
			AddElement(difPlayer1);
			AddElement(difPlayer2);
		}
			
	}
}

