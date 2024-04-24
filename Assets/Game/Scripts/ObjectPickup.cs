using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPickup : MonoBehaviour
{
    [SerializeField] public GameObject objectToPickup; // Reference to the object to pick up
    public KeyCode pickupKey = KeyCode.E; // Key to press to pick up the object
    [SerializeField] private Transform handTransform;

   private void Update()
    {
        if (Input.GetKeyDown(pickupKey))
        {
            Pickup();
        }
    }

    private void Pickup()
    {
        // Find the custom CharacterController script attached to the player GameObject
        KinematicCharacterController.Examples.CharacterController characterController = FindObjectOfType<KinematicCharacterController.Examples.CharacterController>();
        
        // Ensure the CharacterController script is found
        if (characterController != null && objectToPickup != null && handTransform != null)
        {
            // Parent the object to the player's hand
            objectToPickup.transform.SetParent(handTransform);
            
            // Reset local position and rotation to align correctly with the hand
            objectToPickup.transform.localPosition = new Vector3(0.35f, -0.5f, 0.2f);
            objectToPickup.transform.localRotation = Quaternion.identity;

            // Optionally adjust position and rotation if needed
            objectToPickup.transform.localEulerAngles = new Vector3(0, 180, 0); // Adjust these values based on your model orientation

            // If you don't want to destroy the original object in the scene
            objectToPickup.SetActive(true); // Hide or disable instead of destroying
        }
    }

}