using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerCasting : MonoBehaviour {

	public static float DistanceFromTarget;
	public float ToTarget;


	void Update () {
		RaycastHit Hit;
		if (Physics.Raycast (transform.position, transform.TransformDirection (Vector3.forward), out Hit)) {
			ToTarget = Hit.distance;
			DistanceFromTarget = ToTarget;
		}
	}

}





//========================

//using UnityEngine.UI;
/*
public class DoorCellOpen : MonoBehaviour {

	public float TheDistance;
	public GameObject ActionDisplay;
	public GameObject ActionText;
	public GameObject TheDoor;
	public AudioSource CreakSound;

	void Update () {
		TheDistance = PlayerCasting.DistanceFromTarget;
	}

	void OnMouseOver () {
		if (TheDistance <= 2) {
			ActionDisplay.SetActive (true);
			ActionText.SetActive (true);
		}
		if (Input.GetButtonDown("Action")) {
			if (TheDistance <= 2) {
				this.GetComponent<BoxCollider>().enabled = false;
				ActionDisplay.SetActive(false);
				ActionText.SetActive(false);
				TheDoor.GetComponent<Animation> ().Play ("DoorOpenAnim2");
				CreakSound.Play ();
			}
		}
	}

	void OnMouseExit() {
		ActionDisplay.SetActive (false);
		ActionText.SetActive (false);
	}
}

*/