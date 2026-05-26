using UnityEngine;
using Unity.Netcode;
using System.Collections.Generic;
using System;
using TMPro;

public class SceneControl : NetAsset
{
    [SerializeField] public NetUI netUI;
    [SerializeField] private Chat chat;
	private int id;
	public NetworkVariable<List<int>> playerScores = new NetworkVariable<List<int>>();
	public NetworkVariable<bool> active = new NetworkVariable<bool>(false);

	public void Death(int attacker)
	{
        ChangeScore_ServerRpc(playerScores.Value[attacker] +1, attacker);
	}

	public void AddPlayer(Player player)
	{
		Debug.Log("AddPlayer");
		AddPlayer_ServerRpc(player, chat.GetName(playerScores.Value.Count));
	}
	
	[Rpc(SendTo.Server, InvokePermission = RpcInvokePermission.Everyone)]
	public void AddPlayer_ServerRpc(NetworkBehaviourReference playRef, string name)
	{
		Debug.Log("Add");
		/*Debug.Log("Add");
		if(!active.Value)
		{
			Debug.Log("Active");
			active.Value = true;
		}*/
		if (playRef.TryGet(out Player player))
        {
	        Debug.Log("Player");
			player.FinishSetup_ServerRpc(playerScores.Value.Count);
        }
		Debug.Log("UI");
		netUI.AddName_ServerRpc(name);
		Debug.Log("Net");
	    playerScores.Value.Add(0);
	}
	[Rpc(SendTo.Server, InvokePermission = RpcInvokePermission.Everyone)]
    public void ChangeScore_ServerRpc(int score, int index)
    {
	    playerScores.Value[index] = score;
	    netUI.UpdateScore_ClientRpc(index, score);
    }
    
}
