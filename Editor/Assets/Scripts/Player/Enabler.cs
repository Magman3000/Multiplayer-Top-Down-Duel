using UnityEngine;
using Unity.Netcode;
using UnityEngine.InputSystem;

public class Enabler : NetworkBehaviour
{
	[SerializeField] private PlayerController player;
	[SerializeField] private PlayerInput input;
	[SerializeField] private ColorMatch match;
	private NetUI ui;
	private SceneControl sc;

	protected override void OnNetworkSessionSynchronized()
	{
		Debug.Log("Sync");
		if (!IsOwner) input.enabled = false;
		else input.enabled = true;
		//EScript(0);
    }
	public void EScript(int i)
	{
      switch (i)
      {
         case 0: 
			if (player.enabled) Debug.Log("Player Enabled");
			else Debug.Log("Player Not Enabled");
			Debug.Log("E0");
            //player.Activate();
            EScript(1);
            break;
         case 1:
			if (match.enabled) break;
			Debug.Log("E1");
            match.enabled = true;
            break;
         default:
            break;
      }
   }
}
