using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour {

	[SerializeField] Transform startingAnchor = null;
	[SerializeField] int colums = 8;
	[SerializeField] int rows = 4;
	[SerializeField] List<Button> buttons = new List<Button>();
	[SerializeField] Game game = null;

	Vector3[,] matrix = null;
	bool[,] taken = null;

	void Start () {
		matrix = new Vector3[colums,rows];
		taken = new bool[colums,rows];
		if (startingAnchor != null) {
			startingAnchor = transform;
		}
		Vector3 initialVector = Vector3.zero;//startingAnchor.transform.position;
		Vector3 currentVector = initialVector;

		for(var i = 0; i < colums; i++) {
			for(var ii = 0; ii < rows; ii++) {
				matrix[i,ii] = currentVector;
				taken[i,ii] = false;
				currentVector -= Vector3.forward;
			}
			currentVector = new Vector3(currentVector.x+1,initialVector.y,initialVector.z); 
		}

		StartCoroutine(Init ());
	}

	IEnumerator Init () {
		List<Button> tempButtons = new List<Button>();
		foreach(Button b in buttons){
			tempButtons.Add (b);
		}
		for (var i = 0; i < colums; i++) {
			for (var ii = 0; ii < rows; ii++) {
				yield return null;
				if (!taken [i, ii]) {
					int tries = 0;
					while (true) {

						if(tempButtons.Count <= 0) {
							SetOccupied (i, ii, 1,1);
							break;
						}

						tries++;
						if(tries >= 20) {
							SetOccupied (i, ii, 1,1);
							break;
						}

						Button b = tempButtons [Random.Range (0, tempButtons.Count)];
						//print ("button: " + b.numberOfColums + " " + b.numberOfRows); 
						yield return null;

						if (IsAvailable (i, ii, b.numberOfColums, b.numberOfRows)) {
							GameObject newButton = Instantiate (b.gameObject) as GameObject;
							newButton.transform.parent = startingAnchor;
							newButton.transform.localPosition = matrix [i, ii];
							newButton.transform.localRotation = Quaternion.identity;
							game.AddButton(b);
							tempButtons.Remove(b);
							SetOccupied (i, ii, b.numberOfColums, b.numberOfRows);
							break;
						}
					}
				} 
			}
		}
	}

	bool IsAvailable(int currentColum, int currentRow, int x, int y) {
		//print (currentColum + " " +currentRow + " " +x + " " +y);
		if(currentColum + x-1 >= colums){
			return false;
		}

		if(currentRow + y-1 >= rows){
			return false;
		}

		bool _taken = false;
		int xx = x;
		while (xx > 0) {
			xx--;
			if(taken[currentColum + xx,currentRow]) {
				_taken = true;
			}
		}

		int yy = y;
		while (yy > 0) {
			yy--;
			if(taken[currentColum,currentRow + yy]) {
				_taken = true;
			}
		}

		return !_taken;
	}

	void SetOccupied(int currentColum, int currentRow, int x, int y) {
		for (var i = 0; i < x; i++) {
			for (var ii = 0; ii < y; ii++) {
				taken [currentColum + i, currentRow + ii] = true;
			}
		}
	}
}
