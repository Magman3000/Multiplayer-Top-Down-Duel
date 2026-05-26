using UnityEngine;

public class Weapon : MonoBehaviour
{
    public bool b_active = false;
    public float speed;
    public void initalize(float time, float spd)
    {
        b_active = true;
        speed = spd;
        Destroy(gameObject, time);
    }
}
