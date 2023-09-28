using UnityEngine;
using UnityEngine.Tilemaps;


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

    public Tilemap tilemap;
    float finalPos;
    bool isAscend;
    public Rigidbody2D _rb;


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


                if (isAscend)
        {
            if (transform.position.y > finalPos)
            {
                isAscend = false;
                return;
            }

            _rb.velocity = new Vector2(_rb.velocity.x, 4);
            GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            GetComponent<BoxCollider2D>().enabled = true;
        }

        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, Vector2.up, 5);
        Debug.DrawLine(transform.position, (Vector2)transform.position + Vector2.up * 5, Color.red);

        foreach (RaycastHit2D h in hit)
        {
            if (h.collider.gameObject.tag == "platform")
            {
                if (Input.GetKeyDown(KeyCode.J))
                {
                    isAscend = true;
                    finalPos = h.collider.transform.position.y + 2.5f;

                }

                foreach (var position in tilemap.cellBounds.allPositionsWithin)
                {
                    if (!tilemap.HasTile(position))
                    {
                        continue;
                    }

                    if (Input.GetKeyDown(KeyCode.J))
                    {
                        isAscend = true;
                        finalPos = position.y + 2.5f;

                    }
                    Debug.Log(finalPos);
                }
            }
        }
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

    