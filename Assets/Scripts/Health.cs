using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int totalhp = 1;
    private int hp;
    private SpriteRenderer _renderer;
    
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
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
        _renderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        _renderer.color = Color.white;
    }
}
