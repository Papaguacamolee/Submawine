using UnityEngine;

public class SonarProjectile : MonoBehaviour
{
    private MouseFollow spawner;
    public float time = 0f;
    private bool isCounting = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isCounting = true;
    }

    public void setSpawner(MouseFollow source)
    {
        this.spawner = source;
    }
    // Update is called once per frame
    void Update()
    {
        if (isCounting)
        {
            time += Time.deltaTime;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Submarine")&& isCounting)
        {
            isCounting = false;
            spawner.HandleProjectileHit(collision.gameObject, time);
        }
        
    }
}
