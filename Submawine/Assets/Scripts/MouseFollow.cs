using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class MouseFollow : MonoBehaviour
{

    public Camera cam;
    private Vector2 mousePos;
    [SerializeField] private GameObject projPrefab;
    [SerializeField] private float bulletSpeed = 10f;
    public float angle;
    private Vector2 lookDir;
    private float lastTime;
    private GameObject lasthit;
    [SerializeField] private GameObject claw;
    private Claw clawCont;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        clawCont = claw.GetComponent<Claw>();
    }

    // Update is called once per frame


    void Update()
    {
        if (cam == null)
        {
            cam = Camera.main;
            if (cam == null)
            {
                Debug.LogError("Main Camera not found or tagged correctly!");
                return;
            }
        }
        Vector3 mouseScreenPosition = Mouse.current.position.ReadValue();
        mouseScreenPosition.z = cam.transform.position.z;
        mousePos = cam.ScreenToWorldPoint(mouseScreenPosition);
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Shoot();
        }
        if (Mouse.current.rightButton.wasPressedThisFrame && !clawCont.shot)
        {
            clawCont.ClawExtend();
        }
    }
    private void FixedUpdate()
    {
        lookDir = mousePos - (Vector2)transform.position;
        angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        clawCont.clawLookDir = mousePos - (Vector2)claw.transform.position;
        
    }

    void Shoot()
    {
        GameObject sonar = Instantiate(projPrefab, transform.position, Quaternion.Euler(0, 0, angle));
        Rigidbody2D rb = sonar.GetComponent<Rigidbody2D>();
        rb.linearVelocity = lookDir * bulletSpeed;
        SonarProjectile Sp = sonar.GetComponent<SonarProjectile>();
        Sp.setSpawner(this);
        Destroy(sonar, 3f);
    }
    public void HandleProjectileHit(GameObject hitObject, float time)
    {
        Debug.Log("Spawner notified: Projectile hit " + hitObject.name+ " in "+time+" seconds.");
        lasthit = hitObject;
        lastTime = time;
        StartCoroutine(WaitAndPlay(time));
    }
    IEnumerator WaitAndPlay(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        Debug.Log("Waited for " + waitTime + " seconds, and returned " + lasthit.name);
    }

}

