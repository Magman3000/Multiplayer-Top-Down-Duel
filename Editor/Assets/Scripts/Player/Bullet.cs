using UnityEngine;

public class Bullet : Weapon
{
	[SerializeField] Rigidbody rb;
    // Update is called once per frame
    void Update()
    {
        if(!b_active) return;
        Vector3 v = transform.up;
        v = Quaternion.AngleAxis(transform.rotation.z, transform.forward) * v;
        rb.AddForce( v * speed * Time.deltaTime);
    }
}
