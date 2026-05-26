using UnityEngine;
using Unity.Netcode;

public class Buttons : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Chat chat;
    [SerializeField] private GameObject chatbox, quit;
   
    public void Host()
    {
		NetworkManager.Singleton.StartHost();
		gameObject.SetActive(false);
		quit.SetActive(true);
		chatbox.SetActive(true);
		chat.Joined();
	}
	public void Server()
	{
		NetworkManager.Singleton.StartServer();
		chatbox.SetActive(true);
		quit.SetActive(true);
		gameObject.SetActive(false);
	}
	public void Client()
	{
		NetworkManager.Singleton.StartClient();
		gameObject.SetActive(false);
		quit.SetActive(true);
		chatbox.SetActive(true);
		chat.Joined();
	}
	public void Quit()
	{
		Application.Quit();
	}
}
