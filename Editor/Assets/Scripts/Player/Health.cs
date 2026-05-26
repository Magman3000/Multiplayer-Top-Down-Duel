using UnityEngine;
using Unity.Netcode;
using System.Collections.Generic;
using System;
using TMPro;

public class Health : NetworkBehaviour
{
    [SerializeField] private float maxHP;
    [SerializeField] private GameObject healthBar;
    private SceneControl sceneControl;
    public NetworkVariable<float> HP = new NetworkVariable<float>();
	

    void Start()
    {
        sceneControl = GameObject.Find("SceneControl").GetComponent<SceneControl>();
		Start_ServerRpc();
    }

	[Rpc(SendTo.Server, InvokePermission = RpcInvokePermission.Everyone)]
    public void Start_ServerRpc()
	{
        HP.Value = maxHP;
	}

	
	[Rpc(SendTo.ClientsAndHost, InvokePermission = RpcInvokePermission.Server)]
	public void UpdateHealthBar_ClientRpc()
	{
		Vector3 temp = healthBar.transform.localScale;
        temp.x = HP.Value/maxHP;
        healthBar.transform.localScale = temp;
        if (temp.x > 0.5) healthBar.GetComponent<Renderer>().material.color = Color.green;
        else if (temp.x < 0.5 && temp.x > 0.25) healthBar.GetComponent<Renderer>().material.color = Color.yellow;
        else healthBar.GetComponent<Renderer>().material.color = Color.red;
	}

	[Rpc(SendTo.Server, InvokePermission = RpcInvokePermission.Everyone)]
    public void Damage_ServerRpc(float damage, int attacker)
    {
        HP.Value -= damage;
        if (HP.Value <= 0) 
		{
			Debug.Log("Killed by Player: " + attacker);
			sceneControl.Death(attacker);
			HP.Value = maxHP;
		}
        UpdateHealthBar_ClientRpc();
    }

    
    
}
