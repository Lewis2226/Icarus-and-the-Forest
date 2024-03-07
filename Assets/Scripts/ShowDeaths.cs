using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowDeaths : MonoBehaviour
{
    public TextMeshProUGUI textDeaths;

    public void End()
    {
        textDeaths.text = TotalDeaths.totalDeaths.ToString();
    }

    public void ResetDeaths()
    {
        TotalDeaths.totalDeaths = 0;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            End();
        }
    }

}
