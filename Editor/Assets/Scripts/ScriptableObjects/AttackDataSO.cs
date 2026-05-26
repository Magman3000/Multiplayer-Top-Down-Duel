using UnityEngine;

[CreateAssetMenu(fileName = "AttackDataSO", menuName = "Scriptable Objects/AttackDataSO")]
public class AttackDataSO : ScriptableObject
{
    [Tooltip("[float] Duration of attack")]
    public float duration;
    [Tooltip("[float] Time before attack can deal damage")]
    public float startup;
    [Tooltip("[float] Duration of the action")]
    public float cooldown;
    [Tooltip("[float] Speed of the Attack")]
    public float speed;
	[Tooltip("[float] Damage of the Attack")]
    public float damage;
    [Tooltip("[GameObject] Object to spawn")]
    public GameObject obj;
}
