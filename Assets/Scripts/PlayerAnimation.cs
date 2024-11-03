using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private PlayerMovement playerMovement;
    private int grondID;
    private int speedID;
    private int hangingID;
    private int crouchID;
    private int fallID;


    void Start()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponentInParent<PlayerMovement>();
        rb = GetComponentInParent<Rigidbody2D>();

        grondID = Animator.StringToHash("isOnGround");
        hangingID = Animator.StringToHash("isHanging");
        crouchID = Animator.StringToHash("isCrouching");
        speedID = Animator.StringToHash("speed");
        fallID = Animator.StringToHash("verticalVelocity");
    }


    void Update()
    {
        anim.SetFloat(speedID, Mathf.Abs(playerMovement.xVelocity));
        anim.SetBool(grondID, playerMovement.onGround);
        anim.SetBool(hangingID, playerMovement.isHanging);
        anim.SetBool(crouchID, playerMovement.isCrouching);
        anim.SetFloat(fallID,rb.velocity.y);
    }

    public void StepAudio()
    {
        AudioManager.PlayFootStepAudio();
    }

    public void CrouchStepAudio()
    {
        AudioManager.PlayCrouchFootStepAudio();
    }
}
