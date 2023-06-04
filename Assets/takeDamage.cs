using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class takeDamage : MonoBehaviour
{
    public int health = 100;
    Animator animator;
    public Slider healthbar;
    GameObject GM; 
    GameManagerScript GMscript;
    // Start is called before the first frame update
    private void Start()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();
        GM = GameObject.FindWithTag("GM");
        GMscript = GM.GetComponent<GameManagerScript>();
        Debug.Log("EnemiesKilled : " + GMscript.enemiesKilled);
    }
    public void takedamage()
    {
        //Debug.Log("health " + health);
        health = health - 30;
        if (health<=0)
        {
            Debug.Log("dead");
            GMscript.enemiesKilled += 1;
            int LayerIgnoreRaycast = LayerMask.NameToLayer("Ignore Raycast");
            gameObject.layer = LayerIgnoreRaycast;
            animator.SetTrigger("Death");
            Destroy(gameObject, 5f);
        }
        else
        {
            animator.SetTrigger("Damage");
            
        }
     
    }
    private void Update()
    {
        healthbar.value = health;
    }

}
