using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AI;


public class PlayerController : MonoBehaviour
{

    private Joystick joystick;
    protected JumpButton jumpbutton;
    Rigidbody rb;
    //public float speed;
    public int jumpForce;
    public float rotationSpeed = 95.0f;
    public AudioSource audioSource;

    Camera cam;

    // Third Person Movement
    //public CharacterController controller;
    //public float speed = 6f;

    // Calling Scripts
    public AIEnemy enemy;
    public Enemy enem;
    public Interactable focus;

    //Attacks and Buttons
    public Transform firePosition;
    public GameObject spell;
    public Transform senPosition;
    public GameObject senSpell;
    public Transform healPosition;
    public GameObject healSpell;
    bool jump;

    //CoolDown
    public Image imageCooldown;
    public float cooldown = 5;
    bool isCooldown;

    //SanetizeCoolDown
    public Image senImageCooldown;
    public float senCooldown = 5;
    bool senIsCooldown;

    //HealCoolDown
    public Image healImageCooldown;
    public float healCooldown = 5;
    bool healIsCooldown;

    //Canvas Death
    public GameObject canvas;

    //Animations
    Animator anim;
    bool idle;
    bool run;
    bool walk;
    bool jumping;

    //Health
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;

    //Time Delay
    public float delayTime = 4f;

    // Call on Start of the Game
    void Start()
    {
        joystick = GameObject.FindWithTag("Joystick").GetComponent<DynamicJoystick>();
        //controller = GetComponent<CharacterController>();
        jumpbutton = FindObjectOfType<JumpButton>();
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Fixed Update for same effect
    private void FixedUpdate()
    {
        //Calling Player Movement
        WalkHandler();
        
    }

    // Keep updating
    void Update()
    {
        //Calling Third Person Movement
        //PCMovement();

        //Calling Touch Controlles
        touchInteraction();

        // Testing Damage 
        if (Input.GetKey(KeyCode.Space))
        {
            TakeDamage(50);
        }

        ////Jump Button & Animation
        //if ((!jump && jumpbutton.Pressed))
        //{
        //    anim.SetBool("jumping", true);

        //    if ((transform.position.y <= 15f))
        //    {

        //        rb.AddForce(Vector3.up * jumpForce);
        //        jump = true;

        //    }
        //}
        //else
        //{
        //    anim.SetBool("jumping", false);
        //}


        //if (jump && jumpbutton.Pressed)
        //{
        //    jump = false;

        //}

        //Attack CoolDown Start
        if (isCooldown)
        {

            imageCooldown.fillAmount += 1 / cooldown * Time.deltaTime;

            if (imageCooldown.fillAmount >= 1)
            {
                imageCooldown.fillAmount = 0;
                isCooldown = false;
            }
        }

        //Sanetize CoolDown Start
        if (senIsCooldown)
        {

            senImageCooldown.fillAmount += 1 / senCooldown * Time.deltaTime;

            if (senImageCooldown.fillAmount >= 1)
            {
                senImageCooldown.fillAmount = 0;
                senIsCooldown = false;
            }
        }

        //Heal CoolDown Start
        if (healIsCooldown)
        {

            healImageCooldown.fillAmount += 1 / healCooldown * Time.deltaTime;

            if (healImageCooldown.fillAmount >= 1)
            {
                healImageCooldown.fillAmount = 0;
                healIsCooldown = false;
            }
        }

        // Kill Player and Death Animation
        if (currentHealth <= 0)
        {
            //Enemies Destroy when Player Dies
            GameObject[] enemies = (GameObject.FindGameObjectsWithTag("Enemy"));
            foreach (GameObject enemy in enemies)
                Destroy(enemy);
            
            //Play Death Animation
            anim.Play("Death");

            //Canvas Disable
            canvas.SetActive(false);

            //Stop Audio
            audioSource.Stop();

            //Delay Scene Change till Death Animation ends
            Invoke("GameOver", delayTime);
        }  
           
    }

    // Scene Change and GameObject Destroy
    void GameOver()
    {
        Destroy(gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    //Player Take Damage If Hit By Enemy
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            TakeDamage(Random.Range(3, 11));
            anim.SetTrigger("hit");

        }
    }
    

    // PC Movement
    //void PCMovement()
    //{
    //    float horizontal = Input.GetAxisRaw("Horizontal");
    //    float vertical = Input.GetAxisRaw("Vertical");
    //    Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

    //    if (direction.magnitude >= 0.1f)
    //    {
    //        controller.Move(direction * speed * Time.deltaTime);
    //    }
    //}

    //TouchCode
    void touchInteraction()
	{

        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Ray ray = cam.ScreenPointToRay(Input.touches[0].position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    SetFocus(interactable);
                }

            }
        }


        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    SetFocus(interactable);
                }
            }

        }

    }

    //Player Movement and animation with camera direction
    void WalkHandler()
    {
        //// Computer Movement

        //float Horizontal = Input.GetAxis("Horizontal");
        //float Vertical = Input.GetAxis("Vertical");

        //Vector3 keymovement = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0) * new Vector3(Horizontal * speed * Time.deltaTime, 0, Vertical * speed * Time.deltaTime);
        //Vector3 move = transform.right * Horizontal + transform.forward * Vertical;
        //rb.AddForce(move * 10);

        //float Protation = Horizontal * rotationSpeed;
        //Protation *= Time.deltaTime;

        //if (keymovement != Vector3.zero) transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(keymovement), 0.15F);

        //{
        //    transform.Rotate(0, Protation, 0);


        //    if (Horizontal != 0 || Vertical != 0)
        //    {
        //        RemoveFocus();
        //        anim.SetBool("running", true);
        //    }
        //    else
        //    {
        //        anim.SetBool("running", false);

        //    }
        //}

        // JoyStick Movement
        //float h = joystick.Horizontal;
        //float v = joystick.Vertical;

        //Vector3 movement = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0) * new Vector3(h * speed * Time.deltaTime, 0, v * speed * Time.deltaTime);
        //Vector3 newPos = transform.position + movement;
        //rb.MovePosition(newPos);

        //Camera Look
        //transform.LookAt(transform.position + new Vector3(rb.velocity.x, 0, rb.velocity.z));

        //float rotation = h * rotationSpeed;
        //rotation *= Time.deltaTime;

        //if (movement != Vector3.zero) transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15F);

        //{
        //    transform.Rotate(0, rotation, 0);


        //    if (h != 0f || v != 0f)
        //    {
        //        RemoveFocus();
        //        anim.SetBool("running", true);
        //    }
        //    else
        //    {
        //        anim.SetBool("running", false);
                
        //    }
        //}
    }

    //Damage Taking
    public void TakeDamage(int damage)

    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    //Jump Button
    public void BtnJump()
    {
        anim.SetTrigger("jumping");
    }

    // Soap Spell
    public void BtnAttack()
    {
        if (isCooldown == false)
        {
            anim.SetTrigger("attack1");
            Instantiate(spell, firePosition.position, firePosition.rotation);
            isCooldown = true;
        }
    }

    // Sanetize Spell
    public void BtnSAttack()
    {
        if (senIsCooldown == false)
        {
            anim.SetTrigger("Sattack");
            Instantiate(senSpell, senPosition.position, senPosition.rotation);
            senIsCooldown = true;
        }
    }

    // Heal Spell
    public void BtnHeal()
    {
        if (currentHealth >= 100)
        {
            healIsCooldown = true;
        }
            else if (currentHealth < 100)
            {
                currentHealth = 100;
                healthBar.SetHealth(currentHealth);
                healIsCooldown = false;
                anim.SetTrigger("heal");
                Instantiate(healSpell, healPosition.position, healPosition.rotation);
                healIsCooldown = true;
            }
        
    }

    //Interaction Focus
    void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
                focus.OnDefocused();

            focus = newFocus;
        }

        newFocus.OnFocused(transform);
    }

    void RemoveFocus()
    {
        if (focus != null)
            focus.OnDefocused();

        focus = null;
    }


}
