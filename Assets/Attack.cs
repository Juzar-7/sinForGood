using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public followScript src;
    Animator animator;
    AudioSource swoosh;
    public float attackRange = 6f;
    bool canAttack = true;
    // Start is called before the first frame update
    void Start()
    {
        //followScript src = gameObject.GetComponent<followScript>();
        animator = transform.GetChild(0).GetComponent<Animator>();
        swoosh = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
       
       if (src.distance<attackRange && canAttack)
        {
            canAttack = false;
            doAttack();
           



        }
    }
    private IEnumerator attackAgainCoroutine()
    {


        yield return new WaitForSeconds(5f);

        canAttack = true;
    }

    private IEnumerator huihui()
    {
        Debug.Log("Waiting");
        yield return new WaitForSeconds(1f);
        Debug.Log("WaitOver");
        //animator.SetBool("x", false);
        //animator.ResetTrigger("Attack");
    }

    void doAttack()
    {

        //canAttack = false;
        swoosh.Play();
        animator.SetTrigger("Attack");
        //animator.SetBool("x", true);
            StartCoroutine(huihui());
            StartCoroutine(attackAgainCoroutine());
        

    }


}
