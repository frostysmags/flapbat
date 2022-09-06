using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Controller_Player : MonoBehaviour
{
    public enum batType
    {
        origin,
        blood,
        brown,
        feral,
        gray,
        vampire
    };
    private enum SpriteStates
    {
        wake_up,
        flying_,
        dead_,
        attack_
    }
    private int[] stateLength = { 5, 6, 7, 6 };
    private SpriteStates currentSprite;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private float jumpForce = 3.5f;
    private int spriteInt = 1;
    private float animationSpeed = .32f;
    private float animationTimer = 0;
    private float scoreTimer = 2f;
    private bool lastJumpCheck = false;

    public batType batSelected;

    void Start()
    {
        currentSprite = SpriteStates.flying_;
        sr = gameObject.GetComponentInChildren<SpriteRenderer>();
        sr.sortingOrder = 15;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (gameControllerVariables.gameEnd == false)
        {
            scoreTimer -= 1 * Time.deltaTime;
            if (gameControllerVariables.gameEnd == false && scoreTimer <= 0)
            {
                scoreTimer = 2;
                gameControllerVariables.gameScore++;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.velocity = Vector2.up * jumpForce;
            }
            SpriteAnimator();
        } else
        {
            if (lastJumpCheck == false)
            {
                rb.velocity = new Vector2(-2, 5);
                lastJumpCheck = true;
            }
            Quaternion toRotate = Quaternion.LookRotation(Vector3.forward, rb.velocity);
            gameObject.transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotate, -360 * Time.deltaTime);
            setDeathAnimation();
        }
    }
    
    void SpriteAnimator()
    {
        animationTimer += .01f;
        if (animationTimer >= animationSpeed)
        {            if (spriteInt < stateLength[(int)currentSprite])
            {
                spriteInt += 1;
            } else
            {
                spriteInt = 1;
            }
            animationTimer = 0;
        }
        sr.sprite = Resources.Load<Sprite>("Sprites/Bats/" + Enum.GetName(typeof(batType), batSelected) + "/" + Enum.GetName(typeof(SpriteStates), currentSprite).ToString() + spriteInt);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Boundary")
        {
            gameControllerVariables.gameEnd = true;
            spriteInt = 1;
        }
    }

    private void setDeathAnimation()
    {
        if (gameObject.transform.position.y <= -5)
        {
            rb.simulated = false;
            if (spriteInt < stateLength[(int)currentSprite])
            {
                spriteInt += 1;
            }
            gameObject.transform.rotation = Quaternion.identity;
        }
        animationTimer += .01f;
        if (animationTimer >= animationSpeed)
        {
            if (gameObject.transform.position.y > -5 && spriteInt < 4)
            {
                spriteInt += 1;
            }
            animationTimer = 0;
        }
        sr.sprite = Resources.Load<Sprite>("Sprites/Bats/" + Enum.GetName(typeof(batType), batSelected) + "/dead_" + spriteInt);
    }
}