using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D _rb;
    public int moveSpeed;
    public int direction;

    void Start()
    {
        Destroy(gameObject, 3);
    }

    void Update()
    {
        _rb.velocity = new Vector2(direction * moveSpeed, _rb.velocity.y);
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "enemy")
        {
            //enemy die
            Destroy(col.gameObject);
            Destroy(gameObject);
            //explosion
        }

        if (col.gameObject.tag == "box")
        {
            //box explode
        }
    }

}
