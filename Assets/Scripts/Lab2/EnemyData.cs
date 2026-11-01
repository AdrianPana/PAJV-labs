using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Lab2/EnemyData")]
public class EnemyData : ScriptableObject
{
    public Enemy prefab;
    public float moveSpeed = 3f;
    public int maxHP = 1;
}
