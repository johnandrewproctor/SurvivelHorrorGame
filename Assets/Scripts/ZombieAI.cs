using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAI : MonoBehaviour {

    public GameObject thePlayer;
    public GameObject theEnemy;
    public float enemySpeed = 0.01f;
    public bool attackTrigger = false;
    public bool isAttacking = false;

	void Update () {
        transform.LookAt(thePlayer.transform);
        if (attackTrigger == false)
        {
            enemySpeed = 0.01f;
            theEnemy.GetComponent<Animation>().Play("walk");
            transform.position = Vector3.MoveTowards(transform.position, thePlayer.transform.position, enemySpeed);
        }
        if (attackTrigger == true && isAttacking == false)
        {
            enemySpeed = 0;
            theEnemy.GetComponent<Animation>().Play("attack");
            StartCoroutine(InflictDamage());
        }

	}

    void OnTriggerEnter()
    {
        attackTrigger = true;
    }

    void OnTriggerExit()
    {
        attackTrigger = false;
    }


    IEnumerator InflictDamage()
    {
        isAttacking = true;
        yield return new WaitForSeconds(1.1f);
        GlobalHealth.currentHealth -= 5;
        yield return new WaitForSeconds(0.2f);
        isAttacking = false;
    }

}
