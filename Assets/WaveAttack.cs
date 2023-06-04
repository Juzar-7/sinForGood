using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveAttack : MonoBehaviour
{


    Animator animator;
    followScript src;
    takeDamage TakeDamage;
    AudioSource roarSound;
    public float attackRange = 5f;
    public GemsCollector playerScript;
    public GameObject WaveCircle;
    bool canAttack = true;
    // Start is called before the first frame update
    void Start()
    {
        src = GetComponent<followScript>();
        TakeDamage = GetComponent<takeDamage>();
        animator = transform.GetChild(0).GetComponent<Animator>();
        roarSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (src.distance < attackRange && canAttack && TakeDamage.health>0)
        {
            canAttack = false;
            doAttack();
        }

    }

    private IEnumerator WaitToAttackAgain()
    {
        yield return new WaitForSeconds(10f);
        //src.enabled = true;
        canAttack = true;
    }

    void doAttack ()
    {
        roarSound.Play();
        animator.SetTrigger("Attack");
        //src.enabled = false;
        playerScript.deacreaseRedHealth(30);
   
        GameObject impactEffect = Instantiate(WaveCircle, transform);
        Destroy(impactEffect, 6f);
        StartCoroutine(WaitToAttackAgain());

    }
}
