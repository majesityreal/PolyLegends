using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public GameObject player;
    public Transform followTransform;
    private CharacterController p_controller;
    public Animator animator;

    public WeaponManager weaponManager;
    public AudioManager audioManager;

    public float playerSpeed = 1.0f;
    public float airDragMultiplier = 0.5f;
    public float maxSpeed = 1000f;
    public Vector3 playerVelocity;

    private bool isAiming = false;
    private float startAimTime;
    private bool canAttack = true;
    private bool canAttack2 = true;

    public float gravityDownForce = 1.0f;
    public float jumpForce = 1.0f;
    private bool isGrounded = true;
    private float lastTimeJumped = 0f;
    Vector3 p_GroundNormal;
    [Tooltip("Physic layers checked to consider the player grounded")]
    public LayerMask groundCheckLayers = -1;
    [Tooltip("distance from the bottom of the character controller capsule to test for grounded")]
    public float groundCheckDistance = 1f;

    public float mouseSensitivity = 1.0f;

    public GameObject mainCamera;
    public GameObject aimCamera;

    public LayerMask enemyLayers;

    const float k_JumpGroundingPreventionTime = 0.2f;
    const float k_GroundCheckDistanceInAir = 0.2f;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        p_controller = player.GetComponent<CharacterController>();
        if (weaponManager == null)
        {
            weaponManager = player.GetComponentInChildren<WeaponManager>();
        }
        if (audioManager == null)
        {
            audioManager = FindObjectOfType<AudioManager>();
        }
        lastTimeJumped = Time.time;
    }

    // Update is called once per frame
/*    void Update()
    {
       *//* if (Mathf.Abs(Input.GetAxis("Vertical")) > 0 || Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
        {
            isMoving = true;
        }
        if (Mathf.Abs(Input.GetAxis("Mouse X")) > 0)
        {
            mouseXMove = true;
        }
        if (Mathf.Abs(Input.GetAxis("Mouse Y")) > 0)
        {
            mouseYMove = true;
        }*//*
        

    }*/

    private void Update()
    {
        if (CanProcessInput())
        {
            GroundCheck();
            animator.SetFloat("MovementSpeed", Mathf.Abs(p_controller.velocity.magnitude));
            float xRot = 0, yRot = 0;
            if (Mathf.Abs(Input.GetAxis("Mouse X")) > 0)
            {
                xRot = (Input.GetAxis("Mouse X") * mouseSensitivity);
                // player.transform.Rotate(rotation);
                /*                Quaternion q = transform.rotation;
                                q.eulerAngles = new Vector3(q.eulerAngles.x, q.eulerAngles.y, 0);
                                player.transform.rotation = q;*/
            }
            if (Mathf.Abs(Input.GetAxis("Mouse Y")) > 0)
            {
                yRot = -(Input.GetAxis("Mouse Y") * mouseSensitivity);
                // player.transform.Rotate(rotation);
            }
            float tempXVal = transform.rotation.eulerAngles.x + yRot;
            float tempChecker = transform.rotation.eulerAngles.x + yRot;
/*            if (tempChecker < 20f)
            {
                tempXVal = Mathf.Clamp(transform.rotation.eulerAngles.x + yRot, 0f, 20f);
            }*/
            if (tempChecker < 340f && tempChecker > 20f)
            {
                tempXVal = Mathf.Clamp(transform.rotation.eulerAngles.x + yRot, 0f, 20f);
            }
            if (tempChecker > 330f)
            {
                tempXVal = Mathf.Clamp(transform.rotation.eulerAngles.x + yRot, 340f, 360f);
            }
            /*            float tempXVal = Mathf.Clamp(transform.rotation.eulerAngles.x + yRot, 0f, 20f);
            */
            /*            tempXVal = transform.rotation.eulerAngles.x + yRot;
            */
            transform.rotation = Quaternion.Euler(tempXVal, transform.rotation.eulerAngles.y + xRot, 0);

            if (Input.GetMouseButton(0))
            {
                // TODO add weapon logic
                if (canAttack)
                {
                    Attack();
                }
            }
            if (Input.GetMouseButtonDown(1) && this.canAttack2)
            {
                ToggleAim(true);
                animator.SetTrigger("DrawBow");
                PlaySound(((RangedWeapon)weaponManager.equippedWeaponSO).s_PullBow);
                this.canAttack2 = false;
                this.canAttack = false;
                StartCoroutine(WeaponCooldown(GetCurrentWeaponCooldown()));
                // MAKE IT SO YOU CAN USE SOME OF YOUR SPRINT JUICE / ENERGY TO PULL THE BOW EVEN HARDER!! // when attacking, energy is not used but it isnt replenished
            }
            if (Input.GetMouseButtonUp(1))
            {
                if (this.isAiming && weaponManager.equippedWeaponType.Equals("RangedWeapon"))
                {
                    if (Time.time - this.startAimTime < ((RangedWeapon)weaponManager.equippedWeaponSO).attackSpeed)
                    {
                        float timeLeftToAttack = ((RangedWeapon)weaponManager.equippedWeaponSO).attackSpeed - (Time.time - this.startAimTime);
                        Debug.Log(timeLeftToAttack);
                        StartCoroutine(ShootBow(timeLeftToAttack));
                        ToggleAim(false);
                        animator.SetTrigger("Fire");
                    }
                    else
                    {
                        ShootBow();
                        ToggleAim(false);
                        animator.SetTrigger("Fire");
                    }
                }
            }
            if (isGrounded && Input.GetKeyDown(KeyCode.Space))
            {
                HandleJump();
            }
            if (Input.GetKeyDown("p"))
            {
                FindObjectOfType<EnemySpawner>().SpawnEnemy("Skeleton", gameObject.transform);
            }
            HandleMovement();
        }
    }

    public bool CanProcessInput()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            return true;
        }
        return false;
    }

    private void ToggleAim(bool isAiming)
    {
        this.isAiming = isAiming;
        if (isAiming)
        {
            this.startAimTime = Time.time;
            // set the movement speed to go down
            // play bow sound
        }
        else
        {
            // set the movement speed to normal
        }
    }

    private void HandleMovement()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        if (move.x > 0)
        {
            animator.SetInteger("WalkDirection", 2);
        }
        else if (move.x < 0)
        {
            animator.SetInteger("WalkDirection", 4);
        }
        if (move.z > 0)
        {
            animator.SetInteger("WalkDirection", 1);
        }
        else if (move.z < 0)
        {
            animator.SetInteger("WalkDirection", 3);
        }
        if (isGrounded)
            /*if (Mathf.Abs(Input.GetAxis("Vertical")) > 0 || Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
            {*/
                {
            move *= playerSpeed;
            // constrain move input to a maximum magnitude of playerSpeed, otherwise diagonal movement might exceed the playerSpeed
            move = Vector3.ClampMagnitude(move, playerSpeed * (1 / (Mathf.Sqrt(2.0f))));
            playerVelocity += player.transform.TransformVector(move);
            // this makes sure the player can't 'fly' or go through the ground
/*            playerVelocity.y = 0.0f;
*/            // this ensures that movement is frame independent
            playerVelocity *= Time.deltaTime;
            /*        Debug.Log(p_controller.velocity.magnitude);
            */
        }
        else
        {
/*            float airSpeed = playerSpeed * airDragMultiplier * 0.01f;
            move *= airSpeed;
            // constrain move input to a maximum magnitude of 1, otherwise diagonal movement might exceed the max move speed defined
            move = Vector3.ClampMagnitude(move, airSpeed * (1 / (Mathf.Sqrt(2.0f))));
            playerVelocity += player.transform.TransformVector(move);*/
            playerVelocity += Vector3.down * gravityDownForce * Time.deltaTime;
        }
        p_controller.Move(playerVelocity);
    }

    private void HandleJump()
    {
        Vector3 jumpVector = new Vector3(0, 1, 0);
        jumpVector.y = player.transform.TransformVector(jumpVector).y;
        playerVelocity += jumpVector * jumpForce;
        lastTimeJumped = Time.time;
        animator.SetTrigger("Jump");
        Debug.Log("JUMPO");
    }

    private void HandleGravity()
    {
        if (!isGrounded) {
            p_controller.Move(new Vector3(0, -0.1f, 0));
        }
    }

    void GroundCheck()
    {
        // Make sure that the ground check distance while already in air is very small, to prevent suddenly snapping to ground
        float chosenGroundCheckDistance = isGrounded ? (p_controller.skinWidth + groundCheckDistance) : k_GroundCheckDistanceInAir;

        // reset values before the ground check
        isGrounded = false;
        p_GroundNormal = Vector3.up;

        // only try to detect ground if it's been a short amount of time since last jump; otherwise we may snap to the ground instantly after we try jumping
        if (Time.time >= lastTimeJumped + k_JumpGroundingPreventionTime)
        {
            // if we're grounded, collect info about the ground normal with a downward capsule cast representing our character capsule
            if (Physics.CapsuleCast(GetCapsuleTopHemisphere(p_controller.height), GetCapsuleBottomHemisphere(), p_controller.radius, Vector3.down, out RaycastHit hit, chosenGroundCheckDistance, groundCheckLayers, QueryTriggerInteraction.Ignore))
            {
/*                Debug.Log("Raycasting downwards!!!");
*/                // storing the upward direction for the surface found
                p_GroundNormal = hit.normal;

                // Only consider this a valid ground hit if the ground normal goes in the same direction as the character up
                // and if the slope angle is lower than the character controller's limit
                if (Vector3.Dot(hit.normal, transform.up) > 0f &&
                    IsNormalUnderSlopeLimit(p_GroundNormal))
                {
                    isGrounded = true;
                    // handle snapping to the ground
                    if (hit.distance > p_controller.skinWidth)
                    {
                        p_controller.Move(Vector3.down * hit.distance);
                        p_controller.Move(new Vector3(0, -player.transform.position.y, 0));
                    }
                }
            }
        }
    }

    // Returns true if the slope angle represented by the given normal is under the slope angle limit of the character controller
    bool IsNormalUnderSlopeLimit(Vector3 normal)
    {
        return Vector3.Angle(transform.up, normal) <= p_controller.slopeLimit;
    }

    private float GetCurrentWeaponCooldown()
    {
        // created this variable for future modifiers to be able to be applied. 1.0f is the default attack speed
        float weaponCooldown = 1.0f;
        if (weaponManager.equippedWeaponSO.GetWeaponType().Equals("Melee"))
        {
            MeleeWeapon tempWeapon = (MeleeWeapon) weaponManager.equippedWeaponSO;
            weaponCooldown = tempWeapon.attackSpeed;
        }
        if (weaponManager.equippedWeaponSO.GetWeaponType().Equals("Ranged"))
        {
            RangedWeapon tempWeapon = (RangedWeapon)weaponManager.equippedWeaponSO;
            weaponCooldown = tempWeapon.attackSpeed;
        }
        if (weaponManager.equippedWeaponSO.GetWeaponType().Equals("Magic"))
        {
            MagicWeapon tempWeapon = (MagicWeapon)weaponManager.equippedWeaponSO;
            weaponCooldown = tempWeapon.attackSpeed;
        }
        return weaponCooldown;
    }

    private float GetCurrentWeaponSoundDelay()
    {
        float soundDelay = 0.15f;
        if (weaponManager.equippedWeaponSO.GetWeaponType().Equals("Melee"))
        {
            MeleeWeapon tempWeapon = (MeleeWeapon)weaponManager.equippedWeaponSO;
            soundDelay = tempWeapon.soundDelay;
        }
        if (weaponManager.equippedWeaponSO.GetWeaponType().Equals("Ranged"))
        {
            RangedWeapon tempWeapon = (RangedWeapon)weaponManager.equippedWeaponSO;
            soundDelay = tempWeapon.soundDelay;
        }
        if (weaponManager.equippedWeaponSO.GetWeaponType().Equals("Magic"))
        {
            MagicWeapon tempWeapon = (MagicWeapon)weaponManager.equippedWeaponSO;
            soundDelay = tempWeapon.soundDelay;
        }
        return soundDelay;
    }
    private IEnumerator WeaponCooldown(float totalDelay)
    {
        yield return new WaitForSeconds(totalDelay);
        this.canAttack = true;
        this.canAttack2 = true;
        if (weaponManager.equippedWeaponType.Equals("MeleeWeapon"))
        {
            weaponManager.currentWeapon.GetComponent<SwordWeaponBrain>().setIdle(true);
        }
    }

    private IEnumerator PlaySound(string sound, float playSoundTime)
    {
        yield return new WaitForSeconds(playSoundTime);
        audioManager.Play(sound);
    }

    private void PlaySound(string sound)
    {
        audioManager.Play(sound);
    }


    // ADD LOGIC BASED ON WEAPONS, MAKE A WEAPON MANAGER
    void Attack()
    {
        this.canAttack = false;
        animator.SetTrigger("MainAttack");
        StartCoroutine(WeaponCooldown(GetCurrentWeaponCooldown()));
        switch (weaponManager.equippedWeaponType) {
            case "MeleeWeapon":
                StartCoroutine(PlaySound(((MeleeWeapon)weaponManager.equippedWeaponSO).s_SwordSwing, GetCurrentWeaponSoundDelay()));
                weaponManager.currentWeapon.GetComponent<SwordWeaponBrain>().setIdle(false);
                break;
            case "RangedWeapon":
                StartCoroutine(PlaySound(((RangedWeapon)weaponManager.equippedWeaponSO).s_PullBow, 0f));
                StartCoroutine(PlaySound(((RangedWeapon)weaponManager.equippedWeaponSO).s_ShootArrow, GetCurrentWeaponSoundDelay()));
                break;
            case "MagicWeapon":
                break;
        }
    }

    void Attack2()
    {
        animator.SetTrigger("Attack2");
        if (weaponManager.getCurrentWeapon().GetComponent<BowWeaponBrain>() != null) {
            Vector3 localEulerAngles = player.transform.eulerAngles;
            weaponManager.getCurrentWeapon().GetComponent<BowWeaponBrain>().setArrowRotation(localEulerAngles);
            weaponManager.getCurrentWeapon().GetComponent<BowWeaponBrain>().Shoot();
        }
        this.canAttack2 = false;
        StartCoroutine(WeaponCooldown(GetCurrentWeaponCooldown()));
        StartCoroutine(PlaySound("", GetCurrentWeaponSoundDelay()));
    }

    void ShootBow()
    {
        if (weaponManager.getCurrentWeapon().GetComponent<BowWeaponBrain>() != null)
        {
            Vector3 localEulerAngles = player.transform.eulerAngles;
            weaponManager.getCurrentWeapon().GetComponent<BowWeaponBrain>().setArrowRotation(localEulerAngles);
            weaponManager.getCurrentWeapon().GetComponent<BowWeaponBrain>().Shoot();
            StartCoroutine(PlaySound(((RangedWeapon)weaponManager.equippedWeaponSO).s_ShootArrow, 0f));
        }
    }

    private IEnumerator ShootBow(float delay)
    {
        yield return new WaitForSeconds(delay);
        ShootBow();
    }

    // Gets the center point of the bottom hemisphere of the character controller capsule    
    Vector3 GetCapsuleBottomHemisphere()
    {
        return transform.position + (transform.up * p_controller.radius);
    }

    // Gets the center point of the top hemisphere of the character controller capsule    
    Vector3 GetCapsuleTopHemisphere(float atHeight)
    {
        return transform.position + (transform.up * (atHeight - p_controller.radius));
    }
}
