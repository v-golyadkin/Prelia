using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Configs/PlayerConfigs/PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    [Header("Player Stats")]
    public int maxHealth = 4;


    [Header("Movement")]
    public float moveSpeed = 1f;
    public float dashSpeed = 4f;


}
