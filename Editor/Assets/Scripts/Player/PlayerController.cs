using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;


public class PlayerController : NetAsset
{
    [SerializeField] private float speed;
    [SerializeField] private Transform body;
    [SerializeField] private AttackDataSO bullet;
	[SerializeField] private PlayerInput input;
	[SerializeField] private Rigidbody rb;
	[SerializeField] private Renderer renderer, canonRend;
	[SerializeField] private Player player;
	[SerializeField] private GameObject playerParent;
    public Vector2 aim;
    private Vector2 move;
	private NetworkVariable<float> cooldown = new NetworkVariable<float>();
	private GameObject sc;
	private bool active = false;
	private Chat chat;
	private List<Vector2> vs = new List<Vector2>{new Vector2(0,1), new Vector2(-1,1), new Vector2(-1,0), new Vector2(-1,-1), new Vector2(0,-1), new Vector2(1,-1), new Vector2(1,0), new Vector2(1,1)};
	private List<float> fs = new List<float>{0, 45, 90, 135, 180, 225, 270, 315};
    // Start is called once before the first execution of Update after the MonoBehaviour is created

	public override void OnNetworkSpawn()
	{
		if(IsOwner)
		{
			renderer.material.color = Color.green;
			canonRend.material.color = Color.green;
		}
		else
		{
			renderer.material.color = Color.red;
			canonRend.material.color = Color.red;
		}
	}

	protected override void OnNetworkSessionSynchronized()
	{
		Debug.Log("Sync: " + IsOwner + player.id.Value + transform.position);
		input.enabled = IsOwner;
		if(!IsOwner || active) return;
		sc = GameObject.Find("SceneControl");
		sc.GetComponent<SceneControl>().AddPlayer(player);
        input.onActionTriggered += InputTrigger;
		active = true;
		chat =  sc.GetComponent<Chat>();
	}
	

    void OnDisable()
    {
		active = false;
		if (!IsOwner) return;
        input.onActionTriggered -= InputTrigger;
        sc.GetComponent<CameraPosition>().RemovePlayer(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
		if(IsServer && cooldown.Value > 0) cooldown.Value -= Time.deltaTime;
		if (!IsOwner||!active) return;
        Vector3 temp = transform.position;
        temp.x += move.x * Time.deltaTime * speed;
        temp.y += move.y * Time.deltaTime * speed;
		//Debug.Log("Move: " + temp);
		rb.MovePosition(temp);

		int i;
		if(aim != Vector2.zero) i = vs.IndexOf(aim);
		else i = 0;
		body.rotation = new Quaternion(0,0,0,0);
		body.RotateAround(body.position, body.forward, fs[i]);
		
    }

    void InputTrigger(InputAction.CallbackContext context)
    {
		if (!IsOwner) return;
        string action = context.action.name;
        switch (action)
        {
            case("Move"):
                Move(context.ReadValue<Vector2>());
                break;
            case("Look"):
                Aim(context.ReadValue<Vector2>());
                break;
            case("Attack"):
                Range();
                break;
            case("Talk"):
				chat.AddEntry();
                break;
			case("Menu"):
				break;
            default:
                break;
        }
    }
    public void Move(Vector2 direction)
    {
		move = direction;
    }
    
    public void Aim(Vector2 direction)
    {
        if(direction.x > 0) aim.x = 1;
		else if(direction.x < 0) aim.x = -1;
		else aim.x = 0;

		if(direction.y > 0) aim.y = 1;
		else if(direction.y < 0) aim.y = -1;
		else aim.y = 0;
    }

    public void Range()
    {
        Attack_ServerRpc();
    }
	[Rpc(SendTo.Server, InvokePermission = RpcInvokePermission.Everyone)]
	public void Attack_ServerRpc()
    {
        if(cooldown.Value > 0) return;
		cooldown.Value = bullet.cooldown;
		GameObject temp = Instantiate(bullet.obj, body.position, body.rotation);
		Hitbox hit = temp.GetComponent<Hitbox>();
		hit.Setup(playerParent, player.id.Value, bullet.startup);
		hit.weapon.initalize(bullet.duration, bullet.speed);
		temp.GetComponent<NetworkObject>().Spawn();
		if (!IsOwner) temp.GetComponent<Renderer>().material.color = Color.red;
		else temp.GetComponent<Renderer>().material.color = Color.green;
    }
}
