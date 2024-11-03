using UnityEngine;

public class SceneFader : MonoBehaviour
{
    private Animator anim;
    private int faderID;


    private void Start()
    {
        anim = GetComponent<Animator>();
        faderID = Animator.StringToHash("Fade");
        GameManager.RegisterSceneFader(this);
    }

    public void FadeOut()
    {
        anim.SetTrigger(faderID);
    }
}
