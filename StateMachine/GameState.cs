using System;
using System.Collections.Generic;

namespace Minimax
{
	public abstract class GameState
	{

		public List<DivElement> elements = new List<DivElement>();
		public string name;
		public Game1 game;

		public GameState (Game1 g, string n)
		{
			game = g;
			name = n;
		}

		public void Enter(){

		}

		public void AddUIElement(DivElement e){
			elements.Add (e);
		}

		public void HandleInput(){

		}

		public void Update(){}

		public void Exit(){}
	}
}

