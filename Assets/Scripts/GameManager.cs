using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private GameObject _healthBar;
    [SerializeField] private GameObject _healthIcon;
    [SerializeField] private GameObject _objectToDestroy;
    [SerializeField] private TMP_Text _coinText;

    private bool isPaused = false;
    private bool isPausedSettings = false;
    private int coinCount = 0;

    private void Start()
    {
        ChangeHealthIcons(true, _playerController._health);
        _coinText.SetText("Coins: " + coinCount.ToString());
    }

    public void UpdateCoinCount(int value)
    {
        coinCount += value;
        _coinText.SetText("Coins: " + coinCount.ToString());
    }

    public void PauseGame(GameObject pauseScreen)
    {
        if (isPaused == false)
        {
            isPaused = true;

            pauseScreen.SetActive(true);
            Time.timeScale = 0f;
        }
        else if (isPaused == true)
        {
            isPaused = false;

            pauseScreen.SetActive(false);
            Time.timeScale = 1f;
        }   
    }

    public void OpenCloseSettings(GameObject settingsMenu)
    {
        if (isPausedSettings == false)
        {
            isPausedSettings = true;

            settingsMenu.SetActive(true);
        }
        else if (isPausedSettings == true)
        {
            isPausedSettings = false;

            settingsMenu.SetActive(false);
        }
    }

    public void ReturnToMenu (string levelName)
    {
        SceneManager.LoadScene(levelName);

        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }
    }

    public void ChangeHealthIcons(bool isGood, int numOfIcons)
    {
        PlayerController playerController = GetComponent<PlayerController>();

        if (isGood == true)
        {
            for (int i = 1; i <= numOfIcons; ++i)
            {
                Instantiate(_healthIcon, _healthBar.transform);
            }
        }
        else if (isGood == false)
        {
            for (int i = 1; i <= numOfIcons; ++i)
            {
                int size = _objectToDestroy.transform.childCount;
                int rnd = Random.Range(0, size);
                Destroy(_objectToDestroy.transform.GetChild(rnd).gameObject);
            }
        }
    }
}
