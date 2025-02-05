using UnityEngine;

public class Gun1 : MonoBehaviour
{
    public float maxspeed;
    public float speed;
    public GameObject objectToSpawn;
    public Transform player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instantiate(objectToSpawn, Vector3.zero, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
   
    }
}
