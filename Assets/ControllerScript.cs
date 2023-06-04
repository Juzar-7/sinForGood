using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;



public class ControllerScript : MonoBehaviour
{
    Animator animator;
    float velocityZ = 0.0f;
    float velocityX = 0.0f;
    public float acceleration = 2.0f;
    public float deceleration = 2.0f;
    public GameObject sword;
    private int f = 1;
    private CharacterController characterController;
    private int layerMask,AngelLayer;
    public float speed = 5f;
    public Transform orientation;
    Vector3 movement;
    public followScript followscript;
    //public takeDamage takedamage;
    public GameManagerScript GM;
    public int isAttacking = 1;
    public Transform spellInitiate;
    public Camera cam;
    Rigidbody rb;
    public GameObject particlesystem;
    public GameObject ImpactEffect;
    AudioSource AngelKill;
    GemsCollector playerHealth;

    public bool isAbleToTakeDamage = true;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        playerHealth = GetComponent<GemsCollector>();
        AngelKill = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        layerMask = 1 << 6;
        AngelLayer = 1 << 7;
    }

    // Update is called once per frame
    void Update()
    {
        bool forwardPressed = Input.GetKey("w");
        bool backwardPressed = Input.GetKey("s");
        bool leftPressed = Input.GetKey("a");
        bool rightPressed = Input.GetKey("d");
        bool runPressed = Input.GetKey("left shift");


        // forward motion
        if (forwardPressed && velocityZ < 0.5f && !runPressed)
        {
            velocityZ += Time.deltaTime * acceleration;

        }
        if (forwardPressed && runPressed && velocityZ < 2.1f)
        {
            velocityZ += Time.deltaTime * acceleration;
        }
        if (forwardPressed && !runPressed && velocityZ > 0.5f)
        {
            velocityZ -= Time.deltaTime * acceleration;
        }

        // backward motion
        if (backwardPressed && velocityZ > -0.51f )
        {
            velocityZ -= Time.deltaTime * acceleration;
            

        }
       



        //Left Motion
        if (leftPressed && velocityX > -0.5f && !runPressed)
        {
            velocityX -= Time.deltaTime * acceleration;

        }
        if (leftPressed && runPressed && velocityX > -2.1f)
        {
            velocityX -= Time.deltaTime * acceleration;
        }
        if (leftPressed && !runPressed && velocityX < -0.5f)
        {
            velocityX += Time.deltaTime * acceleration;
        }


        // Right Motion
        if (rightPressed && velocityX < 0.5f && !runPressed)
        {
            velocityX += Time.deltaTime * acceleration;
       
        }
        if (rightPressed && runPressed && velocityX < 2.1f)
        {
            velocityX += Time.deltaTime * acceleration;
        }
        if (rightPressed && !runPressed && velocityX > 0.5f)
        {
            velocityX -= Time.deltaTime * acceleration;
        }




        if (!forwardPressed && velocityZ>0.0f)
        {
            velocityZ -= Time.deltaTime * deceleration;
        }
        if (!backwardPressed && velocityZ < 0.0f)
        {
            velocityZ += Time.deltaTime * deceleration;
        }
        if (!forwardPressed && !backwardPressed && velocityZ != 0.0f && (velocityZ > -0.05f && velocityZ < 0.05f))
        {
            velocityZ = 0.0f;
        }


        if (!leftPressed && velocityX<0.0f)
        {
            velocityX += Time.deltaTime * deceleration;
        }
        if (!rightPressed && velocityX > 0.0f)
        {
            velocityX -= Time.deltaTime * deceleration;
        }
        if (!leftPressed && !rightPressed && velocityX!=0.0f && (velocityX > -0.05f && velocityX < 0.05f))
        {
            velocityX = 0.0f;
        }


        if (Input.GetButton("Fire1"))
        {

        
            //animator.SetTrigger("swordAttack");

            //animator.SetBool("Attack", true);


            Shoot();

        }


        animator.SetFloat("VelocityX", velocityX);
        animator.SetFloat("VelocityZ", velocityZ);

        HandleInput();

        
       

    }

    public void HandleInput()
    {
        //float v = Input.GetAxis("Vertical");
        //float h = Input.GetAxis("Horizontal");

        //movement += transform.forward * v * speed * Time.deltaTime * 0.01f;
        //movement += transform.right * h * speed * Time.deltaTime * 0.01f;
        ////Vector3 move = new Vector3(Input.GetAxis("Horizontal") * f, 0, Input.GetAxis("Vertical") * f);
        ////characterController.Move(move * Time.deltaTime * speed);
        //characterController.Move(movement);


        float verticalAxis = Input.GetAxis("Vertical");
        float horizontalAxis = Input.GetAxis("Horizontal");
        if (verticalAxis < 0) verticalAxis = verticalAxis / 1.3f;
      
        if (Input.GetKey("left shift") && verticalAxis > 0) verticalAxis = verticalAxis * 2;
        if (Input.GetKey("left shift") && verticalAxis < 0) verticalAxis = verticalAxis * 1.3f;
        if (Input.GetKey("left shift") ) horizontalAxis = horizontalAxis * 1.5f;
        Vector3 movement = this.transform.forward * verticalAxis*isAttacking*GM.isNotDead + this.transform.right * horizontalAxis*isAttacking*GM.isNotDead;
        //movement.Normalize();

        this.transform.position += movement *Time.deltaTime*speed;
    }



    private IEnumerator ChangeFValueCoroutine()
    {
        // Store the previous value of 'f'
     

        // Set 'f' to 0
   
        //isAttacked = true;
        // Wait for 2 seconds
        yield return new WaitForSeconds(1f);

        // Restore the previous value of 'f'
        f = 1;
        isAbleToTakeDamage = true;
        isAttacking = 1;
    }


    void Shoot()
    {
        RaycastHit hit;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 10f, layerMask))
        {
            //Debug.Log(hit.transform.tag);
            //Debug.Log("hiihhiiiihihhihi");

            takeDamage src = hit.transform.GetComponent<takeDamage>();

            if (src!=null && isAbleToTakeDamage )
            {
                isAbleToTakeDamage = false;
                isAttacking = 0;
                src.takedamage();
                StartCoroutine(ChangeFValueCoroutine());
                animator.SetTrigger("swordAttack");
                playerHealth.deacreaseRedHealth(30);
                //rb.AddForce(transform.up*2.5f, ForceMode.Impulse);
                GameObject eff = Instantiate(particlesystem, transform);
                Destroy(eff, 1f);
                GameObject impact = Instantiate(ImpactEffect, hit.transform);
                Destroy(impact, 1f);


            }

            

        }
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 10f, AngelLayer))
        {
            Debug.Log(hit.transform.tag);

           Sacrifice sacrifice = hit.transform.parent.GetComponent<Sacrifice>();

            if (sacrifice!=null && isAbleToTakeDamage)
            {
                isAbleToTakeDamage = false;
                AngelKill.Play();
                StartCoroutine(ChangeFValueCoroutine());
                animator.SetTrigger("swordAttack");
               

                sacrifice.Die();
                playerHealth.decreaseBlueHealth(150);
                playerHealth.increaseRedHealth(150);

                //rb.AddForce(transform.up*2.5f, ForceMode.Impulse);
                GameObject eff = Instantiate(particlesystem, transform);
                Destroy(eff, 1f);
                GameObject impact = Instantiate(ImpactEffect, hit.transform);
                Destroy(impact, 1f);


            }



        }

    }


   public void changeAttackFlag()
   {
        isAbleToTakeDamage = true;
   }

}
