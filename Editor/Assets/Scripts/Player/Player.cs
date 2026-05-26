using UnityEngine;
using Unity.Netcode;

public class Player : NetAsset
{
   [SerializeField] private GameObject body;
   public NetworkVariable<int> id = new NetworkVariable<int>();
   public override void OnInit()
   {
      GameObject.Find("SceneControl").GetComponent<CameraPosition>().AddPlayer(gameObject);
   }
   [Rpc(SendTo.Server, InvokePermission = RpcInvokePermission.Everyone)]
   public void FinishSetup_ServerRpc(int i)
   {
      id.Value = i;
      Debug.Log("Setup: " + gameObject);
      GameObject.Find("SceneControl").GetComponent<CameraPosition>().AddPlayer(gameObject);
   }
}
