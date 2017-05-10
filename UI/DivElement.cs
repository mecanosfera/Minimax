using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Minimax
{
	public class DivElement : Element
	{
		


        
	
		public DivElement (Game1 g, Vector2 s, Texture2D bg=null)
		{
			game = g;
			size = s;
			pos = new Vector2(0, 0);
			backgroundImage = bg;
		}

		public DivElement (Game1 g, Vector2 s, Vector2 p, Texture2D bg=null)
		{
			game = g;
			size = s;
			pos = p;
			backgroundImage = bg;
		}



		public virtual void Margin(params int[] m){
			if (m.Length == 1) {
				margin = new int[4]{ m[0], m[0], m[0], m[0] };
			} else {
				for (int i = 0; i < m.Length; i++) {
					margin [i] = m [i];
				}
			}
		}

		public virtual void Padding(params int[] p){
			if (p.Length == 1) {
				padding = new int[4]{ p[0], p[0], p[0], p[0] };
			} else {
				for (int i = 0; i < p.Length; i++) {
					padding[i] = p[i];
				}
			}
		}

		public virtual void Align(string ha, string va){
			align = ha;
			vAlign = va;
		}

		public override Vector2 calcPosition(){
			Vector2 actualPos = pos;
			Vector2 actualSize = calcSize();
			Vector2 parentPos = new Vector2(0,0);
			Vector2 parentSize = new Vector2(0,0);
			if (parentNode != null) {
				parentPos = parentNode.calcPosition();
				parentSize = parentNode.calcSize();
			}

			//calcula actualPos.X
			/*
				- align center funciona apenas se o display for block.
				- align left ou right só leva em conta vizinhos de mesmo alinhamento.
			*/
			if (align == "left" || (align=="center" && display=="inline")) {
				if(position == "relative") {
					if(display == "block") {
						actualPos.X += margin[0] + parentPos.X + parentNode.padding[0];
					} else if(display == "inline") {
						if(previousNode != null && previousNode.display!="block" && previousNode.align=="right") {
							actualPos.X += margin[0] + previousNode.calcSize().X + previousNode.margin[2] + previousNode.calcPosition().X;
						} else if (previousNode == null || (previousNode != null && previousNode.display =="block") || (previousNode!=null && previousNode.align=="right")) {
							actualPos.X += margin[0] + parentPos.X + parentNode.padding[0];
						}
					}
				} else if(position == "absolute") {
					actualPos.X += margin[0] + parentPos.X + parentNode.padding[0];
				}
			} else if (align == "center" && display=="block") {
				if (parentNode == null || position == "absolute") {
					actualPos.X = (game.GraphicsDevice.Viewport.Width - actualSize.X) * 0.5f;
				} else if (parentNode != null && position == "relative") {
					if (actualSize.X < parentSize.X) {
						actualPos.X = parentPos.X + (parentSize.X - actualSize.X) * 0.5f;
					} else {
						actualPos.X = margin [0] + parentPos.X + parentNode.padding[0];
					}
				}
			} else if (align == "right") {
				//pos ainda é considerada da esquerda p/ direita, cima p/ baixo. modificar isso.
				if (parentNode == null || position == "absolute") {
					actualPos.X += game.GraphicsDevice.Viewport.Width - (size.X + margin[2] + padding[2]);
				} else if (parentNode != null && position == "relative") {
					//recalcular
					actualPos.X += (parentPos.X + parentNode.padding[2]+parentNode.size.X)-(size.X + margin[2] + padding[2]);
				}
			}

			//calcula actualPos.Y


			if(vAlign == "top") {
				actualPos.Y += margin[1] + parentPos.Y + parentNode.padding[1];
			} else if (vAlign == "flow") {				
				if(position == "relative") {
					if(display == "block") {						
						if(previousNode != null && previousNode.display == "block" && previousNode.vAlign == "flow") {
							actualPos.Y += previousNode.calcPosition().Y + previousNode.calcSize().Y + previousNode.margin[3] + margin[1];
						} else if(previousNode != null && previousNode.display == "inline" && previousNode.vAlign == "flow") {
							actualPos.Y += previousNode.calcPosition().Y;
						} else if(previousNode == null || previousNode.vAlign != "flow") {
							actualPos.Y += parentNode.calcPosition().Y + margin[1];
						} 
					}
				} else {					
					actualPos.Y += margin[1] + parentPos.Y + parentNode.padding[1];
				}
				/*if (parentNode == null || position == "absolute") {
					actualPos.Y += margin[1];
				} else if (parentNode != null && position == "relative") {
					
				} */
			} else if (vAlign == "middle") {
				if (parentNode == null || position == "absolute") {
					actualPos.Y = (game.GraphicsDevice.Viewport.Height - actualSize.Y) * 0.5f;
				} else if (parentNode != null && position == "relative") {
					if (actualSize.Y < parentSize.Y) {
						actualPos.Y = parentPos.Y + (parentSize.Y - actualSize.Y) * 0.5f;
					} else {
						actualPos.Y = margin[1] + parentPos.Y + parentNode.padding[1];
					}
				}
			} else if (vAlign == "bottom") {
				//pos ainda é considerada da esquerda p/ direita, cima p/ baixo. modificar isso.
				if (parentNode == null || position == "absolute") {
					actualPos.Y += game.GraphicsDevice.Viewport.Height - (size.Y + margin[3] + padding[3]);
					actualPos.Y += margin[1];
				} else if (parentNode != null && position == "relative") {
					actualPos.Y += (parentPos.Y + parentNode.padding[3]+parentNode.size.Y) - (size.Y + margin[3] + padding[3]);
					actualPos.Y -= margin[3];
					//depois
					//actualPos.Y = margin[1] + parentPos.Y + parent.padding[1];
				}
			}
			return actualPos;
		}

		public override Vector2 calcSize(){
			//adiciona os paddings left/right e top/bottom às dimensões
			return size+new Vector2(padding[0]+padding[2],padding[1]+padding[3]);
		}

		public void hover(Texture2D background){
			Texture2D basebg = backgroundImage;
			AddEventListener("mouseover",delegate(Event e) {
				backgroundImage = background;
			});
			AddEventListener("mouseout",delegate(Event e) {
				backgroundImage = basebg;
			});
		}

		public void hoverBgColor(Color bg){
			Color basebg = backgroundColor;
			AddEventListener("mouseover",delegate(Event e) {
				backgroundColor = bg;
			});
			AddEventListener("mouseout",delegate(Event e) {
				backgroundColor = basebg;
			});
		}

		public void hoverFgColor(Color fg){
			Color basefg = foregroundColor;
			AddEventListener("mouseover",delegate(Event e) {
				foregroundColor = fg;
			});
			AddEventListener("mouseout",delegate(Event e) {
				foregroundColor = basefg;
			});
		}



			

		public override void DrawBackgroundImage(){
			if (backgroundImage != null) {
				Vector2? bgCover = null;
				if(backgroundType == "cover") {
					bgCover = new Vector2(calcSize().X / backgroundImage.Width, calcSize().Y / backgroundImage.Height); 
				}
				game.spriteBatch.Draw(
					backgroundImage,
					calcPosition(),
					null,
					null,
					Vector2.Zero,
					0.0f,
					bgCover,
					foregroundColor,
					SpriteEffects.None,
					0.0f
				);
			}
		}

		public override void Draw(){
			if (display!="none") {
				DrawBackgroundColor();
				DrawBackgroundImage();
				foreach(DivElement ch in children) {
					ch.Draw();
				}
			}

		}
	}
}

