using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SaFrLib;

namespace WhiteMask {
	public class BoardBuilder : MonoBehaviour {

		private Board board;
		public Board.BoardState currentBoardState;

		void Start(){
			board = FindObjectOfType<State> ().board;

			// UI
			currentBoardState = board.currentState;
			currentStateId.text = currentBoardState.id;
			allBoardStates.Setup<Board.BoardState> (
				board.boardStates.ToArray(),
				(button, boardState) => {
					RefreshStateButton (button, boardState);
				}
			);
		}

		public void AddCell(){
			
		}


		// UI
		public Text currentStateId;
		public MenuRefresher allBoardStates;

		private void RefreshStateButton(GameObject button, Board.BoardState boardState){
			Button b = button.GetComponent<Button> ();
			Text t = button.GetComponentInChildren<Text> ();

			b.onClick.AddListener (() => {
				currentBoardState = board.GetStateById(boardState.id);
				currentStateId.text = currentBoardState.id;
			});

			t.text = boardState.id;
		}


		// Update
		void Update(){
			//board.currentState.id = currentStateId.text;
		}
	}
}