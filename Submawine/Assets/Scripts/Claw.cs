using UnityEngine;

public class Claw : MonoBehaviour
{
    [SerializeField] private GameObject clawPrefab;
    public float clawSpeed;
    public Vector2 clawLookDir;
    private float angle;
    public bool shot = false;
    //private Transform submarine;
    //private bool returning = false;
    private GameObject grabbed = null;
    private GameObject claw = null;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {   
       
  
    }

    private void FixedUpdate()
    {
        if (!shot)
        {
            angle = Mathf.Atan2(clawLookDir.y, clawLookDir.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
        
    }

    public void ClawExtend()
    {
        claw = Instantiate(clawPrefab, transform.position, Quaternion.Euler(0, 0, angle));
        Rigidbody2D rb = claw.GetComponent<Rigidbody2D>();
        rb.linearVelocity = clawLookDir * clawSpeed;
        clawProjectile Sp = claw.GetComponent<clawProjectile>();
        Sp.setSpawner(this);
        GetComponent<SpriteRenderer>().enabled = false;
        shot = true;
    }

    public void HandleClawHit(GameObject hitObject)
    {
        Debug.Log("HandlingClawHit");
        if (hitObject != null) grabbed = hitObject; Debug.Log("Returned " + grabbed); Destroy(hitObject, 0);
        shot = false;
        GetComponent<SpriteRenderer>().enabled = true;
        Destroy(claw, 0f);
    }
}
