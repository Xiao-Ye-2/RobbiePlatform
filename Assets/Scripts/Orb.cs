using Unity.VisualScripting;
using UnityEngine;

public class Orb : MonoBehaviour
{
    public GameObject explosionVFXPrefab;
    private int playerLayerID;
    void Start()
    {
        playerLayerID = LayerMask.NameToLayer("Player");
        GameManager.RegisterOrb(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == playerLayerID)
        {
            if (explosionVFXPrefab != null)
            {
                Instantiate(explosionVFXPrefab, transform.position, Quaternion.identity);
            }
            gameObject.SetActive(false);
            AudioManager.PlayOrbAudio();
            GameManager.OrbCollected(this);
        }
    }

}
