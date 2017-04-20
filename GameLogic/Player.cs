using System;
using System.Collections.Generic;
namespace Minimax
{
	public class Player
	{

		public int playerNumber;
		public int opponentNumber;
		public bool npc;
		public int wins = 0;
		public int difficulty = 100; //define a aleatoriedade do npc, de 0 a 100
        public Game1 game;

		public Player(Game1 game, int number, bool npc, int random=100)
		{
			playerNumber = number;
			if (playerNumber == 1){
				opponentNumber = 2;
			}
			else { 
				opponentNumber = 1;
			}

			difficulty = random;
			this.npc = npc;
            this.game = game;

		}

		public int[] bestMove(bool alphabeta=false)
		{
			Console.WriteLine("in minimax");
			Board board = game.board;
			int[] nextMove = new int[2] { -1, -1 };
			int bestVal = -10;

			//calcula a chance de uma escolha aleatória a partir do nível de randomness do jogador
			Random r = new Random ();

			/*se optar por Minimax o loop passa por cada célula livre e calcula o valor
			do Minimax, guardando o maior valor a célula correspondente.*/
			if (difficulty == 100 || r.Next(1, 101) <= difficulty) {
				for (int y = 0; y < board.size; y++) {
					for (int x = 0; x < board.size; x++) {
						if (board.cell [x, y] == 0) {
							
							//faz uma cópia do board com a primeira jogada e verifica se ganha o jogo.
							Board myCopy = board.GetCopy ();
							myCopy.cell [x, y] = playerNumber;
							if (myCopy.Victory () == playerNumber) {
								nextMove [0] = x;
								nextMove [1] = y;
								return nextMove;
							}

							// caso a jogada não ganhe entra no Minimax. como a próxima jogada é do oponente 
							// o Minimax inicia com false.
							int val;
							if (alphabeta) {
								val = Minimax(myCopy, board.getLeft (), false, 9999, -9999);
							} else {
								val = Minimax(myCopy, board.getLeft (), false);
							}
							if (val >= bestVal) {
								bestVal = val;
								nextMove [0] = x;
								nextMove [1] = y;
							}

						}
					}
				}
			} else {
				//escolhe aleatoriamente uma das células livres 
				int next = r.Next (0, board.getLeft ());
				for (int y = 0; y < board.size; y++) {
					for (int x = 0; x < board.size; x++) {
						if (board.cell[x, y] == 0) {
							if (next == 0) {
								nextMove[0] = x;
								nextMove[1] = y;
								return nextMove;
							}
							next--;
						}
					}
				}
			}
			Console.WriteLine("out minimax");
			return nextMove;
		}

		public int Minimax(Board Board, int depth, bool isMax, int alpha, int beta){
			return 0;
		}


		public int Minimax(Board board, int depth, bool isMax){
			if(board.IsGameOver() || depth == 0){
				return board.GetScore(this);
			}

			int value;
			if(!isMax){
				value = 9999999;
				List<Board> possibilities = board.GetPossibilities(opponentNumber);
				foreach(Board p in possibilities){
					value = Math.Min(value, Minimax(p, depth - 1, true));
				}
			} else{
				value = -9999999;
				List<Board> possibilities = board.GetPossibilities(playerNumber);
				foreach (Board p in possibilities){
					value = Math.Max(value, Minimax(p, depth - 1, false));
				}
			}
			return value;
		}
			
	}
}
