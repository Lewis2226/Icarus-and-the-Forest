using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : EnemyHazard
{
    [SerializeField] protected float velocidadMovimiento;
    [SerializeField] protected Transform[] puntosMoviminto;
    [SerializeField] private float distanciaMinima;
    public bool lookingRigth; 

    protected int siguientePunto;

   
    private void Start()
    {
        Girar();
    }

    private void Update()
    {
        Move();
       

    }
    public void Girar()
    {
        lookingRigth = !lookingRigth;
        float localScaleX = transform.localScale.x;
        localScaleX = localScaleX * -1f;
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.y);
    }

    public virtual void Move()
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
}
