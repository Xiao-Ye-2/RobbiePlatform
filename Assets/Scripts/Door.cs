using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator anim;
    private int openID;

    void Start()
    {
        anim = GetComponent<Animator>();
        openID = Animator.StringToHash("Open");
        GameManager.RegisterDoor(this);
    }

    public void Open()
    {
        anim.SetTrigger(openID);
        AudioManager.PlayDoorAudio();
    }
}
