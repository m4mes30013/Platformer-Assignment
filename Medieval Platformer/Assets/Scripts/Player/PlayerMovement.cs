using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;
    public int moveSpeed;
    public int jumpSpeed;
    public Animator _anim;
    Vector2 myScale;
    float myScaleX;
    bool isJumping;
    public GameObject target;

    private Vector3 currentVelocity = Vector3.zero;
    [SerializeField] private float smoothTime = 0.1f;

    private bool jumpButtonReleased = true;

    private void Awake() 
    {
        body = GetComponent<Rigidbody2D>();    
        myScale = transform.localScale;
        myScaleX = myScale.x;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            if(target !=null && target.GetComponent<Item>()){
                target.GetComponent<Item>().ASD();
            }  
        }
        
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        Vector3 targetVelocity = new Vector3(horizontalInput * speed, body.velocity.y);
        body.velocity = Vector3.SmoothDamp(body.velocity, targetVelocity, ref currentVelocity, smoothTime);

        //flip player while moving left-right
        if (Input.GetAxisRaw("Horizontal") < 0)
            {
                myScale.x = -myScaleX;
            }
        else if (Input.GetAxisRaw("Horizontal") > 0)
            {
                myScale.x = myScaleX;  
            }

            transform.localScale = myScale;

        if(Input.GetKey(KeyCode.Space) && isJumping == false && jumpButtonReleased)
            {
            isJumping = true;
            jumpButtonReleased = false;
            body.velocity = new Vector2(body.velocity.x, speed);
            _anim.SetBool("isJump", true);
            _anim.SetTrigger("isJumpTrigger");
            }

        if(!Input.GetKey(KeyCode.Space))
        {
            jumpButtonReleased = true;
        }

        _anim.SetBool("isRun", Input.GetAxisRaw("Horizontal") != 0);
    }

    void OnCollisionEnter2D(Collision2D col) 
    {
        if (col.gameObject.tag == "Ground")
        {
            _anim.SetBool("isJump", false);
            isJumping = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D col) 
    {
        if (col.gameObject.name == "Item")
        {
            col.gameObject.GetComponent<Animator>().SetBool("is open",true);
        }    
    }
}
