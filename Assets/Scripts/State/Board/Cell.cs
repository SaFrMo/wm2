using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Newtonsoft.Json.Linq;
using SaFrLib;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System;

namespace WhiteMask {

	[System.Serializable]
	public class Cell {

		public const string 
			NORTHWEST = "NW", 
			NORTH = "N", 
			NORTHEAST = "NE", 
			WEST = "W", 
			EAST = "E", 
			SOUTHWEST = "SW", 
			SOUTH = "S", 
			SOUTHEAST = "SE";

		public float x = 0;
		public float y = 0;
		public float width = 0;
		public float height = 0;
		public string id = "0";
		public string name = "Default Cell";

		// Stats
		public List<string> directions = new List<string> (){ NORTHWEST, NORTH, NORTHEAST, WEST, EAST, SOUTHWEST, SOUTH, SOUTHEAST };
		public int health = 100;
		public int attack = 10;
		public int worth = 20;

		public Cell(){

			//CreateSnapshot ();

		}

		public Cell(string id, string name) {
			this.id = id;
			this.name = name;
		}

		public string CreateSnapshot(){
			using (StringWriter w = new StringWriter ()) {
				XmlSerializer serializer = new XmlSerializer (typeof(Cell));
				serializer.Serialize (w, this);
				return w.ToString ();
			}
		}

		public static Cell LoadFromSnapshot(string snapshot){
			StringReader strReader = null;
			XmlSerializer serializer = null;
			XmlTextReader xmlReader = null;
			Cell output = null;
			try
			{
				strReader = new StringReader(snapshot);
				serializer = new XmlSerializer(typeof(Cell));
				xmlReader = new XmlTextReader(strReader);
				output = (Cell)serializer.Deserialize(xmlReader);
			}
			catch (Exception exp)
			{
				//Handle Exception Code
				MonoBehaviour.print("error!" + exp);
			}
			finally
			{
				if (xmlReader != null)
				{
					xmlReader.Close();
				}
				if (strReader != null)
				{
					strReader.Close();
				}
			}
			return output;

		}

//		public const string 
//			CELL_ID_KEY = "id",
//			COORD_X_KEY = "x",
//			COORD_Y_KEY = "y",
//			MAX_HP_KEY = "maxHp",
//			WIDTH_KEY = "width",
//			HEIGHT_KEY = "height",
//			ATTACK_KEY = "attack",
//			CONNECTORS_KEY = "connectors",
//			WORTH_KEY = "worth",
//			PRIORITY_KEY = "attackPriority";
//
//		public string guid;
//		public Vector2 relativeCoordinates;
//		public int maxHp;
//		public float width;
//		public float height;
//		public int attack;
//		public List<string> connectors;
//		public Board.BoardState parentState;
//		public int hp = 100;
//		public int worth = 100;
//		public string queuedOccupant = null;
//		public string livingOccupant = null;
//		public string occupant { 
//			get { return queuedOccupant == null ? livingOccupant : queuedOccupant; } 
//			set {
//				if (livingOccupant != null) {
//					livingOccupant = value;
//				} else {
//					queuedOccupant = value;
//				}
//			}
//		}
//		public bool occupied { get { return queuedOccupant != null || livingOccupant != null; } }
//		public int attackPriority;
//		public bool awarded = false;
//
//		public Cell(){}
//
//		public Cell(JObject source, Board.BoardState parentState) {
//			this.guid = (string)source [CELL_ID_KEY];
//			this.relativeCoordinates = new Vector2 ((float)source [COORD_X_KEY], (float)source [COORD_Y_KEY]);
//			this.maxHp = (int)source [MAX_HP_KEY];
//			this.width = (float)source [WIDTH_KEY];
//			this.height = (float)source [HEIGHT_KEY];
//			this.attack = (int)source [ATTACK_KEY];
//			this.connectors = new List<string> ();
//			JArray connectorsSrc = (JArray)source [CONNECTORS_KEY];
//			foreach (string connector in connectorsSrc.Values<string>()) {
//				this.connectors.Add (connector);
//			}
//			this.worth = (int)source [WORTH_KEY];
//			this.parentState = parentState;
//			this.attackPriority = (int)source [PRIORITY_KEY];
//		}
//
//		public Cell(Cell original, Board.BoardState parentState){
//			this.guid = original.guid;
//			this.relativeCoordinates = new Vector2 (original.relativeCoordinates.x, original.relativeCoordinates.y);
//			this.maxHp = original.maxHp;
//			this.width = original.width;
//			this.height = original.height;
//			this.attack = original.attack;
//			this.connectors = new List<string> ();
//			foreach (string connector in original.connectors) {
//				this.connectors.Add (connector);
//			}
//			this.worth = original.worth;
//			this.parentState = parentState;
//			this.attackPriority = original.attackPriority;
//		}
//
//		public void Attack(Slice slice) {
//			slice.hp -= attack;
//			if (slice.hp < 0) {
//				slice.hp = 0;
//			}
//		}
//
//		public void ProcessChange(string key, string value) {
//			switch (key) {
//				
//			case COORD_X_KEY:
//				this.relativeCoordinates.x = float.Parse (value);
//				break;
//
//			case COORD_Y_KEY:
//				this.relativeCoordinates.y = float.Parse (value);
//				break;
//
//				// TODO: Other available changes
//				
//			};
//		}
	}

}
