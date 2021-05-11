using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jpFirst : MonoBehaviour
{
	Animator anim;
	//int jumpHash = Animator.StringToHash("Jump");

	public int EnemyHealth = 20;
	public GameObject TheEnemy;
	public int StatusCheck;
	public AudioSource JumpscareMusic;


    // Start is called before the first frame update
    void Start()
       {
		anim = GetComponent<Animator>();
		//anim.Play("walk",-1,0f);
     }

	void DamageZombie (int DamageAmount)
	  {
		EnemyHealth -= DamageAmount;
		print("DamageZombie called from first zombie:");
	  }


    // Update is called once per frame
    void Update()
    {
		//print (DamageAmount);
		//anim.Play("fallingback",-1,0f);

		if (EnemyHealth <= 0 && StatusCheck == 0)
		{
			print(EnemyHealth);
			StatusCheck = 2;
			//anim.Play("walk",-1,0f);
			//anim.Play("attack",-1,0f);
			anim.Play("fallingback",-1,0f);
			JumpscareMusic.Stop();
			print("EnemyHelth..");
			//TheEnemy.GetComponent<Animation>().Stop("walk");
			//TheEnemy.GetComponent<Animation>().Play("fallingback");




		}






		if(Input.GetKeyDown("1"))
			{
				anim.Play("attack",-1,0f);
			    print ("I pressed attack1");


			}


		if(Input.GetKeyDown("2"))
		{
			anim.Play("fallingback",-1,0f);
			print ("I pressed fallingback");


		}


		if(Input.GetKeyDown("3"))
		{
			anim.Play("walk",-1,0f);
			print ("I pressed fallingback");


		}




		if(Input.GetKeyDown("4"))
		{
			
			print (EnemyHealth);
			//print (DamageAmount);

		}




		/*
		float move = Input.GetAxis("Vertical");
		anim.SetFloat("Speed", move);

		if(Input.GetKeyDown(KeyCode.Space))
		{
			anim.SetTrigger (jumpHash);
		}
		*/

    }
}
