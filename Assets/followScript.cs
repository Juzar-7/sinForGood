using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followScript : MonoBehaviour
{
    public Transform target;
    public float followSpeed = 5.0f;
    public float min_dis = 20.0f;
    public float distance=10.0f;
    takeDamage takedamage;
    Animator animator;
    bool canAttck = true;
    // Start is called before the first frame update
    void Start()
    {
        takedamage = GetComponent<takeDamage>();
        animator = transform.GetChild(0).GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {


        distance = Vector3.Distance(target.position, transform.position);
        //Debug.Log(takedamage.health);
        if (takedamage.health>0 && distance>=min_dis)
        {
            transform.LookAt(target);
            transform.Translate(Vector3.forward * followSpeed * Time.deltaTime);
        }
       
    }


    
}
