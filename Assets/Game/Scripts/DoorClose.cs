using UnityEngine;

public class DoorClose : MonoBehaviour
{
    [SerializeField] Transform doorTransform;
    [SerializeField] GameObject player;
    Vector3 closedDoorRot = new Vector3(0,180,0);

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && HasGrandchildWithTag(other.transform, "Rifle"))
        {
            CloseDoor();
        }
    }

    bool HasGrandchildWithTag(Transform parent, string tag)
    {
        foreach (Transform child in parent)
        {
            foreach (Transform grandchild in child)
            {
                if (grandchild.CompareTag(tag))
                {
                    return true;
                }
            }
        }
        return false;
    }

    void CloseDoor()
    {
        Vector3 rot = closedDoorRot;
        doorTransform.localRotation = Quaternion.Euler(rot);
    }
}