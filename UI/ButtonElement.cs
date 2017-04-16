using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Minimax
{
	public class ButtonElement : TextElement
	{

		public bool toggle = false;

		public ButtonElement (Game1 g, string t, Vector2 s,Vector2 p, SpriteFont f=null, Texture2D bg = null) : base(g,t,s,p,f,bg)
		{}

		public ButtonElement (Game1 g, string t, Vector2 s, SpriteFont f=null, Texture2D bg = null): base(g,t,s,f,bg){}

		public ButtonElement (Game1 g, string t, SpriteFont f = null, Texture2D bg = null) : base(g,t,f,bg){}

	}
}

