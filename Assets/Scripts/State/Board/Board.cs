using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using SaFrLib;

namespace WhiteMask {

	[System.Serializable]
	public class Board {

		public List<Cell> cells = new List<Cell>();
		public List<BoardState> boardStates = new List<BoardState>();
		public BoardState currentState;

		public Board(){
			boardStates.Add (new BoardState ());
			currentState = boardStates [0];
		}

		public Board(TextAsset snapshot){}

		[System.Serializable]
		public class BoardState {
			public BoardState( string id = "0", string name = "State 1"){
				this.id = id;
				this.name = name;
			}

			public string id = "0";
			public string name = "State 1";
		}

		public BoardState GetStateById(string id){
			return boardStates.FirstOrDefault<BoardState> (x => x.id == id);
		}

		public Cell GetCellById(string id){
			return cells.FirstOrDefault<Cell> (x => x.id == id);
		}

//		const string 
//			DEFAULT_CELL_SLUG = "default", 
//			STATE_NAME_KEY = "name", 
//			ENERGY_DELTA_KEY = "decay", 
//			STATE_LIST_KEY = "states", 
//			CELLS_LIST_KEY = "cells", 
//			STATE_ID_KEY = "name",
//			STATE_CHANGES_KEY = "changes",
//			CELL_ID_KEY = "id",
//			STARTING_ENERGY_KEY = "energy",
//			MAX_ENERGY_KEY = "maxEnergy";
//
//		public Dictionary<string, BoardState> states = new Dictionary<string, BoardState> ();
//		public List<string> stateNameList;
//		public List<BoardState> boardStateList;
//		public string currentStateName = "Default";
//		public BoardState currentState { get { return this.states[this.currentStateName]; } }
//		public string name;
//		public string slug;
//		public int startingEnergyDelta;
//		public int startingEnergy;
//		public int startingMaxEnergy;
//		public State state;
//
//		public BoardActor actor;
//
//		public Board(){}
//
//		/// <summary>
//		/// Initializes a new instance of the <see cref="WhiteMask.Board"/> class based on a JSON source file.
//		/// </summary>
//		/// <param name="source">Source.</param>
//		public Board( TextAsset source, State newState ){
//			JObject j = JObject.Parse(source.text);
//
//			this.name = (string)j.GetValue (STATE_NAME_KEY);
//			this.slug = this.name.ToLower().Replace (' ', '-');
//			this.startingEnergyDelta = (int)j.GetValue (ENERGY_DELTA_KEY);
//			this.startingEnergy = (int)j.GetValue (STARTING_ENERGY_KEY);
//			this.startingMaxEnergy = (int)j.GetValue (MAX_ENERGY_KEY);
//
//			this.state = newState;
//
//			JToken board = j[STATE_LIST_KEY];
//			BuildBoardStates (board);
//
//			// Build the in-game BoardActor
//			this.actor = GameObject.FindObjectOfType<BoardActor>();
//			this.actor.state = this.state;
//		}
//
//		/// <summary>
//		/// Builds the states for the board, using the given default state and its changes.
//		/// </summary>
//		/// <param name="json">Json.</param>
//		private void BuildBoardStates(JToken jStates){
//
//			stateNameList = new List<string>();
//			boardStateList = new List<BoardState> ();
//
//			// Find and build the default Cell (first cell with the name "Default")
//			JToken defaultStateJson = jStates.Values<JObject> ().Where (m => ((string)m [STATE_NAME_KEY]).ToLower () == DEFAULT_CELL_SLUG).FirstOrDefault ();
//			BoardState defaultState = new BoardState (defaultStateJson, this);
//			states [defaultState.stateId] = defaultState;
//			stateNameList.Add (defaultState.stateId);
//			boardStateList.Add (defaultState);
//
//			// Build the rest of the board states
//			foreach (JToken state in jStates.Children()) {
//				
//				string slug = ((string)state [STATE_NAME_KEY]).ToLower ();
//
//				// Ignore the default state
//				if (slug == DEFAULT_CELL_SLUG)
//					continue;
//
//				BoardState newState;
//
//				// If we've declared changes, apply those changes to the default state
//				if (state [STATE_CHANGES_KEY] != null) {
//					newState = new BoardState (defaultState, (JObject)state, this);
//				} else {
//					// Otherwise, create a new BoardState from scratch
//					newState = new BoardState (state, this);
//				}
//
//				// Save the newly-built state
//				states [newState.stateId] = newState;
//				stateNameList.Add (newState.stateId);
//				boardStateList.Add (newState);
//
//			}
//		}
//
//		[System.Serializable]
//		public class BoardState {
//
//			public Dictionary<string, Cell> cells = new Dictionary<string, Cell>();
//			public List<Cell> cellsList;
//			public string stateId;
//			public Board board;
//
//			public BoardState(JToken stateSource, Board newBoard){
//				this.stateId = (string)stateSource[STATE_ID_KEY];
//				board = newBoard;
//
//				JArray cellsToParse = (JArray)stateSource[CELLS_LIST_KEY];
//
//				foreach(JObject cellToParse in cellsToParse) {
//					Cell cell = new Cell(cellToParse, this);
//					this.cells[cell.guid] = cell;
//				}
//
//				this.cellsList = cells.Values.ToList();
//			}
//
//			/// <summary>
//			/// Initializes a new instance of the <see cref="WhiteMask.Board+BoardState"/> class. Pulls in the default state and makes changes to that state.
//			/// </summary>
//			/// <param name="defaultState">Default state.</param>
//			/// <param name="stateSource">State source.</param>
//			public BoardState(BoardState defaultState, JObject stateSource, Board newBoard) {
//				board = newBoard;
//				// Save this state ID
//				this.stateId = (string)stateSource[STATE_ID_KEY];
//
//				// Prep the changes to parse
//				JArray changesToParse = (JArray)stateSource[STATE_CHANGES_KEY];
//
//				// Make a copy of the default state's cells
//				//this.cells = new Dictionary<string, Cell>(defaultState.cells);
//
//				foreach(KeyValuePair<string, Cell> kvp in defaultState.cells){
//					this.cells[kvp.Key] = new Cell(kvp.Value, this);
//				}
//
//				foreach (Cell cell in this.cells.Values.ToList()){
//					cell.parentState = this;
//				}
//
//				// Apply each change to the default cell
//				foreach(JObject changeToParse in changesToParse){
//					string cellId = (string)changeToParse[CELL_ID_KEY];
//					List<string> keys = changeToParse.Properties().Select(p => p.Name).ToList();
//					List<string> values = new List<string>();
//					foreach( string key in keys ){
//						values.Add( (string)changeToParse[(string)key] );
//					}
//
//					for(int i = 0; i < keys.Count; i++){
//						Cell toEdit = this.cells.FirstOrDefault(x => x.Value.guid == cellId).Value;
//						toEdit.ProcessChange(keys[i], values[i]);
//					}
//				}
//
//				this.cellsList = cells.Values.ToList();
//			}
//		}
	}
}
