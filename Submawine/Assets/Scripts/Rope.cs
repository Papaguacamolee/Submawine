using UnityEngine;

public class Rope : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject submarine;
    [SerializeField]public GameObject defaultClaw;
    public GameObject claw;
    public float boxWidth;
    
    void Start()
    {
        claw = defaultClaw;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = (submarine.transform.position + claw.transform.position) / 2f;

        Vector3 direction = claw.transform.position - submarine.transform.position;
        float distance = direction.magnitude;
        transform.localScale = new Vector3(distance, boxWidth, 1f);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        if (claw == null)
        {
            claw = defaultClaw;
        }
    }
    public void setClaw(GameObject newClaw)
    {
        this.claw = newClaw;
    }

    public void resetRope()
    {
        this.claw = defaultClaw;
    }
}
