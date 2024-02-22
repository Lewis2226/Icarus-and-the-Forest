using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int totalhp = 1;
    private int hp;
    private SpriteRenderer renderer;
    
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        hp = totalhp; 
    }

    public void HaveDamage(int damage)
    {
        hp -= damage;

        //Efecto visual
        StartCoroutine("EfectoVisual");
        //¿Has muerto?
        if (hp <= 0)
        {
            hp = 0;
            Debug.Log("Te has muerto");
        }

    }

    private IEnumerator EfectoVisual()
    {
        renderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        renderer.color = Color.white;
    }
}
