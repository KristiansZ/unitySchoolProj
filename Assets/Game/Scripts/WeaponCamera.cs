using UnityEngine;
public class WeaponCamera : MonoBehaviour
{
    public Camera mainCamera;
    public Camera weaponCamera;

    void LateUpdate()
    {
        weaponCamera.transform.position = mainCamera.transform.position;
        weaponCamera.transform.rotation = mainCamera.transform.rotation;
    }
}