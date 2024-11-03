using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private bool gameOver = false;
    private SceneFader fader;
    private Door door;
    private List<Orb> orbs;
    private float gameTime;
    private int deathNumber;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        orbs = new List<Orb>();
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (gameOver) return;

        gameTime += Time.deltaTime;
        UIManager.UpdateTimeUI(gameTime);
    }

    public static void OrbCollected(Orb orb)
    {
        instance.orbs.Remove(orb);
        if (instance.orbs.Count == 0)
        {
            instance.door.Open();
        }
        UIManager.UpdateOrbUI(instance.orbs.Count);
    }

    public static void RegisterSceneFader(SceneFader fader)
    {
        instance.fader = fader;
    }

    public static void RegisterOrb(Orb orb)
    {
        instance.orbs.Add(orb);
        UIManager.UpdateOrbUI(instance.orbs.Count);
    }

    public static void RegisterDoor(Door door)
    {
        instance.door = door;
    }

    public static void PlayerDeath()
    {
        instance.fader?.FadeOut();
        instance.deathNumber += 1;
        UIManager.UpdateDeathUI(instance.deathNumber);
        instance.Invoke("RestartScene", 1.5f);
    }

    public static void PlayerWin()
    {
        instance.gameOver = true;
        UIManager.UpdateGameOverUI(true);
        AudioManager.PlayWinAudio();
    }

    public static bool isGameOver() => instance.gameOver;

    private void RestartScene()
    {
        instance.orbs.Clear();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
