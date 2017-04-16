using System;
using Microsoft.Xna.Framework;

namespace Minimax
{
	public class Event
	{

		public string valueType = "int";
		public int[] vInt;
		public string[] vString;
		public float[] vFloat;
		public Vector2 vVector;

		public Event (params int[] p)
		{
			valueType="int";
			vInt = p;
		}

		public Event(params string[] p)
		{
			valueType = "string";
			vString = p;
		}

		public Event(params float[] p){
			valueType = "float";
			vFloat = p;
		}

		public Event(Vector2 v){
			valueType="Vector2";
			vVector = v;
		}
			

	}
}

