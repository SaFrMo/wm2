using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WhiteMask.Builder {
	
	public class CellLink : MonoBehaviour {

		public Cell cell;

		// UI presentation
		public Text cellInfo;
		public GameObject directionTogglesWrap;
		public Button selectCell;

		public void Refresh() {
			cellInfo.text = "ID: " + cell.id + "\nName: " + cell.name;

			RefreshDirectionColors ();

			selectCell.onClick.AddListener (() => {
				FindObjectOfType<CellBuilder>().SelectCell(cell.id);
			});
		}

		public void LinkTo(Cell newCell){
			cell = newCell;

			// refresh info
			Refresh ();
		}

		public void ToggleDirection(string direction){
			if (cell.directions.Contains (direction)) {
				cell.directions.Remove (direction);
			} else {
				cell.directions.Add (direction);
			}

			RefreshDirectionColors ();
		}

		private void RefreshDirectionColors(){
			foreach (Transform child in directionTogglesWrap.transform) {
				child.GetComponent<Image> ().color = cell.directions.Contains (child.name) ? Color.green : Color.red;
			}
		}
	}

}
