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
		public int actualBg;
		public DivElement[] charLeft;
		public DivElement[] charRight;
		public TextElement[] charNameLeft;
		public TextElement[] charNameRight;

		public EndGameState(Game1 g, string n): base(g,n)
		{

			cells = new CellButton[game.board.size,game.board.size];
			for (int y = 0; y < game.board.size; y++) {
				for (int x = 0; x < game.board.size; x++) {
					Vector2 cellPos = new Vector2(
						x * 72 + ((view.size.X - (72 * game.board.size)) / 2),
						y * 72 + ((view.size.Y-220 - (72 * game.board.size)))
					);

					DivElement cellBorder = new DivElement(
						game,
						new Vector2(72, 72),
						cellPos,
						game.cellEmpty
					);
					cellBorder.position="absolute";
					cellBorder.backgroundType="cover";

					CellButton cell = new CellButton(
						game,
						new Vector2(72,72),
						cellPos,
						new int[2] {x,y}
					);
						
					cell.position = "absolute";

					view.Append(cellBorder);
					view.Append(cell);
					cells[x, y] = cell;
				}
			}

			scoreP1 = new TextElement(game,game.charactersP1.ToUpper()+": "+score[1]);
			scoreP2 = new TextElement(game,game.charactersP2.ToUpper()+": "+score[2]);
			scoreEmpate = new TextElement(game,"DRAW: "+score[0]);
			scoreP1.Margin(20);
			scoreP1.vAlign="top";
			scoreP2.Margin(20);
			scoreP1.align="left";
			scoreP2.align="right";
			scoreP2.vAlign="top";
			scoreP2.textAlign="right";
			scoreEmpate.Margin(20);
			scoreEmpate.align="center";
			scoreEmpate.vAlign="top";
			scoreEmpate.textAlign="center";

			txtWinner = new TextElement(game,"DRAW!",game.ff6_32);
			txtWinner.align = "center";
			txtWinner.pos.Y = 500;
			txtWinner.position="absolute";
			txtWinner.textAlign="center";

			quitMenu = new ButtonElement(game,"Quit to Menu",game.ff6_32);
			//quitMenu.font = game.ff6_32;
			quitMenu.Align("right", "bottom");
			quitMenu.Margin(30,10,50,8);
			quitMenu.Padding(40,0,40,0);
			quitMenu.AddEventListener("click",delegate(Event e) {
				game.GameMode.Change("title");
			});
			quitMenu.hover(game.hand);
			quitMenu.setSound();

			nextGame = new ButtonElement(game, "Next game",game.ff6_32);
			//nextGame.font= game.ff6_32;
			nextGame.Align("left","bottom");
			nextGame.Margin(30,10,50,8);
			nextGame.Padding(40,0,0,0);
			nextGame.AddEventListener("click",delegate(Event e){
				game.GameMode.Change("play");
			});

			nextGame.hover(game.hand);
			nextGame.setSound();

			int topPos = 190;
			int leftPos = 580;
			int textPos = 460;
			charLeft = new DivElement[game.characters["returners"].Length];
			charNameLeft = new TextElement[game.characters["returners"].Length];



			for(int i=0;i<charLeft.Length;i++) {
				charLeft[i] = new DivElement(
					game,
					new Vector2(72,72),
					new Vector2(leftPos,topPos),
					game.spritesLeft[game.charactersP2]["profile"][i]
				);
				charLeft[i].position="absolute";
				view.Append(charLeft[i]);
				leftPos += 20;
				topPos += 55;
				textPos += 40;
			}

			topPos = 190;
			int rightPos = 60;
			textPos = 460;

			charRight = new DivElement[game.characters["returners"].Length];
			charNameRight = new TextElement[game.characters["returners"].Length];

			for(int i=0;i<charRight.Length;i++) {
				charRight[i] = new DivElement(
					game,
					new Vector2(72,72),
					new Vector2(rightPos,topPos),
					game.spritesRight[game.charactersP1]["profile"][i]
				);
				charRight[i].position="absolute";
				view.Append(charRight[i]);
				rightPos -= 20;
				topPos += 55;
				textPos += 40;
			}


			view.Append(scoreP1);
			view.Append(scoreP2);
			view.Append(scoreEmpate);
			view.Append(txtWinner);
			view.Append(nextGame);
			view.Append(quitMenu);
		}


		public override void Enter(string lastState=null){
			game.battleStartPlay.Stop();
			game.battlePlay.Stop();
			game.bossPlay.Stop();
			game.victoryPlay.Play();

			PlayGameState play = (PlayGameState)game.GameMode.get(lastState);
			actualBg = play.actualBg;
			view.backgroundImage = game.battle_bg1[actualBg];
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
			scoreEmpate.text = "DRAW: "+score[0];
			scoreP1.text = game.charactersP1.ToUpper()+": " + score[1];
			scoreP2.text = game.charactersP2.ToUpper()+": "+ score[2];

			if(winner == 1) {
				for(int i = 0; i < 4; i++) {
					charLeft[i].backgroundImage = game.spritesLeft[game.charactersP2]["dead"][i];
					charRight[i].backgroundImage = game.spritesRight[game.charactersP1]["victory"][i];
					txtWinner.text = game.charactersP1.ToUpper()+" WINS!!!";
				}
			} else if(winner == 2) {
				for(int i = 0; i < 4; i++) {
					charLeft[i].backgroundImage = game.spritesLeft[game.charactersP2]["victory"][i];
					charRight[i].backgroundImage = game.spritesRight[game.charactersP1]["dead"][i];
					txtWinner.text = game.charactersP2.ToUpper()+" WINS!!!";
				}
			} else {
				for(int i = 0; i < 4; i++) {
					charLeft[i].backgroundImage = game.spritesLeft[game.charactersP2]["front"][i];
					charRight[i].backgroundImage = game.spritesRight[game.charactersP1]["front"][i];
					txtWinner.text = "DRAW!";
				}
			}

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
				PlayGameState play = (PlayGameState)game.GameMode.get("play");
				if(actualBg == game.battle_bg1.Length - 1) {
					play.actualBg = 0;
				} else {
					play.actualBg++;
				}
				foreach(DivElement c in cells) {
					c.backgroundImage = null;
				}	
			}
		}
	}
}

