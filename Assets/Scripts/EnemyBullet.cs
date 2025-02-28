using Charactercontroller;
using UnityEditor;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed;
    private Transform player;
    private Vector3 target;
    public int damage = 2;
    private PlayerHealth playerHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player").transform;
        if(player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();

        // target = new Vector3(player.position.x, player.position.y, player.position.z);
            target = player.transform.position;
            transform.LookAt(target);
            Debug.Log("wow this is ok");

        }
        else
        {
            Debug.LogError("hey over here is brokenn");
        }
    }

    // Update is called once per frame
    void Update()
    {        
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);



    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (playerHealth != null)
            {
                playerHealth = player.GetComponent<PlayerHealth>();
                playerHealth.takeDamage(damage);
                Debug.Log($"Damage Amount: {damage}");
                Destroy(gameObject);
            }
            else
            {
              Debug.LogError("Player health broken");
 
        
            
            }
            Destroy(gameObject);
    }
    else 
    {
        Debug.LogError("oh shiiit");
    }

    }   
}

