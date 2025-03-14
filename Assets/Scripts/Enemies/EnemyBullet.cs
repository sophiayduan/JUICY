using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed;
    private Transform player;
    private Vector3 target;
    public float amount = 10;
    // public float KOtime;
    // public int damage = 2;
    private PlayerHealth playerHealth;
    // private float variance;
    public  ParticleSystem particles;
    void Start()
    {
        // variance = UnityEngine.Random.Range(-0.25f, 0.25f);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector3(player.position.x, player.position.y + 1, player.position.z );

        if(player != null)
        {
            Debug.Log("Player != null (enemybullet)");
            playerHealth = player.GetComponent<PlayerHealth>();

            if(playerHealth != null){;
                Debug.Log("Playerhealth != null (enemybullet)");

                target = player.transform.position;
                transform.LookAt(target);
                Debug.Log("wow this is ok"); 
            }
            else {
                playerHealth = player.GetComponent<PlayerHealth>();
                Debug.LogError("playerhealth null (enemy bullet)");
            }

        }
        else 
        {
            Debug.LogError("player itself == null");
        }          
    }
    void Update()
    {        
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if(transform.position.x == target.x && transform.position.y == target.y){
            DestroyEnemyBullet();
            if(player.position.x <= 0.25+ transform.position.x && player.position.z <= 0.25 + transform.position.z && player.position.y <= 0.25 + transform.position.y ){
                Instantiate(particles,transform.position,Quaternion.identity);

            }
 
        }
    }

    void DestroyEnemyBullet(){
        Destroy(gameObject);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Ontriggerener tag is player");
            PlayerHealth playerHealth = other.GetComponentInParent<PlayerHealth>();

            if(playerHealth != null)
            {
                playerHealth.takeDamage(amount);
                Debug.Log("playerHealth took damage");

            }
            else 
            {
                Debug.LogError("playerhealth bottom = null");
            }
        }
    }

}


