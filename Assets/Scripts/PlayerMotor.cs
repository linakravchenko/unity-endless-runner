using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;

    public float verticalVelocity;
    private float speed = 10.0f;
    private float jumpForce = 10.0f;
    private float gravity = 22.0f;

    private Animator anim;

    private bool isDead = false;

    public HealthBarManager healthManager;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (isDead)
            return;

        if (controller.isGrounded)
        {
            verticalVelocity = -gravity * Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0))
                if (Input.mousePosition.x > Screen.height / 8)
                {
                    verticalVelocity = jumpForce;
                    anim.Play("jump");
                }            
        }
        else
            verticalVelocity -= gravity * Time.deltaTime;

        Vector3 moveVector = Vector3.zero;
        moveVector.y = verticalVelocity;
        moveVector.z = speed;

        controller.Move(moveVector * Time.deltaTime);

    }

    public void SetSpeed(int modifier)
    {
        speed = 11.0f + modifier;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Cones" && healthManager.fillAmount > 0.0f)
        {
            healthManager.fillAmount -= 10;
            healthManager.healthText.text = (healthManager.fillAmount).ToString() + "/100";
        }
        if (col.tag == "Three" && healthManager.fillAmount > 0.0f)
        {
            healthManager.fillAmount -= 20;
            healthManager.healthText.text = (healthManager.fillAmount).ToString() + "/100";
        }
        if (col.tag == "Trash" && healthManager.fillAmount > 0.0f)
        {
            healthManager.fillAmount -= 50;
            healthManager.healthText.text = (healthManager.fillAmount).ToString() + "/100";
        }
        if (healthManager.fillAmount <= 0)
        {
            healthManager.healthText.text = "0/100";
            Death(col);
        }
    }

    private void Death(Collider col)
    {
        //isDead = true;
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            GetComponent<Score>().OnDeath();
        }
    }
}
