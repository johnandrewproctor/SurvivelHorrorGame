using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDeath : MonoBehaviour {

    public int EnemyHealth = 20;
    public GameObject TheEnemy;
    public int StatusCheck;
	public AudioSource JumpscareMusic;

   void DamageZombie (int DamageAmount)
       {
        EnemyHealth -= DamageAmount;
		//print("DamageZombie2 called:");
		//TheEnemy.GetComponent<Animation>().Stop("walk2");
		print(EnemyHealth);
      }




    void Update () {
		//print("Update called");
		//print(EnemyHealth);
		if (EnemyHealth <= 0 && StatusCheck == 0)
        {
			print("EnemyHealth for Zombie2");
            StatusCheck = 2;
            TheEnemy.GetComponent<Animation>().Stop("walk2");
            TheEnemy.GetComponent<Animation>().Play("fallingback");
			JumpscareMusic.Stop();
			print("After fallingback");
        }
	}
}
