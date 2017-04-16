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

			btPlayer1 = new ButtonElement(game, "", new Vector2(game.cellO.Width, game.cellO.Height), new Vector2(10, 10), null, game.cellO);
			btPlayer2 = new ButtonElement(game, "", new Vector2(game.cellX.Width, game.cellX.Height), new Vector2(10, 10), null, game.cellX);
			tipoPlayer1 = new TextElement(game, "player", new Vector2(game.cellEmpty.Width, 20), new Vector2(70, 70));
			tipoPlayer2 = new TextElement(game, "player", new Vector2(game.cellEmpty.Width, 20), new Vector2(70, 70));
			tipoPlayer1.textAlign = "center";
			tipoPlayer2.textAlign = "center";


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


			AddElement(btStartGame);
			AddElement(btPlayer1);
			AddElement(btPlayer2);
			AddElement(tipoPlayer1);
			AddElement(tipoPlayer2);



		}
			
	}
}

