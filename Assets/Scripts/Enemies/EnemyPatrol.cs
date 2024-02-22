using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : EnemyHazard
{
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private Transform[] puntosMoviminto;
    [SerializeField] private float distanciaMinima;

    private int siguientePunto;

    private SpriteRenderer spriteRenderer;
   



    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Girar();
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, puntosMoviminto[siguientePunto].position, velocidadMovimiento * Time.deltaTime);
        if (Vector2.Distance(transform.position, puntosMoviminto[siguientePunto].position) < distanciaMinima)
        {
            siguientePunto += 1;
            if (siguientePunto >= puntosMoviminto.Length)
            {
                siguientePunto = 0;
            }
            Girar();
        }

    }
    private void Girar()
    {
        if (transform.position.x < puntosMoviminto[siguientePunto].position.x)
        { 
            spriteRenderer.flipX = true;

        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
}
