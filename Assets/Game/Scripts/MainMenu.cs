using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject infoPanel;
    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Gameplay");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ShowInformationPanel()
    {
        infoPanel.SetActive(!infoPanel.activeSelf);
    }

    public void QuitGame()
    {
        Application.Quit(); // Quit the game
    }
}