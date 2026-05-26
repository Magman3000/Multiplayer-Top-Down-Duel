using UnityEngine;
using Unity.Netcode;
using System.Collections;

public class Hitbox : NetAsset
{
    [SerializeField] private AttackDataSO data;
    [SerializeField] public Weapon weapon;
    private GameObject player;
	private int playerId;
    private bool active = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void Setup(GameObject obj, int i, float startup)
    {
        player = obj;
        playerId = i;
        if (IsOwner)
            GetComponent<Renderer>().material.color = Color.green;
        else GetComponent<Renderer>().material.color = Color.red;
        IEnumerator coroutine = Startup(startup);
        StartCoroutine(coroutine);
    }

    IEnumerator Startup(float f)
    {
        yield return new WaitForSeconds(f);
        active = true;
    }
    public void OnTriggerEnter(Collider other)
    {
		if(!active)  return;
        
        if (other.CompareTag("Player"))
        {
			if (other.gameObject.GetComponent<Player>().id.Value == playerId) return;
            other.gameObject.GetComponentInChildren<Health>().Damage_ServerRpc(data.damage, playerId);
			Destroy_ServerRpc();
        }
    }

}

