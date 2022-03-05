using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerCode : MonoBehaviour
{

    Rigidbody2D rb;
    Animator anim;
    int speed = 7;
    public int jumpForce;
    public LayerMask groundLayer;
    public Transform Feet;
    public GameObject bulletPrefab;
    public AudioClip shootSound;
    int bulletForce = 500;
    bool grounded = false;
    float groundCheckDis = 0.1f;
    AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(Feet.position, groundCheckDis, groundLayer);
        anim.SetBool("Grounded", grounded);
    }

    // Update is called once per frame
    void Update()
    {
        // moving the player left and right
        float xSpeed = Input.GetAxis("Horizontal") * speed;
        rb.velocity = new Vector2(xSpeed, rb.velocity.y);
        anim.SetFloat("Speed", Mathf.Abs(xSpeed));
        

        if(grounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(new Vector2(0, jumpForce));
        }

        if(xSpeed > 0 && transform.localScale.x < 0 || xSpeed < 0 && transform.localScale.x > 0)
        {
            transform.localScale *= new Vector2(-1, 1);
        }

        if(Input.GetButtonDown("Fire1"))
        {
            _audioSource.PlayOneShot(shootSound);
            anim.SetTrigger("Shoot");
            GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            newBullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(bulletForce * transform.localScale.x, 0));
        }
    }
}