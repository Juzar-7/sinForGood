using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class giveDamage : MonoBehaviour
{
    public GemsCollector script;
    public GameObject redCircle;
    //public takeDamage damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            GameObject impact = Instantiate(redCircle, GameObject.FindWithTag("Player").transform.GetChild(1).transform);

            script.deacreaseRedHealth(60);
           
            Destroy(impact, 0.5f);

        }
    }
}
