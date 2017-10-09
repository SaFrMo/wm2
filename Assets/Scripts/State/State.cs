using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace WhiteMask {
	public class State : MonoBehaviour {

		public string pathToLevel = "Sandbox/sandbox";
		public Board board;

		protected virtual void Start(){
			// Ignore loading on Board Builder level
			if (pathToLevel.Length <= 0) {
				board = new Board ();
				return;
			}

			board = new Board (Resources.Load<TextAsset>(pathToLevel));
		}

//		protected virtual void Awake(){
//			//board = new Board (Resources.Load<TextAsset>("Levels/sandbox"), this);
//			//currentTurn = new Turn (this);
//			//ephemeral = new Ephemeral (Resources.LoadAll<TextAsset>("Slices"));
//		}

		// Current and past states
		//public Turn currentTurn;
//		public List<Turn> history = new List<Turn>();
//
//		// Non-recorded level and user information
//		public Board board;
//		public Ephemeral ephemeral;
//
//		public void FinishTurn() {
//			
//			// Cycle through Cells and update occupants
//			foreach (Cell cell in board.currentState.cellsList) {
//				// Apply damage
//				if (cell.livingOccupant != null) {
//					Slice slice = GetSliceById (cell.livingOccupant);
//					if (slice.attackPriority > cell.attackPriority) {
//						if (slice.hp > 0) {
//							slice.Attack (cell);
//						}
//						if (cell.hp > 0) {
//							cell.Attack (slice);
//						}
//					} else {
//						if (cell.hp > 0) {
//							cell.Attack (slice);
//						}
//						if (slice.hp > 0) {
//							slice.Attack (cell);
//						}
//					}
//				}
//				// Create queued Slices
//				if (cell.queuedOccupant != null) {
//					Slice toPlace = new Slice (ephemeral.sliceDictionary [cell.queuedOccupant], true);
//					currentTurn.placedSlices [cell.guid] = toPlace;
//					cell.livingOccupant = toPlace.guid;
//					cell.queuedOccupant = null;
//
//				} else if (cell.occupant == null && currentTurn.placedSlices.ContainsKey(cell.guid)) {
//					// Remove Slice from Cell when empty
//					currentTurn.placedSlices.Remove(cell.guid);
//				} 
//			}
//
//			// Apply in-game calculations
//			currentTurn.ChangeEnergy(currentTurn.energyDelta);
//
//			history.Add (currentTurn);
//
//			currentTurn = new Turn (currentTurn);
//
//		}
//
//		/// <summary>
//		/// Gets the cell by identifier.
//		/// </summary>
//		/// <returns>The cell by identifier.</returns>
//		/// <param name="id">Identifier.</param>
//		public Cell GetCellById(string id){
//			return board.currentState.cellsList.FirstOrDefault<Cell> (x => x.guid == id);
//		}
//
//		/// <summary>
//		/// Gets the slice by identifier.
//		/// </summary>
//		/// <returns>The slice by identifier.</returns>
//		/// <param name="id">Identifier.</param>
//		public Slice GetSliceById(string id){
//			return currentTurn.placedSlices.Values.FirstOrDefault<Slice> (x => x.guid == id);
//		}

	}
}