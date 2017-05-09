using System;
using Microsoft.Xna.Framework;

namespace Minimax
{
	public class EndGameState : GameState
	{

		public int[] score = new int[3];
		public int winner;
		public TextElement scoreP1;
		public TextElement scoreP2;
		public TextElement scoreEmpate;
		public TextElement txtWinner;
		public DivElement[,] cells;
		public ButtonElement quitMenu;
		public ButtonElement nextGame;

		public EndGameState(Game1 g, string n): base(g,n)
		{

			cells = new DivElement[game.board.size, game.board.size];
			for (int y = 0; y < game.board.size; y++) {
				for (int x = 0; x < game.board.size; x++) {
					Vector2 cellPos = new Vector2(
						x * game.cellEmpty.Width + ((view.size.X - (game.cellEmpty.Width * game.board.size)) / 2),
						y * game.cellEmpty.Height + ((view.size.Y - (game.cellEmpty.Height * game.board.size)) / 2)
					);
					//Console.WriteLine(cellPos);
					DivElement cellBorder = new DivElement(
						game,
						new Vector2(game.cellEmpty.Width, game.cellEmpty.Height),
						cellPos,
						game.cellEmpty
					);
					cellBorder.position = "absolute";	

					DivElement cell = new DivElement(
						game,
						new Vector2(game.cellEmpty.Width,game.cellEmpty.Height),
						cellPos
					);
					Console.WriteLine(game.board.cell[x, y]);

					cell.position = "absolute";
					cells[x, y] = cell;
					view.Append(cellBorder);
					view.Append(cell);
				}
			}

			scoreP1 = new TextElement(game,"player 1: "+score[1]);
			scoreP2 = new TextElement(game,"player 2: "+score[2]);
			scoreEmpate = new TextElement(game,"empate: "+score[0],new Vector2(80,20));
			scoreP1.Margin(20);
			scoreP1.vAlign="top";
			scoreP2.Margin(20);
			scoreP2.align="right";
			scoreP2.vAlign="top";
			scoreP2.textAlign="right";
			scoreEmpate.Margin(20);
			scoreEmpate.align="center";
			scoreEmpate.vAlign="top";
			scoreEmpate.textAlign="center";

			quitMenu = new ButtonElement(game,"Quit to Menu",new Vector2(50,20));
			quitMenu.Align("right", "bottom");
			quitMenu.Margin(20);
			quitMenu.AddEventListener("click",delegate(Event e) {
				game.GameMode.Change("start");
			});
			nextGame = new ButtonElement(game, "Next game");
			nextGame.Align("center","bottom");
			nextGame.AddEventListener("click",delegate(Event e){
				game.GameMode.Change("play");
			});


			view.Append(scoreP1);
			view.Append(scoreP2);
			view.Append(scoreEmpate);
			view.Append(nextGame);
			view.Append(quitMenu);
		}


		public override void Enter(string lastState=null){
			PlayGameState play = (PlayGameState)game.GameMode.get(lastState);
			score = play.score;
			winner = play.win;
			play.win = 0;
			for(int y = 0; y < game.board.size; y++) {
				for(int x = 0; x < game.board.size; x++) {
					if(game.board.cell[x, y] == 1) {
						cells[x,y].backgroundImage = game.spriteP1;
					} else if(game.board.cell[x, y] == 2){
						cells[x,y].backgroundImage = game.spriteP2;
					}
				}
			}
			scoreEmpate.text = "empate: "+score[0];
			scoreP1.text = "player 1: " + score[1];
			scoreP2.text = "player 2: "+ score[2];
		}

		public override void Exit(string newState=null){
			if(newState == "start") {
				score = new int[3] { 0, 0, 0 };
				game.board.Clear();
				//game.player1.npc = false;
				//game.player2.npc = false;
				//game.player1.difficulty = 100;
				//game.player2.difficulty = 100;
				game.actualPlayer = game.player1;
			} else if(newState == "play") {
				foreach(DivElement c in cells) {
					c.backgroundImage = null;
				}	
			}
		}
	}
}

