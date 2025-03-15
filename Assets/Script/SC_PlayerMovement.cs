using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SC_PlayerMovement : MonoBehaviour
{

    [SerializeField] private float movementSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movementInput;
    private Animator animator;
    private bool playingFootsteps = false;
    public float footstepSpeed = 0.2f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = movementInput * movementSpeed;
        if (rb.velocity.magnitude > 0 && !playingFootsteps)
        {
            SartFootsteps();
        }
        else if (rb.velocity.magnitude == 0 )
        {
            StopFootsteps();
        }
    }

    public void Move(InputAction.CallbackContext c)
    {
        animator.SetBool("isWalking", true);

        if (c.canceled)
        {
            animator.SetBool("isWalking",false);
            animator.SetFloat("LastInputX",movementInput.x);
            animator.SetFloat("LastInputY",movementInput.y);
        }

        movementInput = c.ReadValue<Vector2>();
        animator.SetFloat("InputX",movementInput.x);
        animator.SetFloat("InputY",movementInput.y);
    }

    void SartFootsteps()
    {
        playingFootsteps = true;
        InvokeRepeating(nameof(PlayFootsep), 0f,footstepSpeed);
        
    }
    void StopFootsteps()
    {
        playingFootsteps = false;
        CancelInvoke(nameof(PlayFootsep));
    }
    void PlayFootsep()
    {
        SC_SoundManager.Play("Walking", true);
    }
}
