using UnityEngine;

public class clawProjectile : MonoBehaviour
{
    private Claw spawner;
    private bool grabbed;
    public GameObject target = null;
    private float returnSpeed = 7;
    private bool returning = false;
    public Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void setSpawner(Claw source)
    {
        spawner = source;
    }
    // Update is called once per frame
    void Update()
    {
        if (returning)
        {
            returnToSub();
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Submarine") && returning)
        {
            returning = true;
            Debug.Log("Log2: returning and collided with Submarine");
            spawner.HandleClawHit(target); 
        } 
        else if (collision.gameObject.CompareTag("Grabbable")){
            target = collision.gameObject;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            collision.gameObject.transform.SetParent(transform);
            collision.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            returning = true;
            Debug.Log("Grabbed" +collision.gameObject.name);
        }else if (collision.gameObject.CompareTag("Ground"))
        {
            returning = true;
            Debug.Log("Log #1: hit Ground, returning");
        }
        
    }
    void returnToSub()
    {
        rb.constraints = RigidbodyConstraints2D.None;
        Vector2 parentPos = spawner.gameObject.transform.parent.position;
        Vector2 returnDir = parentPos - (Vector2)transform.position;
        rb.linearVelocity = returnDir * returnSpeed;
        Debug.Log("returning");

    }
}
