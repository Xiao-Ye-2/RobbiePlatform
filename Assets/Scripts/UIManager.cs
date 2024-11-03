using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public TextMeshProUGUI orbText, timeText, deathText, gameOverText;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public static void UpdateOrbUI(int orbs) => instance.orbText.text = orbs.ToString();
    public static void UpdateTimeUI(float time)
    {
        int minutes = (int)time / 60;
        float seconds = time % 60;
        instance.timeText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }
    public static void UpdateDeathUI(int deaths) => instance.deathText.text = deaths.ToString();
    public static void UpdateGameOverUI(bool active) => instance.gameOverText.enabled = active;
}
