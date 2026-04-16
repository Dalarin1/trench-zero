
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("AI Settings")]
    public float MoveSpeed = 2f;
    public int Health = 100;
    public int Damage = 10;

    private int waveIndex;
    private Transform target;

    void Start()
    {
        // Найти ближайшего бойца
        target = FindNearestSoldier();
    }

    void Update()
    {
        if (target != null)
        {
            MoveTowardsTarget();
        }
    }

    void SetWaveIndex(int index)
    {
        waveIndex = index;
        gameObject.tag = $"Wave{index}";
    }

    Transform FindNearestSoldier()
    {
        GameObject[] soldiers = GameObject.FindGameObjectsWithTag("Soldier");
        Transform nearest = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject soldier in soldiers)
        {
            float distance = Vector3.Distance(transform.position, soldier.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = soldier.transform;
            }
        }

        return nearest;
    }

    void MoveTowardsTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, MoveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) < 1.5f)
        {
            Attack();
        }
    }

    void Attack()
    {
        Soldier soldier = target.GetComponent<Soldier>();
        if (soldier != null)
        {
            soldier.TakeDamage(Damage);
        }
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
