using System;
using System.Threading;

namespace Minimax{
	
	public class ThreadControl{

		protected Thread oThread;
		public delegate void ThreadHandler();
		public int value; 


		public ThreadControl(ThreadHandler th){
			oThread = new Thread(new ThreadStart(th));
			oThread.Start();
		}

		public bool isFinished(){
			return !oThread.IsAlive;
		}

		public int getValue(){
			return value;
		}

	}
}

