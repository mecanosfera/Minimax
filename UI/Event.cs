using System;
using Microsoft.Xna.Framework;

namespace Minimax
{
	public class Event
	{

		public string type;
		public Element target;
		public Element origin;
		public Vector2 coords;

		public Event(string t, Element tgt, int X=-1, int Y=-1, Element o = null){
			type = t;
			target = tgt;
			coords = new Vector2(X, Y);
			origin = o;
		}
					

	}
}

