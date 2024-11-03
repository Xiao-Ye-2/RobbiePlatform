using UnityEngine;

public class WinZone : MonoBehaviour
{
    private int playerLayer;

    private void Start()
    {
        playerLayer = LayerMask.NameToLayer("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == playerLayer)
        {
            GameManager.PlayerWin();
        }
    }
}
