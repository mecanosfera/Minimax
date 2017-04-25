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
		public ThreadControl tc;
		public ThreadMinmax tm;

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


		public int[] bestMoveThreaded(){
			Board board = game.board;
			int[] nextMove = new int[2] { -1, -1 };
			int bestVal = -10;
			List<ThreadMinmax> threads = new List<ThreadMinmax>();
			Queue<int[]> next = new Queue<int[]>();

			for(int y = 0; y < board.size; y++) {
				for(int x = 0; x < board.size; x++) {
					if(board.cell[x, y] == 0) {
						//faz uma cópia do board com a primeira jogada e verifica se ganha o jogo.
						Board myCopy = board.GetCopy();
						myCopy.cell[x, y] = playerNumber;
						if(myCopy.Victory() == playerNumber) {
							nextMove[0] = x;
							nextMove[1] = y;
							return nextMove;
						}
						next.Enqueue(new int[]{x,y});
					}
				}
			}


			for(int s = 0; s < board.size; s++) {
				if(next.Count > 0) {
					//ThreadMinimax tm; 
					Console.WriteLine("Creating thread");
					tm = new ThreadMinmax(delegate() {
						foreach(int[] c in tm.coord) {
							Board b = board.GetCopy();
							b.cell[c[0], c[1]] = playerNumber;
							int depth = board.getLeft();
							if(board.size > 3) {
								depth = 4;
							}
							tm.value = Minimax(b, depth, false);
							if(tm.value >= tm.bestVal) {
								tm.bestVal = tm.value;
								tm.nextPos = c;
							}
						}
					});
					threads.Add(tm);
					tm.Add(next.Dequeue());
				}
			}
			while(next.Count > 0) {
				foreach(ThreadMinmax t in threads) {
					if(next.Count > 0) {
						t.Add(next.Dequeue());
					}
				}
			}
			foreach(ThreadMinmax t in threads) {
				t.Start();
			}
			bool isFinished = false;
			while(!isFinished) {
				isFinished = true;
				foreach(ThreadMinmax t in threads) {
					if(!t.isFinished()) {
						isFinished = false;
					}
				}	
			}
			foreach(ThreadMinmax t in threads) {
				if(t.bestVal >= bestVal) {
					bestVal = t.bestVal;
					nextMove = t.nextPos;
				}
			}
			return nextMove;
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
								int depth = board.getLeft();
								if(board.size > 3) {
									depth = 3;
								}
								val = Minimax(myCopy, depth, false);
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
