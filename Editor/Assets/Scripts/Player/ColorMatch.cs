using UnityEngine;
using Unity.Netcode;


public class ColorMatch : NetworkBehaviour
{
    
    void OnEnable()
    {
        if (IsOwner)
            GetComponent<Renderer>().material.color =
                transform.parent.gameObject.GetComponent<Renderer>().material.color;
    }

   
}
