import UnityEngine
		
class ZombieAnim : MonoBehaviour {

	GameObject theZombie;
	}
	public void ChangeAnim ()
	{
		theZombie.GetComponent<Animation>().Play("attack2");

	}

	}

