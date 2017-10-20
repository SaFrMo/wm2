using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace WhiteMask.Builder {
	
	public class CellBuilder : MonoBehaviour {

		public BoardBuilder boardBuilder;
		public string selectedCellId = string.Empty;

		void Start(){
			id.onEndEdit.AddListener (newValue => {
				cell.id = newValue;
				selectedCellId = newValue;
				Refresh();
			});

			cellName.onEndEdit.AddListener (newValue => {
				cell.name = newValue;
				Refresh();
			});

			health.onEndEdit.AddListener (newValue => {
				int.TryParse(newValue, out cell.health);
				Refresh();
			});

			attack.onEndEdit.AddListener (newValue => {
				int.TryParse(newValue, out cell.attack);
				Refresh();
			});

			worth.onEndEdit.AddListener (newValue => {
				int.TryParse(newValue, out cell.worth);
				Refresh();
			});
		}

		public void SelectCell(string newId){
			selectedCellId = newId;
			Refresh ();
		}

		// UI fields
		public InputField id, cellName, health, attack, worth;

		private Cell cell { 
			get { return boardBuilder.masterCellList.FirstOrDefault<Cell> (x => x.id == selectedCellId); } 
		}
	
		public void Refresh() {
			if (selectedCellId == string.Empty && boardBuilder.masterCellList.Count > 0) {
				selectedCellId = boardBuilder.masterCellList [0].id;
			}

			// Refresh main builder
			boardBuilder.RefreshBuilder();

			// Dump current information
			id.text = cell.id;
			cellName.text = cell.name;
			health.text = cell.health.ToString();
			attack.text = cell.attack.ToString();
			worth.text = cell.worth.ToString();
		}

	}

}