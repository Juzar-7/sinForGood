using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public GameObject player;
    public float sensitivity = 10;
    private int angel = 0;
    private int evil = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        
    }

    void HandleInput()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
  

        player.transform.position += new Vector3(horizontal  * Time.deltaTime * sensitivity, 0, vertical  * Time.deltaTime*sensitivity);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided");
        if (other.gameObject.tag == "WhiteGem")
        {
            angel++;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "RedGem")
        {
            evil++;
            Destroy(other.gameObject);
        }

    }



}
