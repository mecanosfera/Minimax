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
		ButtonElement btQuit;
		DivElement divPlayer1;
		DivElement divPlayer2;
		ButtonElement btTipoPlayer1;
		ButtonElement btTipoPlayer2;
		ButtonElement btDifPlayer1;
		ButtonElement btDifPlayer2;

		ButtonElement btPlayer1;
		ButtonElement btPlayer2;
		TextElement txtPlayer1;
		TextElement txtPlayer2;



		public StartGameState(Game1 g, string n): base(g,n)
		{
			
			btStartGame = new ButtonElement(game,"start",game.ff6_32);
			divPlayer2 = new DivElement(game,new Vector2(340,680)/*,game.cellEmpty*/);
			divPlayer1 = new DivElement(game,new Vector2(340,680)/*,game.cellEmpty*/);
			btTipoPlayer1 = new ButtonElement(game, "", new Vector2(72,72),null,game.spriteP1);
			btTipoPlayer2 = new ButtonElement(game, "",new Vector2(72,72),null,game.spriteP2);
			btDifPlayer1 = new ButtonElement(game, "hard");
			btDifPlayer2 = new ButtonElement(game, "hard");

			btQuit = new ButtonElement(game,"Quit to Menu",game.ff6_32);
			btQuit.position="absolute";
			btQuit.pos = new Vector2(640-(btQuit.size.X+0),590);
			btQuit.Padding(40,0,40,0);
			btQuit.AddEventListener("click",delegate(Event e) {
				game.GameMode.Change("title");
			});
			btQuit.hover(game.hand);
			btQuit.setSound();

			txtPlayer1 = new TextElement(game, game.charactersP1.ToUpper(), game.ff6_32);
			txtPlayer1.align="center";
			txtPlayer1.textAlign="center";
			txtPlayer1.Margin(10);


			btPlayer1 = new ButtonElement(game,"",game.ff6_32,game.charFacesRight[game.charactersP1]);
			btPlayer1.size = new Vector2(100,380);
			btPlayer1.align="center";
			btPlayer1.textAlign="center";

			txtPlayer2 = new TextElement(game, game.charactersP2.ToUpper(), game.ff6_32);
			txtPlayer2.align="center";
			txtPlayer2.textAlign="center";
			txtPlayer2.Margin(10);


			btPlayer2 = new ButtonElement(game,"",game.ff6_32,game.charFacesLeft[game.charactersP2]);
			btPlayer2.size = new Vector2(100,380);
			btPlayer2.align="center";
			btPlayer2.textAlign="center";


			//btStartGame.vAlign(bo);
			btStartGame.Padding(40, 0, 40, 0);
			btStartGame.position="absolute";
			btStartGame.textAlign="center";
			btStartGame.pos = new Vector2(20,590);

			divPlayer2.align = "right";
			divPlayer2.vAlign = "top";
			divPlayer2.Margin(10);
			divPlayer1.align = "left";
			divPlayer1.Margin(10);
			divPlayer1.vAlign = "top";

			//divPlayerLeft.backgroundImage=game.cellEmpty;
			divPlayer1.backgroundType="cover";
			//divPlayerRight.backgroundImage=game.cellEmpty;
			divPlayer2.backgroundType="cover";


			btPlayer1.AddEventListener("click",delegate(Event e) {
				if(game.charactersP1=="returners"){
					game.charactersP1="empire";
					game.charactersP2="returners";
					game.spriteP1=game.cellX;
					game.spriteP2=game.cellO;
					btPlayer1.backgroundImage=game.charFacesRight["empire"];
					btPlayer2.backgroundImage=game.charFacesLeft["returners"];
					txtPlayer1.text = "EMPIRE";
					txtPlayer2.text = "RETURNERS";
					if(!game.player1.npc){
						btTipoPlayer1.backgroundImage=game.cellX;
					}
					if(!game.player2.npc){
						btTipoPlayer2.backgroundImage=game.cellO;
					}
				} else {
					game.charactersP1="returners";
					game.charactersP2="empire";
					game.spriteP1=game.cellO;
					game.spriteP2=game.cellX;
					btPlayer1.backgroundImage=game.charFacesRight["returners"];
					btPlayer2.backgroundImage=game.charFacesLeft["empire"];
					txtPlayer2.text = "EMPIRE";
					txtPlayer1.text = "RETURNERS";
					if(!game.player1.npc){
						btTipoPlayer1.backgroundImage=game.cellO;
					}
					if(!game.player2.npc){
						btTipoPlayer2.backgroundImage=game.cellX;
					}
				}
			});

			btPlayer1.setSound(false);

			btPlayer2.AddEventListener("click",delegate(Event e) {
				if(game.charactersP1=="returners"){
					game.charactersP1="empire";
					game.charactersP2="returners";
					game.spriteP1=game.cellX;
					game.spriteP2=game.cellO;
					btPlayer1.backgroundImage=game.charFacesRight["empire"];
					btPlayer2.backgroundImage=game.charFacesLeft["returners"];
					txtPlayer1.text = "EMPIRE";
					txtPlayer2.text = "RETURNERS";
					if(!game.player1.npc){
						btTipoPlayer1.backgroundImage=game.cellX;
					}
					if(!game.player2.npc){
						btTipoPlayer2.backgroundImage=game.cellO;
					}
				} else {
					game.charactersP1="returners";
					game.charactersP2="empire";
					game.spriteP1=game.cellO;
					game.spriteP2=game.cellX;
					btPlayer1.backgroundImage=game.charFacesRight["returners"];
					btPlayer2.backgroundImage=game.charFacesLeft["empire"];
					txtPlayer2.text = "EMPIRE";
					txtPlayer1.text = "RETURNERS";
					if(!game.player1.npc){
						btTipoPlayer1.backgroundImage=game.cellO;
					}
					if(!game.player2.npc){
						btTipoPlayer2.backgroundImage=game.cellX;
					}
				}
			});

			btPlayer2.setSound(false);






			btTipoPlayer1.align = "center";
			btTipoPlayer2.align = "center";
			//btTipoPlayer1.Padding(40, 0, 40, 0);
			//btTipoPlayer2.Padding(40, 0, 40, 0);
			btTipoPlayer1.setSound();
			btTipoPlayer2.setSound();
			btTipoPlayer1.Margin(0, 20, 0, 0);
			btTipoPlayer2.Margin(0, 20, 0, 0);
			btTipoPlayer1.size = new Vector2(72, 72);
			btTipoPlayer2.size = new Vector2(72, 72);
			//btTipoPlayer1.hover(game.hand);
			//btTipoPlayer2.hover(game.hand);

			btDifPlayer1.align="center";
			btDifPlayer1.textAlign="center";
			btDifPlayer1.display = "none";
			btDifPlayer2.align="center";
			btDifPlayer2.textAlign="center";
			btDifPlayer1.display = "none";

			btStartGame.AddEventListener("click",delegate(Event e) {
				game.GameMode.Change("play");
			});

			btStartGame.setSound();
			btStartGame.hover(game.hand);

			btTipoPlayer1.AddEventListener("click",delegate(Event e) {
				if(game.player1.npc){
					game.player1.npc = false;
					btDifPlayer1.display="none";
					btTipoPlayer1.backgroundImage=game.spriteP1;
					btTipoPlayer1.size = new Vector2(72,72);
				} else {
					game.player1.npc = true;
					btDifPlayer1.display="block";
					btTipoPlayer1.size = new Vector2(96,96);
					if(game.player1.difficulty==100){
						btTipoPlayer1.backgroundImage=game.spriteHard;
					} else {
						btTipoPlayer1.backgroundImage=game.spriteMedium;
					}
				}
			});
				

            btTipoPlayer2.AddEventListener("click",delegate(Event e) {
				if(game.player2.npc){
					game.player2.npc = false;
					btDifPlayer2.display="none";
					btTipoPlayer2.backgroundImage=game.spriteP2;
					btTipoPlayer2.size = new Vector2(72,72);
				} else {
					game.player2.npc = true;
					btDifPlayer2.display="block";
					btTipoPlayer2.size = new Vector2(96,96);
					if(game.player2.difficulty==100){
						btTipoPlayer2.backgroundImage=game.spriteHard;
					} else {
						btTipoPlayer2.backgroundImage=game.spriteMedium;
					}
				}
			});


			btDifPlayer1.AddEventListener("click",delegate(Event e) {
				if(game.player1.difficulty==100){
					game.player1.difficulty = 50;
					btDifPlayer1.text="medium";
					btTipoPlayer1.backgroundImage=game.spriteMedium;
				} else if(game.player1.difficulty==50){
					game.player1.difficulty = 100;
					btDifPlayer1.text="hard";
					btTipoPlayer1.backgroundImage=game.spriteHard;
				} 
			});

			btDifPlayer2.AddEventListener("click",delegate(Event e) {				
				if(game.player2.difficulty==100){
					game.player2.difficulty = 50;
					btDifPlayer2.text="medium";
					btTipoPlayer2.backgroundImage=game.spriteMedium;
				} else if(game.player2.difficulty==50){
					game.player2.difficulty = 100;
					btDifPlayer2.text="hard";
					btTipoPlayer2.backgroundImage=game.spriteHard;
				} 
			});
				
				

			divPlayer1.Append(txtPlayer1);
			divPlayer1.Append(btPlayer1);
			divPlayer1.Append(btTipoPlayer1);
			divPlayer1.Append(btDifPlayer1);
			divPlayer2.Append(txtPlayer2);
			divPlayer2.Append(btPlayer2);
			divPlayer2.Append(btTipoPlayer2);
			divPlayer2.Append(btDifPlayer2);
			//divPlayer1.Append(btStartGame);

			view.Append(divPlayer2);
			view.Append(divPlayer1);
			view.Append(btStartGame);
			view.Append(btQuit);

			view.backgroundImage = game.menu4;
		}

		public override void Enter(string lastState){
			if(game.player1.npc) {
				btDifPlayer1.display = "block";
				//btTipoPlayer1.text = "player 1 (npc)";
			} else {
				btDifPlayer1.display = "none";
				//btTipoPlayer1.text = "player 1";
			}
			if(game.player2.npc) {
				btDifPlayer2.display = "block";
				//btTipoPlayer2.text = "player 2 (npc)";
			} else {
				btDifPlayer2.display = "none";
				//btTipoPlayer2.text = "player 2";
			}

		}
			
	}
}

