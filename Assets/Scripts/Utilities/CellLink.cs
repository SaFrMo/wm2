using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

namespace WhiteMask.Builder {
	
	public class CellLink : MonoBehaviour {

		public Cell cell;
		public BoardBuilder builder;

		// UI presentation
		public Text cellInfo;
		public GameObject directionTogglesWrap;
		public Button selectCell;
		public Image selectCellButtonImage;
		public Image attackImage, healthImage, worthImage;
		public Text attackBrief, healthBrief, worthBrief;

		public void Refresh() {
			cellInfo.text = "ID: " + cell.id + "\nName: " + cell.name;

			RefreshDirectionColors ();
			RefreshStats ();

			selectCell.onClick.AddListener (() => {
				FindObjectOfType<CellBuilder>().SelectCell(cell.id);
			});

			selectCellButtonImage.color = cell.id == builder.cellBuilder.selectedCellId ? new Color(0, 208f, 0) : Color.white;
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

		private void RefreshStats(){
			// Compare this cell's stats with the board as a whole
			int 
				lowestAttack = int.MaxValue,
				highestAttack = int.MinValue, 
				lowestHealth = int.MaxValue, 
				highestHealth = int.MinValue, 
				lowestWorth = int.MaxValue, 
				highestWorth = int.MinValue;

			for (int i = 0; i < builder.gridWidth * builder.gridHeight; i++) {
				Cell curr = builder.masterCellList [i];
				if (curr.attack < lowestAttack)
					lowestAttack = curr.attack;
				if (curr.attack > highestAttack)
					highestAttack = curr.attack;
				if (curr.health < lowestHealth)
					lowestHealth = curr.health;
				if (curr.health > highestHealth)
					highestHealth = curr.health;
				if (curr.worth < lowestWorth)
					lowestWorth = curr.worth;
				if (curr.worth > highestWorth)
					highestWorth = curr.worth;
			}
				
			float relativeAttack = (float)(cell.attack - lowestAttack) / (float)(highestAttack - lowestAttack);
			relativeAttack = float.IsNaN (relativeAttack) ? 1f : relativeAttack;
			float relativeHealth = (float)(cell.health - lowestHealth) / (float)(highestHealth - lowestHealth);
			relativeHealth = float.IsNaN (relativeHealth) ? 1f : relativeHealth;
			float relativeWorth = (float)(cell.worth - lowestWorth) / (float)(highestWorth - lowestWorth);
			relativeWorth = float.IsNaN (relativeWorth) ? 1f : relativeWorth;

			attackBrief.text = "A: " + cell.attack + " (" + Math.Round((decimal)relativeAttack, 2) + ")";
			healthBrief.text = "H: " + cell.health + " (" + Math.Round((decimal)relativeHealth, 2) + ")";
			worthBrief.text = "$: " + cell.worth + " (" + Math.Round((decimal)relativeWorth, 2) + ")";

		}
	}

}
