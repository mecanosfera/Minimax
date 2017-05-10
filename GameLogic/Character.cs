using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Minimax{
	
	public class Character {

		public Texture2D spriteSheetLeft;
		public Texture2D spriteSheetRight;
		public bool isLeft = true;
		public string name;
		public string filiation; //returners, empire
		public Player player;
		public Game1 game;
		public Dictionary<string,Vector2[]> spriteState;

		public Character(Game1 g, Texture2D sl, Texture2D sr, string n, string f="returners"){
			game = g;
			spriteSheetRight = sr;
			spriteSheetLeft = sl;
			name = n;
			filiation = f;
			spriteState = new Dictionary<string,Vector2[]>(){
				{"attack", new Vector2[]{new Vector2(0,289),new Vector2(0,0),new Vector2(73,0), new Vector2(145,0),new Vector2(361,0)}},
				{"join", new Vector2[]{new Vector2(73,0),new Vector2(145,0),new Vector2(361,0)}},
				{"victory", new Vector2[]{new Vector2(73,73),new Vector2(0,73)}},
				{"defeat", new Vector2[]{new Vector2(0,217),new Vector2(0,145)}},
				{"combatIdle", new Vector2[]{new Vector2(0,217)}},
				{"boardIdle", new Vector2[]{new Vector2(0,289)}}
			};
		}
	}
}

