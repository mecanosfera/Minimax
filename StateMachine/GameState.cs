using System;
using System.Collections.Generic;

namespace Minimax
{
	public abstract class GameState : IState
	{

		public List<DivElement> elements = new List<DivElement>();
		public string name;
		public Game1 game;

		public GameState (Game1 g, string n)
		{
			game = g;
			name = n;
		}

		public virtual void AddElement(DivElement e){
			elements.Add (e);
		}

		public virtual void Enter(string lastState=null){}
			
		public virtual void HandleInput(){}

		public virtual void Update(){}

		public virtual void Draw(){
			foreach (DivElement e in elements) {
				if (e.parent == null) {
					e.Draw();
				}
			}
		}

		public virtual void Exit(){}
	}
}

