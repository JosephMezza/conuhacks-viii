using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 0.5f;
    private Rigidbody2D rb;
    private Vector2 input;
    private Vector2 lastMoveDirection;

    public Transform Aim;

    public bool isWalking = false;

    Animator anim;
    private bool facingLeft = true;

    public HealthBar health;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
        Animate();
        if (input.x< 0 && !facingLeft || input.x >0 && facingLeft) {
            Flip();
        }
    }

    private void FixedUpdate(){
        rb.velocity = input * speed;
        if(isWalking){
            Vector3 vector3 = Vector3.left * input.x + Vector3.down * input.y;
            Aim.rotation = Quaternion.LookRotation(Vector3.forward, vector3);
        }
    }

    void ProcessInputs() {

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if((moveX == 0 && moveY == 0) && (input.x != 0 || input.y != 0)){
            isWalking = false;
            lastMoveDirection = input;
            Vector3 vector3 = Vector3.left * lastMoveDirection.x + Vector3.down * lastMoveDirection.y;
            Aim.rotation = Quaternion.LookRotation(Vector3.forward, vector3);
        }else if(moveX != 0 || moveY != 0){
            isWalking = true;
        }

        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        input.Normalize();

    }

    void Animate() {
        anim.SetFloat("MoveX", input.x);
        anim.SetFloat("MoveY", input.y);
        anim.SetFloat("MoveMagnitude", input.magnitude);
        anim.SetFloat("LastMoveX", lastMoveDirection.x);
        anim.SetFloat("LastMoveY", lastMoveDirection.y);
    }

    void Flip() {
        Vector3 scale = transform.localScale;
        //flip sprite on x axis by multilying negative 1
        scale.x *= -1;
        transform.localScale = scale;
        facingLeft = !facingLeft;
    }

    public void PlayerTakeDamage(float damage)
    {
        health.healthAmount -= damage;
        health.healthBar.fillAmount = health.healthAmount / 100;

    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();


        if (enemy != null)
        {
            PlayerTakeDamage(25);

        }
    }


}
