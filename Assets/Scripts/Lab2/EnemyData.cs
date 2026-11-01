using UnityEngine;
using Lab2;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Lab2/EnemyData")]

public class EnemyData : ScriptableObject
{
    public Enemy prefab;
    public float moveSpeed = 3f;
    public int maxHP = 1;
    public float attackCooldown = 1f;
}
