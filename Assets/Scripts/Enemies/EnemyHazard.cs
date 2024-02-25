using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHazard : MonoBehaviour
{
    private int damege = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("He tocado al jugador");
            collision.SendMessage("HaveDamage", damege);
        }
    }
}
