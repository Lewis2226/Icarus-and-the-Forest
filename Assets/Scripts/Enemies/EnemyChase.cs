using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : EnemyPatrol
{
    public Transform playertransform;
    public bool playerSeen;
    public int playerSeenDistance;
   
    public override void Move()
    {
        if (Vector2.Distance(transform.position, playertransform.position) < playerSeenDistance)
        {
            playerSeen = true;
            Debug.Log("He visto al jugador");
            if (playertransform.position.x  > transform.position.x)
            {
                transform.localScale= new Vector3 (1f, 1f, 1);
                lookingRigth = true;
            }
            else
            {
                transform.localScale = new Vector3(-1f, 1f, 1);
                lookingRigth = false;
            }
        } 
        else
        {
            playerSeen = false;
            
        }

        if (playerSeen)
        {
            
            transform.position = Vector2.MoveTowards(transform.position, playertransform.position, velocidadMovimiento * Time.deltaTime);
        }
        else
        {  if(puntosMoviminto[siguientePunto].position.x > transform.position.x)
            {
                transform.localScale = new Vector3(1f, 1f, 1);
                lookingRigth =true;
            }
            else
            {
                transform.localScale = new Vector3(-1f, 1f, 1);
                lookingRigth = false;
            }
            base.Move();
        }
        
    }

}
