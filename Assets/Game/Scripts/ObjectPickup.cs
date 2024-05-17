using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

using KinematicCharacterController.Examples;
public class ObjectPickup : MonoBehaviour
{
    [SerializeField] public GameObject rifle; // Reference to the object to pick up
    public KeyCode pickupKey = KeyCode.E; // Key to press to pick up the object
    [SerializeField] private Transform handTransform;
    private PlayerStatManager playerStatManager;
    private Weapon weapon;
    private KinematicCharacterController.Examples.CharacterController characterController;
    [SerializeField] public GameObject pickupCanvas;
    [SerializeField] public TextMeshProUGUI shroomsText;
    [SerializeField] public GameObject victoryText;
    private int shroomsFound = 0;

    private void Start()
    {
        playerStatManager = FindObjectOfType<PlayerStatManager>();
        characterController = FindObjectOfType<KinematicCharacterController.Examples.CharacterController>();
        weapon = FindObjectOfType<Weapon>();
    }
   private void Update()
    {
        if (Input.GetKeyDown(pickupKey))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if (hit.collider.CompareTag("Rifle"))
                {
                    if (characterController != null && rifle != null && handTransform != null)
                    {
                        Collider rifleCollider = hit.collider;
                        if (rifleCollider != null)
                        {
                            rifleCollider.enabled = false;
                        }
                        rifle.transform.SetParent(handTransform);
                        
                        rifle.transform.localPosition = new Vector3(0.35f, -0.5f, 0.2f);
                        rifle.transform.localRotation = Quaternion.identity;

                        rifle.transform.localEulerAngles = new Vector3(0, 180, 0);

                        rifle.SetActive(true);
                        pickupCanvas.SetActive(false);
                    }
                }
                else if (hit.collider.CompareTag("CreamMush"))
                {
                    playerStatManager.IncreaseMovementSpeed(2f);
                    Destroy(hit.collider.gameObject);
                    shroomsFound++;
                }
                else if (hit.collider.CompareTag("RedMush"))
                {
                    playerStatManager.IncreaseHealth(20f);
                    Destroy(hit.collider.gameObject);
                    shroomsFound++;
                }
                else if (hit.collider.CompareTag("BlueMush"))
                {
                    weapon.IncreaseAttackSpeedShrooms(1f);
                    Destroy(hit.collider.gameObject);
                    shroomsFound++;
                }
                else if (hit.collider.CompareTag("PurpleMush"))
                {
                    weapon.IncreaseDamageShrooms(1f);
                    Destroy(hit.collider.gameObject);
                    shroomsFound++;
                }
            }
        }
        UpdateShroomsText();
    }
    private void UpdateShroomsText()
    {
        if (shroomsText != null)
        {
            shroomsText.text = "Shrooms: " + shroomsFound.ToString() + "/20";
        }
        if (shroomsFound == 20)
        {
            StartCoroutine(ShowVictoryAndLoadScene());
        }
    }

    private IEnumerator ShowVictoryAndLoadScene()
    {
        if (victoryText != null)
        {
            victoryText.SetActive(true);
        }
        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene("WinScreen");
    }
}