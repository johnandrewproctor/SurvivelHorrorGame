using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorCellOpen : MonoBehaviour {

	public float TheDistance;
	public GameObject ActionDisplay;
	public GameObject ActionText;
	public GameObject TheDoor;
	public AudioSource CreakSound;
	public GameObject ExtraCross;

	void Update () {
		TheDistance = PlayerCasting.DistanceFromTarget;
	}

	void OnMouseOver () {
		if (TheDistance <= 2) {
			ExtraCross.SetActive(true);
			ActionDisplay.SetActive (true);
			ActionText.SetActive (true);
		}
		if (UltimateJoystick.GetJoystickState("Open"))
		{
			print("You Tapped");
			if (TheDistance <= 2) {
				this.GetComponent<BoxCollider>().enabled = false;
				ActionDisplay.SetActive(false);
				ActionText.SetActive(false);
				print("OPEN DOOR");
				TheDoor.GetComponent<Animation> ().Play ("DoorOpenAnim2");
				CreakSound.Play ();
			}
		}
	}

	void OnMouseExit() {
		ExtraCross.SetActive(false);
		ActionDisplay.SetActive (false);
		ActionText.SetActive (false);
	}
}