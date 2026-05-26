using UnityEngine;
using Unity.Netcode;

public class NetAsset : NetworkBehaviour
{
    [SerializeField] private NetworkObject nObj;

    public void Init()
    {
        Debug.Log("Init");
        nObj.Spawn();
		OnInit();
    }

	public virtual void OnInit(){}
	
	[Rpc(SendTo.Server, InvokePermission = RpcInvokePermission.Everyone)]
	public void Destroy_ServerRpc()
	{
		Destroy(gameObject);
	}
}
