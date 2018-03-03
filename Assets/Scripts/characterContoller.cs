using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterContoller : MonoBehaviour {
    public float maxSpeed = 20f;
    public float jumpForce = 2000f;
    bool facingRight = true;
    bool grounded = false;
   // public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    public float score;
    public float move;
    public float spawnx, spawny;

    private GameObject star;

    // new code
   Animator anim;


    void Start()
    {
        
        spawnx = transform.position.x;
        spawny = transform.position.y;
   
       anim = GetComponent<Animator>();

    }


    void FixedUpdate()
    {
        move = Input.GetAxis("Horizontal");
    }

    void Update()
    {
        if (grounded && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            grounded = false;
            anim.SetBool("Jump", true);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
        }
        GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

        if (move > 0 && !facingRight)
            Flip();
        else if (move < 0 && facingRight)
            Flip();

 
        if (move == 0)
        {
            anim.SetBool("Walk", false);
        }
        else
        {
            anim.SetBool("Walk", true);
        }
    


        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKey(KeyCode.R))
        {
            Application.LoadLevel(Application.loadedLevel);
        }

  
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag == "Grass")
        {
            grounded = true;
            anim.SetBool("Jump", false);
        }
        if (col.gameObject.tag == "Pila" || col.gameObject.name == "dieCollider")
            transform.position = new Vector3(spawnx, spawny, transform.position.z);

        if (col.gameObject.name == "EndLevel")
            Application.LoadLevel("Level3");

        if (col.gameObject.name == "EndLevel1")
            Application.LoadLevel("2Level");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.gameObject.name == "dieCollider") || (col.gameObject.tag == "Pila"))
            Application.LoadLevel(Application.loadedLevel);

        if (col.gameObject.tag == "Burg")
        {
            score++;
            Destroy(col.gameObject);

        }
       

    }

    void OnGUI()
    {
        GUI.Box(new Rect(0, 0, 100, 100), " Hamburgers : " + score);
    }


    
}



