﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public GameObject player;
    public Transform followTransform;
    private CharacterController p_controller;
    public Animator animator;

    public float playerSpeed = 1.0f;
    public float maxSpeed = 1000f;

    private bool isAiming = false;
    private bool canAttack = true;

    public float mouseSensitivity = 1.0f;

    public Quaternion nextRotation;
    public Vector2 _move;
    public Vector2 _look;
    public float rotationLerp = 0.5f;

    public GameObject mainCamera;
    public GameObject aimCamera;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        p_controller = player.GetComponent<CharacterController>();

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
            animator.SetFloat("MovementSpeed", Mathf.Abs(p_controller.velocity.magnitude));
            if (Mathf.Abs(Input.GetAxis("Vertical")) > 0 || Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
            {
                HandleMovement();

            }
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
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x + yRot, transform.rotation.eulerAngles.y + xRot, 0);
/*            if (Input.GetMouseButton(1))
            {
                ToggleAim(true);
                // MAKE IT SO YOU CAN USE SOME OF YOUR SPRINT JUICE / ENERGY TO PULL THE BOW EVEN HARDER!! // when attacking, energy is not used but it isnt replenished
            }
            else
            {
                if (this.isAiming)
                {
                    animator.SetTrigger("Fire");
                    Debug.Log("FIRE FIRE FIRE FIRE");
                }
                ToggleAim(false);
            }*/
            if (Input.GetMouseButton(0))
            {
                // TODO add weapon logic
                if (canAttack)
                {
                    Attack();
                }
            }
            if (Input.GetKeyDown("p"))
            {
            }
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
            mainCamera.SetActive(false);
            aimCamera.SetActive(true);
        }
        else
        {
            aimCamera.SetActive(false);
            mainCamera.SetActive(true);
        }
    }

    private void HandleMovement()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        move *= playerSpeed;
        // constrain move input to a maximum magnitude of 1, otherwise diagonal movement might exceed the max move speed defined
        move = Vector3.ClampMagnitude(move, maxSpeed);
        Vector3 movementTest = player.transform.TransformVector(move);
        // this makes sure the player can't 'fly' or go through the ground
        movementTest.y = 0.0f;
        // this ensures that movement is frame independent
        movementTest *= Time.deltaTime;
        p_controller.Move(movementTest);
/*        Debug.Log(p_controller.velocity.magnitude);
*/    }

    private float GetCurrentWeaponCooldown()
    {
        return 1.0f;
    }
    private IEnumerator WeaponCooldown(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        this.canAttack = true;
    }

    // ADD LOGIC BASED ON WEAPONS, MAKE A WEAPON MANAGER
    void Attack()
    {
        this.canAttack = false;
        animator.SetTrigger("MainAttack");
        GetComponent<AudioSource>().Play();
        StartCoroutine(WeaponCooldown(GetCurrentWeaponCooldown()));
    }

}