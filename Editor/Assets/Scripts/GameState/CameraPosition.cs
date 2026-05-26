using UnityEngine;
using System.Collections.Generic;
using System;
using Unity.Netcode;

public class CameraPosition : NetworkBehaviour
{
    [SerializeField] private float cameraScaleRate;
    [SerializeField] private float verticalScaleModifier;
	[SerializeField] private float minimumCameraSize;
    private Camera camera;
    private List<GameObject> players = new List<GameObject>();
	public NetworkVariable<float> fX,fY,fV = new NetworkVariable<float>();
    void Start()
    {
        camera = GetComponent<Camera>();
    }
    public void ClearPlayerList()
    {
        players.Clear(); 
		Debug.Log("Clear");
    }
    public void RemovePlayer(GameObject player)
    {
        players.Remove(player);
		Debug.Log("Remove: " + player);
    }
    public void AddPlayer(GameObject player)
    {
        players.Add(player);
		Debug.Log("Add: " + player);
    }
    
    void Update()
    {
		if(IsHost)
		{
        	Vector3 temp = new Vector3(0, 0, -10);

        	foreach (GameObject obj in players)
        	{
            	temp += obj.transform.position;
        	}

        	if (players.Count > 0)
        	{
            	temp.x = temp.x / players.Count;
            	temp.y = temp.y / players.Count;   
        	}

			fX.Value = temp.x;
			fY.Value = temp.y;
        	float distance = 1;
        
        	for (int i = 0; i < players.Count -1; i++)
        	{
            	float fl = Vector3.Distance(players[i].transform.position, players[i + 1].transform.position);
            	if (fl > distance)
            	{
                	if (Math.Abs(players[i].transform.position.x - players[i + 1].transform.position.x) >
                    	Math.Abs(players[i].transform.position.y - players[i + 1].transform.position.y))
                	{
                    	distance = fl;
                	}
                	else
                	{
                    	distance = Camera.VerticalToHorizontalFieldOfView(fl, camera.aspect) / verticalScaleModifier;
                	}
                
            	}
        	}
        
			float f = cameraScaleRate * distance;
			if(f > minimumCameraSize) fV.Value = f;
			else fV.Value = minimumCameraSize;
		}
		camera.fieldOfView = fV.Value;
		transform.position = new Vector3(fX.Value, fY.Value, -10);
    }
}
