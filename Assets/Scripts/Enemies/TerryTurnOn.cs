using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerryTurnOn : MonoBehaviour
{
    public GameObject Terry;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Terry.SetActive(true);
        }
    }

    public void Off()
    {
        Terry.SetActive(false);
    }
}
