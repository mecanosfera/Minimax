using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

namespace Minimax
{
	public abstract class Element
	{

		public Game1 game;
		public Vector2 pos; 
		public Vector2 size;
		public Color backgroundColor = Color.Transparent;
		public Color foregroundColor = Color.White;
		public Texture2D backgroundImage;
		public string backgroundType = "no-repeat"; //repeat, no-repeat, cover	
		public string display="block"; //block, inline, none
		public Element parentNode = null;
		public Element firstNode = null;
		public Element lastNode = null;
		public Element previousNode = null;
		public Element nextNode = null;
		public List<DivElement> children = new List<DivElement>();
		public string id="";
		public int[] margin = new int[4] {0,0,0,0}; //left,top,right,bottom
		public int[] padding = new int[4] {0,0,0,0}; //left,top,right,bottom
		public delegate void EventHandler(Event e);

		public Element()
		{
			
		}



		public virtual Vector2 calcPosition(){
			return pos;
		}

		public virtual Vector2 calcSize(){
			return size;
		}
			
		public virtual void Append(DivElement e){
			children.Add(e);
			e.parentNode = this;
			if(firstNode == null) {
				firstNode = e;
				lastNode = e;
			} else {
				e.previousNode = lastNode;
				lastNode.nextNode = e;
				lastNode = e;
			}
			//Console.WriteLine("ch: "+children.Count+" t:"+e.GetType());
		}

		public virtual Element GetElementById(string id){
			foreach(Element e in children) {
				if(e.id == id) {
					return e;
				}
			}
			return null;
		}

		public virtual bool Remove(DivElement e){
			if(children.Remove(e)) {					
				if(e.previousNode != null) {
					e.previousNode.nextNode = e.nextNode;
				}
				if(e.nextNode != null) {
					e.nextNode.previousNode = e.previousNode;
				}
				if(this.firstNode == e) {
					if(e.nextNode != null) {
						this.firstNode = e.nextNode;
					}
				}
				if(this.lastNode == e) {
					this.lastNode = e.previousNode;
				}
				e.parentNode = null;
				return true;
			}
			return false;
		}

		public virtual void DrawBackgroundColor(){
			if(backgroundColor != Color.Transparent) {
				
			}
		}

		public virtual void DrawBackgroundImage(){
			if (backgroundImage != null) {
				//Console.WriteLine (background.Name);
				game.spriteBatch.Draw(
					backgroundImage,
					calcPosition(),
					null,
					null,
					Vector2.Zero,
					0.0f,
					new Vector2(calcSize().X / backgroundImage.Width, calcSize().Y / backgroundImage.Height),
					foregroundColor,
					SpriteEffects.None,
					0.0f
				);
			}
		}

		public virtual void Draw(){
			DrawBackgroundColor();
			DrawBackgroundImage();
			foreach(Element ch in children) {
				ch.Draw();
			}
		}

		public List<DivElement> GetChildren(){
			List<DivElement> c = new List<DivElement>();
			//Console.WriteLine("c: "+children.Count+" "+this.GetType());
			foreach(DivElement ch in children) {
				c.Add(ch);
				//Console.WriteLine("ch: "+ch.GetChildren().Count+" "+ch.GetType());
				c = c.Concat(ch.GetChildren()).ToList();

				//c.AddRange(ch.GetChildren());
			}
			return c;
		}

		public virtual void AddEventListener(string type, EventHandler callback){}
	}		
}

