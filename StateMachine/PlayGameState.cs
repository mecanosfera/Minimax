using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace Minimax
{
	public class PlayGameState : GameState
	{

		//public List<CellButton> cells;
		public CellButton[,] cells;
		public int[] score = new int[3] {0,0,0}; //0=empate, 1=p1, 2=p2
		public int win=0;
		public TextElement scoreP1;
		public TextElement scoreP2;
		public TextElement scoreEmpate;
		public ButtonElement quitMenu;

		public PlayGameState(Game1 g, string n): base(g,n)
		{
			cells = new CellButton[game.board.size,game.board.size];
			for (int y = 0; y < game.board.size; y++) {
				for (int x = 0; x < game.board.size; x++) {
					Vector2 cellPos = new Vector2(
						x * game.cellEmpty.Width + ((view.size.X - (game.cellEmpty.Width * game.board.size)) / 2),
						y * game.cellEmpty.Height + ((view.size.Y - (game.cellEmpty.Height * game.board.size)) / 2)
					);
					Console.WriteLine(cellPos);
					DivElement cellBorder = new DivElement(
						                        game,
						                        new Vector2(game.cellEmpty.Width, game.cellEmpty.Height),
						                        cellPos,
						                        game.cellEmpty
					                        );
					cellBorder.position="absolute";

					CellButton cell = new CellButton(
						game,
						new Vector2(game.cellEmpty.Width,game.cellEmpty.Height),
						cellPos,
						new int[2] {x,y}
					);
					cell.AddEventListener("click",delegate(Event e){
						CellButton c = (CellButton) e.target;
						if(win==0 && !game.actualPlayer.npc){
							if(game.board.cell[c.cell[0],c.cell[1]]==0){
								c.updateCell(game.actualPlayer.playerNumber);
								win = game.board.Victory();
							}
						}
					});
					cell.position = "absolute";

					view.Append(cellBorder);
					view.Append(cell);
					cells[x, y] = cell;
				}
			}

			scoreP1 = new TextElement(game,"O: "+score[1],new Vector2(50,20));
			scoreP2 = new TextElement(game,"X: "+score[2],new Vector2(50,20));
			scoreEmpate = new TextElement(game,"Empate: "+score[0],new Vector2(80,20));
			scoreP1.Margin(20);
			scoreP2.Margin(20);
			scoreP2.align="right";
			scoreP2.textAlign="right";
			scoreEmpate.Margin(20);
			scoreEmpate.align="center";
			scoreEmpate.textAlign="center";

			quitMenu = new ButtonElement(game,"Quit to Menu",new Vector2(50,20));
			quitMenu.Align("right", "bottom");
			//quitMenu.Margin(0);
			quitMenu.AddEventListener("click",delegate(Event e) {
				game.GameMode.Change("start");
			});
				
			view.Append(scoreP1);
			view.Append(scoreP2);
			view.Append(scoreEmpate);
			view.Append(quitMenu);
		}

		public override void Enter(string lastState=null){
			game.board.Clear();
			win = 0;
			if(lastState == "end") {
				score = ((EndGameState)game.GameMode.get("end")).score;
			} else
            {
                score = ((PlayGameState)game.GameMode.get("play")).score;
                //score = new int[3]{ 0, 0, 0 };
            }
		}

		public override void Update(){
			base.HandleInput();
			if(win == 0) {
				if(game.actualPlayer.npc) {
					
					int[] nextMove;
					if(game.board.size < 7) {
						nextMove = game.actualPlayer.bestMove(true);
					} else {
						nextMove = game.actualPlayer.bestMoveThreaded();
					}
					if(nextMove[0] > -1) {
						cells[nextMove[0], nextMove[1]].updateCell(game.actualPlayer.playerNumber);
					}
					win = game.board.Victory();
				}
			} else {
				if(win == 3) {
					score[0]++;
					scoreEmpate.text = "Empate: "+score[0];
				} else if(win == 1) {
					score[1]++;
					scoreP1.text = "O: " + score[1];
				} else {
					score[2]++;
					scoreP2.text = "X: "+score[2];
				}
                Console.WriteLine(score[0]+" "+ score[1] + " "+ score[2]);
				game.GameMode.Change("play");
			}
			
		}


		public override void Exit(string newState=null){
			if(newState == "start") {
				score = new int[3] { 0, 0, 0 };
				game.board.Clear ();
				win = 0;
				game.player1.npc = false;
				game.player2.npc = false;
				game.player1.difficulty = 100;
				game.player2.difficulty = 100;
			}

			foreach(CellButton cell in cells) {
				cell.Clear();
			}
		}
			
	}
}

