﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Minimax
{
	public class TextElement : DivElement
	{

		public string text;
		public string textAlign = "left"; //left,right,center
		public Vector2 textSize;
		protected SpriteFont font;


		public TextElement (Game1 g, string t, Vector2 s,Vector2 p, SpriteFont f=null, Texture2D bg = null) : base(g,s,p,bg)
		{
			text = t;
			if (f != null) {
				font = f;
			}
			textSize = font.MeasureString (text);
		}

		public TextElement (Game1 g, string t, Vector2 s, SpriteFont f=null, Texture2D bg = null): base(g,s,bg){
			text = t;
			if (f != null) {
				font = f;
			}
			textSize = font.MeasureString(text);
			size = textSize;
		}

		public TextElement (Game1 g, string t, SpriteFont f = null, Texture2D bg = null) : base(g,default(Vector2),bg){
			game = g;
			pos = new Vector2 (0, 0);
			if (f != null) {
				font = f;
			}
			textSize = font.MeasureString (text);
			size = textSize;
			text = t;
			background = bg;
		}

		public Vector2 calcTextPosition(){
			Vector2 actualPos = calcPosition();

			if (textAlign == "left") {
					return new Vector2(actualPos.X + padding[0], actualPos.Y + padding[1]); 
			} else if (align == "center") {
				if (textSize.X >= size.X) {
					return new Vector2(actualPos.X + padding[0], actualPos.Y + padding[1]);
				} else {
					return new Vector2((actualPos.Y+(size.X-textSize.X)*0.5f) + padding[0],actualPos.Y+padding[1]);
				}
			} else if (align == "right") {

			}

			return new Vector2(actualPos.X + padding[0], actualPos.Y + padding[1]); 
		}


		public void DrawText(){
			game.spriteBatch.DrawString(
				font, 
				text, 
				calcTextPosition(), 
				foregroundColor, 
				0.0f, 
				textSize / 2.0f, 
				Vector2.One, 
				SpriteEffects.None, 
				0.0f
			);
		}

	}
}
