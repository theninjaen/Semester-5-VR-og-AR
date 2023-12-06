using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public int damage;

    public float attackTimeResetValue;
    private float attackTimer;

    private void Update()
    {
        attackTimer -= Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy" && attackTimer <= 0)
        {
            other.GetComponent<EnemyAI>().health -= damage;
            Debug.DrawLine(other.transform.position, transform.position, Color.green);
            attackTimer = attackTimeResetValue;
        }
    }
}
