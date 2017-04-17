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
		public CellButton[,] cells = new CellButton[3,3];
		public int[] score = new int[3] {0,0,0}; //0=empate, 1=p1, 2=p2
		public int win=0;
		public TextElement scoreP1;
		public TextElement scoreP2;
		public TextElement scoreEmpate;
		public ButtonElement quitMenu;

		public PlayGameState(Game1 g, string n): base(g,n)
		{
			for (int y = 0; y < 3; y++) {
				for (int x = 0; x < 3; x++) {
					Vector2 cellPos = new Vector2(
						x * game.cellEmpty.Width + ((game.GraphicsDevice.Viewport.Width - game.cellEmpty.Width * 3) / 2),
						y * game.cellEmpty.Height + ((game.GraphicsDevice.Viewport.Height - (game.cellEmpty.Height * 3)) / 2)
					);
					AddElement(new DivElement(
						game,
						new Vector2(game.cellEmpty.Width,game.cellEmpty.Height),
						cellPos,
						game.cellEmpty
					));
					CellButton cell = new CellButton(
						game,
						new Vector2(game.cellEmpty.Width,game.cellEmpty.Height),
						cellPos,
						new int[2] {x,y}
					);
					cell.AddEventListener("click",delegate(DivElement origin, Event e){
						CellButton c = (CellButton) origin;
						if(win==0 && !game.actualPlayer.npc){
							if(game.board.cell[c.cell[0],c.cell[1]]==0){
								c.updateCell(game.actualPlayer.playerNumber);
								win = game.board.Victory();
							}
						}
					});

					AddElement(cell);
					cells [x, y] = cell;;
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
			quitMenu.Margin(20);
			quitMenu.AddEventListener("click",delegate(DivElement origin, Event e) {
				game.GameMode.Change("start");
			});
				
			AddElement(scoreP1);
			AddElement(scoreP2);
			AddElement(scoreEmpate);
			AddElement(quitMenu);
		}

		public override void Enter(string lastState=null){
			game.board.Clear();
			win = 0;
			if(lastState == "end") {
				score = ((EndGameState)game.GameMode.get("end")).score;
			} else {
				score = new int[3]{ 0, 0, 0 };
			}
		}

		public override void Update(){
			base.HandleInput();
			if(win == 0) {
				if(game.actualPlayer.npc) {
					int[] nextMove = game.actualPlayer.bestMove();
					if(nextMove[0] > -1) {
						cells[nextMove[0], nextMove[1]].updateCell(game.actualPlayer.playerNumber);
					}
					win = game.board.Victory();
				}
			} else {
				if(win == 3) {
					score[0]++;
				} else if(win == 1) {
					score[1]++;
				} else {
					score[2]++;
				}
				game.GameMode.Change("end");
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

