using UnityEngine;
using System.Collections;

public class PlayerMoveScript : MonoBehaviour {

    public float movementSpeed;
    public float jumpSpeed;
    private Rigidbody2D rb;
    private Vector2 currentSpeed;

    public GameObject groundCheck;
    private bool isGrounded = false;

    private float dir =1;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        isGrounded = Physics2D.Linecast(transform.position, groundCheck.transform.position, 1 << LayerMask.NameToLayer("Ground"));
       
        if (Input.GetButtonDown("Jump") && isGrounded) {
            currentSpeed = rb.velocity;
            rb.AddForce(new Vector2(0,jumpSpeed),ForceMode2D.Impulse);
        }

        if (Input.GetAxisRaw("Horizontal") != 0) {
            //if (isGrounded)
            //{

            if (dir != Mathf.Sign(Input.GetAxisRaw("Horizontal"))){
                GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
            }

            dir = Mathf.Sign(Input.GetAxisRaw("Horizontal"));
            currentSpeed = rb.velocity;
            rb.AddForce(new Vector2(Input.GetAxisRaw("Horizontal") * movementSpeed,0));

            
            //}
            //else {
            //    currentSpeed = rb.velocity;
            //    rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * movementSpeed/2, currentSpeed.y);
            //}
        }

	}


}
