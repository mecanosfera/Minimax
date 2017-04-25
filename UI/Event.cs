using System;
using Microsoft.Xna.Framework;

namespace Minimax
{
	public class Event
	{

		public string type;
		public DivElement target;
		public DivElement origin;
		public Vector2 coords;

		public Event(string t, DivElement tgt, int X=-1, int Y=-1, DivElement o = null){
			type = t;
			target = tgt;
			coords = new Vector2(X, Y);
			origin = o;
		}
					

	}
}

