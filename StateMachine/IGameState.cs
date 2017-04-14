using System;

namespace Minimax
{
	public interface IGameState
	{
		
		void Enter();

		void HandleInput();

		void AddUIElement(UIElement e);

		void Update();

		void Exit();
	}
}

