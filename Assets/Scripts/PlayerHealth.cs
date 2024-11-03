using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public GameObject deathVFXprefab;
    private int trapsLayer;
    void Start()
    {
        trapsLayer = LayerMask.NameToLayer("Traps");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == trapsLayer)
        {
            Instantiate(deathVFXprefab, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
            AudioManager.PlayDeathAudio();

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
