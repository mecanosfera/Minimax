using System;

namespace Minimax
{
	public class EndGameState : GameState
	{

		public int[] score = new int[3];
		public int winner;

		public EndGameState(Game1 g, string n): base(g,n)
		{
		}


		public override void Enter(string lastState=null){
			PlayGameState play = (PlayGameState)game.GameMode.get(lastState);
			score = play.score;
			winner = play.win;
			play.win = 0;
		}


	}
}

