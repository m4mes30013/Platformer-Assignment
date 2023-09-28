using UnityEngine;
using UnityEngine.Tilemaps;

public class character : MonoBehaviour
{
    public Rigidbody2D _rb;
    public int moveSpeed;

    public int jumpSpeed;
    public Animator _anim;

    Vector2 myScale;
    float myScaleX;
    bool isJumping;

    public GameObject target;

    public bool isChat;
    bool isDie;

    public float fullhp;
    float currentHP;
    public GameObject HPBar;

    public GameObject HeartParent;
    public GameObject heart;

    public GameObject bullet;

    public float shootDelay;
    float shootTime;
    int direction;
    bool isAscend;

    public Tilemap tilemap;
    float finalPos;

    void Start()
    {
        direction = 1;

        myScale = transform.localScale;
        myScaleX = myScale.x;
        //--------------//
        currentHP = fullhp;
        //--------------//

        for (int i = 0; i < fullhp; i++)
        {
            Instantiate(heart, HeartParent.transform, false);
        }


    }

    public void DeductHP(int deductedHP)
    {
        if (currentHP <= 0)
        {
            currentHP = 0;
            isDie = true;
            return;
        }

        currentHP -= deductedHP;

        // health bar
        HPBar.transform.localScale = new Vector2(currentHP / fullhp, 1);

        // heart
        for (int i = 0; i < deductedHP; i++)
        {
            HeartParent.transform.GetChild((int)currentHP + i).gameObject.SetActive(false);
        }
    }

    void Update()
    {

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

        if (!isChat && !isDie)
        {
            //shoot
            shootTime += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (shootTime >= shootDelay)
                {
                    GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
                    newBullet.GetComponent<Bullet>().direction = direction;
                    shootTime = 0;
                }
            }

            _rb.velocity = new Vector2
               (Input.GetAxisRaw("Horizontal") * moveSpeed,
               _rb.velocity.y);

            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                myScale.x = -myScaleX;
                direction = -1;
            }
            else if (Input.GetAxisRaw("Horizontal") > 0)
            {
                myScale.x = myScaleX;
                direction = 1;
            }

            transform.localScale = myScale;

            if (Input.GetKeyDown(KeyCode.W) && isJumping == false)
            {
                isJumping = true;
                _rb.velocity = new Vector2
                (_rb.velocity.x, jumpSpeed);

                _anim.SetBool("isJump", true);

            }

            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                _anim.SetBool("isRun", true);
            }
            else
            {
                _anim.SetBool("isRun", false);
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (target != null && target.GetComponent<item>())
            {
                target.GetComponent<item>().DoSomething();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "platform")
        {
            _anim.SetBool("isJump", false);
            isJumping = false;
        }

        if (col.gameObject.name == "enemy")
        {
            DeductHP(2);
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {

    }

    // void OnTriggerEnter2D(Collider2D col)
    // {
    //     if (col.gameObject.name == "box")
    //     {
    //         col.gameObject.GetComponent<Animator>().SetBool("isOpen", true);
    //     }
    // }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name == "box")
        {
            col.gameObject.GetComponent<Animator>().SetBool("isOpen", false);
        }
    }



}
