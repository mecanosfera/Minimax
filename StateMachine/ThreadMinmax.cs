using System;
using System.Collections.Generic;
using System.Threading;

namespace Minimax
{
	public class ThreadMinmax
	{

		protected Thread oThread;
		public delegate void ThreadHandler();
		public int value;
		public int bestVal = -10;
		public int[] nextPos;
		public List<int[]> coord = new List<int[]>();


		public ThreadMinmax(ThreadHandler th){
			oThread = new Thread(new ThreadStart(th));
		}

		public void Add(int[] p){
			coord.Add(p);
		}

		public void Start(){
			oThread.Start();
		}

		public bool isFinished(){
			return !oThread.IsAlive;
		}
	}
}

