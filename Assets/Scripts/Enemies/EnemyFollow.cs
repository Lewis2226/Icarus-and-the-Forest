using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : EnemyHazard
{
    public float velocidadMovimiento;
    public Transform playertransform;
    public Transform posicioninicial;
   
    


    private void Start()
    {
        
        transform.position = posicioninicial.position;
    }

    public void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, playertransform.position, velocidadMovimiento * Time.deltaTime);
        if (playertransform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(1f, 1f, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1f, 1f, 1);
        }
    }

    
}
