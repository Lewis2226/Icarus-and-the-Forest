using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : EnemyHazard
{
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private Transform[] puntosMoviminto;
    [SerializeField] private float distanciaMinima;
    public bool lookingRigth;

    private int siguientePunto;
    public Transform playtransform;
    public bool isChasing;
    public float chaseDistance;

    private void Start()
    {
        Girar();
    }
    private void Update()
    {
        if (isChasing)
        {
            if (transform.position.x > playtransform.position.x)
            {
                transform.position += Vector3.left * velocidadMovimiento * Time.deltaTime;
                
            }

            if (transform.position.x < playtransform.position.x)
            {
                transform.position += Vector3.right * velocidadMovimiento * Time.deltaTime;
            }
        }
        else
        {
            if (Vector2.Distance(transform.position, playtransform.position) < chaseDistance)
            {
                isChasing = true;
            }
            else
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
    }

    private void Girar()
    {
        lookingRigth = !lookingRigth;
        float localScaleX = transform.localScale.x;
        localScaleX = localScaleX * -1f;
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.y);
    }
   
}
