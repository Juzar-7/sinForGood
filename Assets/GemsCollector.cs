using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemsCollector : MonoBehaviour
{
    public int BlueHealth = 10;
    public int RedHealth = 30;
    public Slider BlueHealthBar;
    public Slider RedHealthBar;
    public GameObject particleEffect, particleEffect2;
    AudioSource collect;
    Animator animator;
    public int RedCollection = 0;
    public int BlueCollection = 0;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        collect = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        BlueHealthBar.value = BlueHealth;
        RedHealthBar.value = RedHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hoo");
        if (other.tag=="BlueGem")
        {
            collect.Play();
            //Debug.Log("GemCollectedBlue");
            BlueHealth = BlueHealth + 15;
            BlueCollection++;
            GameObject impact = Instantiate(particleEffect, transform);
            Destroy(impact, 1.5f);
            animator.SetTrigger("PowerUp");
            Destroy(other.gameObject);

        }
        if (other.tag=="RedGem")
        {
            collect.Play();
            RedCollection++;
            GameObject impact = Instantiate(particleEffect2, transform);
            Destroy(impact, 1.5f);
            RedHealth = RedHealth + 15;
            animator.SetTrigger("PowerUp");
            Destroy(other.gameObject);
        }
    }


    public void increaseRedHealth(int healthGain)
    {
        RedHealth = RedHealth + healthGain;
    }
    public void increaseBlueHealth()
    {
        BlueHealth = BlueHealth + 100;
    }
    public void deacreaseRedHealth(int damage)
    {
        RedHealth = RedHealth - damage;
    }
    public void decreaseBlueHealth(int damage)
    {
        BlueHealth = BlueHealth - damage;
    }


}
