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
    
}
