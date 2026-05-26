using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using TMPro;

public class Chat : MonoBehaviour
{
    [SerializeField] private GameObject textObject, nameField, textField;
    [SerializeField] private Transform chatPannel;
    [SerializeField] private NetUI netUI;
    [SerializeField] private TMP_InputField nameInput, textInput;
    private string playerName;
	private int id;
    
    public void AddEntry()
    {
        if (textInput.text == "") return;
		netUI.UpdateChat_ServerRpc(id, textInput.text);
        textInput.text = "";
    }

    public void Joined()
    {
        nameField.SetActive(false);
        textField.SetActive(true);
    }
	public string GetName(int i)
	{
		id = i;
		return nameInput.text;
	}
}
