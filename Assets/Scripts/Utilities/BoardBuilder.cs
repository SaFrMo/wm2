using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SaFrLib;
using System.Linq;

namespace WhiteMask.Builder {
	public class BoardBuilder : MonoBehaviour {

		private Board board;
		public Board.BoardState currentBoardState;
		public Cell currentCell;

		void Start(){
			board = FindObjectOfType<State> ().board;

			// UI
			currentBoardState = board.currentState;
			currentStateId.text = currentBoardState.id;
			RefreshBuilder ();
		}

		public void AddCell(){
			AddCell ("0");
		}

		public Cell AddCell(string id, string name = "New Cell") {
			// Make sure our ID is unique
			while (board.cells.FindAll (x => x.id == id).Count > 0) {
				id = SaFrMo.GenerateRandomString ();
			}
			Cell newCell = new Cell (id, name);
			board.cells.Add (newCell);
			RefreshBuilder ();
			return newCell;

		}

		public void AddState(){
			AddState ("0");
		}

		public Board.BoardState AddState(string id, string name = "New State") {
			// Make sure our ID is unique
			while (board.boardStates.FindAll (x => x.id == id).Count > 0) {
				id = SaFrMo.GenerateRandomString ();
			}
			Board.BoardState newState = new Board.BoardState (id, name);
			board.boardStates.Add (newState);
			RefreshBuilder ();
			return newState;
		}

		// Builders
		public MenuRefresher cellActors;

		// UI
		public Text currentStateId;
		public MenuRefresher allBoardStates;
		public MenuRefresher allCells;

		// Grid
		public GridLayoutGroup boardGrid;
		public float gridWidth = 3f, gridHeight = 3f;
		public void ChangeRows(float delta){
			gridHeight += delta;
			gridHeight = Mathf.Clamp (gridHeight, 0, 10f);
			RefreshBuilder ();
		}
		public void ChangeColumns(float delta){
			gridWidth += delta;
			gridWidth = Mathf.Clamp (gridWidth, 0, 10f);
			RefreshBuilder ();
		}

		public void RefreshBuilder(){
			// Refresh board state selectors
			allBoardStates.Setup<Board.BoardState> (
				board.boardStates.ToArray(),
				(button, boardState) => {
					RefreshStateButton (button, boardState);
				}
			);

			// Refresh grid size
			RectTransform boardRect = (RectTransform)boardGrid.transform;
			boardGrid.cellSize = new Vector2(boardRect.sizeDelta.x / gridWidth, boardRect.sizeDelta.y / gridHeight);

			// Refresh cell selectors
			allCells.Setup<Cell> (
				board.cells.ToArray(),
				(button, cell) => {
					RefreshCellButton (button, cell);
				}
			);

			// Make sure each cell has a GameObject
			cellActors.Setup<Cell> (
				board.cells.ToArray (),
				(createdObject, originalCell) => {

				}
			);
		}

		private void RefreshStateButton(GameObject button, Board.BoardState boardState){
			Button b = button.GetComponent<Button> ();
			Text t = button.GetComponentInChildren<Text> ();

			b.onClick.AddListener (() => {
				currentBoardState = board.GetStateById(boardState.id);
				currentStateId.text = currentBoardState.id;
			});

			t.text = boardState.id;
		}

		private void RefreshCellButton(GameObject button, Cell cell) {
			Button b = button.GetComponent<Button> ();
			Text t = button.GetComponentInChildren<Text> ();

			b.onClick.AddListener (() => {
				currentCell = board.GetCellById(cell.id);
			});

			t.text = cell.id;
		}


		// Update
		void Update(){
			//board.currentState.id = currentStateId.text;
		}
	}
}