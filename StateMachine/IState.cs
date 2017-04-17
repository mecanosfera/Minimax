using System;

namespace Minimax
{
	public interface IState
	{

		void Enter(string lastState=null);

		void HandleInput();

		void Update();

		void Draw();

		void Exit(string newState=null);
	}
}

