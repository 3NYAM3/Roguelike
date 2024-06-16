using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Singleton<PlayerController>
{
    public bool FacingLeft { get { return facingLeft; } }
    
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float runSpeed = 1.5f;
    [SerializeField] private float dashSpeed = 4f;
    [SerializeField] private TrailRenderer myTrailRenderer;
    [SerializeField] private Transform weaponCollider;
    [SerializeField] private AudioClip moveSound;
    [SerializeField] private AudioClip dashSound;

    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRenderer;
    private Knockback knockback;
    private float startingMoveSpeed;
    private MovableObject currentMovableObject;
    private AudioSource audioSource;

    private bool facingLeft = false;
    private bool isDashing = false;
    private bool isRunning = false;
    private bool isMoving = false;

    protected override void Awake()
    {
        base.Awake();

        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        knockback = GetComponent<Knockback>();
        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        playerControls.Combat.Dash.performed += _ => Dash();
        playerControls.Movement.Run.performed += _ => RunningStateChange();
        playerControls.Movement.Run.canceled += _ => RunningStateChange();
        playerControls.MoveObject.MoveObject.performed += _ => StartMovingObject();
        playerControls.MoveObject.MoveObject.canceled += _ => StopMovingObject();
        startingMoveSpeed = moveSpeed;
    }


    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        AdjustPlayerFacingDirection();
        Move();
    }

    public Transform GetWeaponCollider()
    {
        return weaponCollider;
    }

    private void PlayerInput()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();

        myAnimator.SetFloat("moveX", movement.x);
        myAnimator.SetFloat("moveY", movement.y);
    }

    private void Move()
    {
        if (knockback.GettingKnockedBack) { return; }
        float currentSpeed = isRunning ? moveSpeed * runSpeed : moveSpeed;
        rb.MovePosition(rb.position + movement * (currentSpeed * Time.fixedDeltaTime));

        if (movement != Vector2.zero && !isMoving) {
            isMoving = true;
            PlayMoveSound();
        } else if (movement == Vector2.zero && isMoving) {
            isMoving = false;
            StopMoveSound();
        }
    }

    private void RunningStateChange()
    {
        if(!isRunning)
        {
            isRunning = true;
        }
        else
        {
            isRunning=false;
        }
    }

    private void AdjustPlayerFacingDirection()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if(mousePos.x < playerScreenPoint.x)
        {
            mySpriteRenderer.flipX = true;
            facingLeft = true;
        }
        else
        {
            mySpriteRenderer.flipX= false;
            facingLeft = false;
        }
    }

    private void Dash()
    {
        if(!isDashing && StaminaManager.Instance.CurrentStamina > 0)
        {
            StaminaManager.Instance.UseStamina();
            PlayDashSound();

            isDashing = true;
            moveSpeed *= dashSpeed;
            myTrailRenderer.emitting = true;
            StartCoroutine(EndDashRoutine());
        }
    }

    private IEnumerator EndDashRoutine()
    {
        float dashTime = .2f;
        float dashCD = .4f;
        yield return new WaitForSeconds(dashTime);
        moveSpeed = startingMoveSpeed;
        myTrailRenderer.emitting=false;
        yield return new WaitForSeconds(dashCD);
        isDashing=false;
    }

    private void StartMovingObject()
    {
        if (currentMovableObject != null) {
            currentMovableObject.StartMoving();
        }
    }

    private void StopMovingObject() 
    {
        if (currentMovableObject != null) {
            currentMovableObject.StopMoving();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<MovableObject>(out MovableObject movableObject)) {
            currentMovableObject = movableObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<MovableObject>(out MovableObject movableObject) && currentMovableObject == movableObject) {
            currentMovableObject.StopMoving();
            currentMovableObject = null;
        }
    }

    public static void DestroyInstance() {
        if (Instance != null) {
            Destroy(Instance.gameObject);
            Instance = null;
        }
    }



    private void PlayMoveSound()
    {
        if (moveSound != null) {
            audioSource.clip = moveSound;
            audioSource.loop = true;
            audioSource.volume = 0.5f;
            audioSource.Play();
        }
    }

    private void StopMoveSound()
    {
        if (audioSource.isPlaying && audioSource.clip == moveSound) {
            audioSource.Stop();
        }
    }

    private void PlayDashSound()
    {
        if (dashSound != null) {
            audioSource.volume = 0.8f;
            audioSource.PlayOneShot(dashSound);
        }
    }

}
