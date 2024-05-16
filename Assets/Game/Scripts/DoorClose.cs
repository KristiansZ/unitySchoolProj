using UnityEngine;

public class DoorClose : MonoBehaviour
{
    [SerializeField] Transform doorTransform;
    Vector3 closedDoorRot = new Vector3(0,180,0);

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CloseDoor();
        }
    }

    void CloseDoor()
    {
        Vector3 rot = closedDoorRot;
        doorTransform.localRotation = Quaternion.Euler(rot);
    }
}