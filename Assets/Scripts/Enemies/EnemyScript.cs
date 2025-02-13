using Unity.VisualScripting;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float Maxspeed;
    private float Speed;
    private Collider[] hitColliders;
    private RaycastHit Hit;
    public float SightRange;
    public float DetectionRange;

    public Rigidbody rb;
    public GameObject Target;
    private bool seePlayer;

    public float AttackDamage;

    public float Cooldown;
    public float AttackRange;
    public float LastAttack;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Speed = Maxspeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!seePlayer)
        {
            hitColliders = Physics.OverlapSphere(transform.position, DetectionRange);
            foreach (var HitCollider in hitColliders)
            {
                if(HitCollider.tag == "Player")
                {
                    Target = HitCollider.gameObject;
                    seePlayer = true;
                }
            }
        }
        else
        {
            if(Physics.Raycast(transform.position, (Target.transform.position -transform.position), out Hit, SightRange))
            {
                if(Hit.collider.tag != "Player")
                {
                    seePlayer = false;
                }
                else{

                    var Heading = Hit.transform.position - transform.position;
                    var Distance = Heading.magnitude;
                    var Direction = Heading / Distance;

                    Vector3 Move = new Vector3(Direction.x *Speed, 0, Direction.z * Speed);
                    rb.linearVelocity = Move;
                    transform.forward = Move;

                    if(Distance <= AttackRange && Time.time >= LastAttack + Cooldown){ 
//hi sia, and me, basically it only attacks if its in range AND the total time the game has been running is more than the timestamp of its last attack + the cooldown for the attack
                        // Attack(); // using attack
                        LastAttack = Time.time; //reset cooldown basically

        
                    }
                    else {
                        return;
                    
                }
            }

        }
        
    }
    }
        void Attack()
    {

    }
}

