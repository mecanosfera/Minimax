using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Minimax{
	
	public class AnimatedElement : Element{

		public Texture2D spriteSheet;
		public Dictionary<string,Vector2[]> spriteState;
		public Rectangle spriteRec;
		public delegate void AnimationHandler();


		public AnimatedElement(Game1 g, Texture2D sheet, Dictionary<string,Vector2[]> state, Vector2 startSpritePosition, Vector2 sSize, Vector2 sPosition){
			game = g;
			spriteSheet = sheet;
			spriteState = state;
			backgroundImage = spriteSheet;
			position = "absolute";
			size = sSize;
			spriteRec = new Rectangle((int)startSpritePosition.X,(int)startSpritePosition.Y,(int)size.X,(int)size.Y);
			pos = sPosition;
		}



		public override void DrawBackgroundImage(){
			if (backgroundImage != null) {
				//Console.WriteLine("xxxx");
				game.spriteBatch.Draw(
					backgroundImage,
					calcPosition(),
					spriteRec,
					Color.White/*,
					0.0f,
					new Vector2(0,0),
					new Vector2(0,0),
					SpriteEffects.None,
					0.0f*/
				);
			}
		}


		public void animate(string action, Vector2 endPos, AnimationHandler callback){
			
		}
	}
}

