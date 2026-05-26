using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System;

public class NetUI : NetworkBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GameObject chatPrefab;
    [SerializeField] private Transform chatPannel;
    private List<string> names = new List<string>();
	private List<int> scores = new List<int>();

	[Rpc(SendTo.Server, InvokePermission = RpcInvokePermission.Everyone)]
	public void AddName_ServerRpc(string name)
	{
		for (int i = 0; i < names.Count; i++)
		{
			NewScoreBoard_ClientRpc(names[i], i, scores[i]);
		}
		AddName_ClientRpc(name);
	}
	[Rpc(SendTo.ClientsAndHost)]
	private void AddName_ClientRpc(string name)
	{
		Debug.Log(name);
		scores.Add(0);
		names.Add(name);
		UpdateScoreBoard();
	}
	[Rpc(SendTo.ClientsAndHost)]
	private void NewScoreBoard_ClientRpc(string name, int i, int score)
	{
		if (i < scores.Count) return;
		names.Add(name);
		scores.Add(score);
	}
	[Rpc(SendTo.Server, InvokePermission = RpcInvokePermission.Everyone)]
	public void UpdateChat_ServerRpc(int i, string message)
	{
		UpdateChat_ClientRpc(i, message);
	}
	[Rpc(SendTo.Everyone)]
	private void UpdateChat_ClientRpc(int i, string message)
	{
		UpdateChat(i, message);
	}
	[Rpc(SendTo.ClientsAndHost)]
	public void UpdateScore_ClientRpc(int i, int score)
	{
		Debug.Log("UpdateScore[" + i + "]: " + score); 
		scores[i] = score;
		UpdateScoreBoard();
	}

    private void UpdateScoreBoard()
    {
        string[] text = new string[scores.Count];
        for (int i = 0; i < scores.Count; i++)
        {
            text[i] = names[i] + ": " + scores[i];
        }

        scoreText.text = string.Join("\n", text);
    }

    private void UpdateChat(int i, string message)
    {
        GameObject temp = Instantiate(chatPrefab, chatPannel);
        temp.GetComponent<TMP_Text>().text = names[i] + ": " + message;
    }
    
}
