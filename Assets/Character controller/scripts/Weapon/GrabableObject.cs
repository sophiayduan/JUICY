using UnityEngine;

public class GrabableObject : MonoBehaviour
{
    private Rigidbody objectRigidbody;
    private Transform objectGrabPointTransform;

    private void Awake()
    {
        objectRigidbody = GetComponent<Rigidbody>();
    }
    public void Grab(Transform objectGrabPointTransform){
        this.objectGrabPointTransform = objectGrabPointTransform;
        objectRigidbody.useGravity = false;
    }
    private void FixedUpdate()
    {
        if(objectGrabPointTransform != null){
            objectRigidbody.MovePosition(objectGrabPointTransform.position);
        }
    }


}
