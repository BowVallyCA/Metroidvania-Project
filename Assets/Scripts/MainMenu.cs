using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private bool isOpen = false;
    public void PlayGame(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void OpenCloseSettings(GameObject settingsMenu)
    {
        if (isOpen == false)
        {
            isOpen = true;

            settingsMenu.SetActive(true);
        }
        else if (isOpen == true)
        {
            isOpen = false;

            settingsMenu.SetActive(false);
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
