using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    private Animator animator;
    private Vector2 movement;
    private string currentAnimation;
    private bool isAlive = true;
    public bool isGameOver;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] AudioSource musicPlayer;
    [SerializeField] AudioSource gameOverSound;


    void Start()
    {
        animator = GetComponent<Animator>();
        gameOverPanel.SetActive(false);
        isGameOver = false;
    }

    void Update()
    {
        if (!isAlive) return;
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        transform.Translate(movement * moveSpeed * Time.deltaTime);
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        if (movement.x > 0)
        {
            ChangeAnimationState("attack_right");
        }
        else if (movement.x < 0)
        {
            ChangeAnimationState("attack_left");
        }
        else if (movement.y > 0)
        {
            ChangeAnimationState("attack_up");
        }
        else if (movement.y < 0)
        {
            ChangeAnimationState("attack_down");
        }
    }

    private void ChangeAnimationState(string newState)
    {
        if (currentAnimation == newState) return; 
        animator.Play(newState);
        currentAnimation = newState;
    }

    public void TriggerDeathAnimation()
    {
        if (!isAlive) return; 
        isAlive = false;
        isGameOver = true;
        ChangeAnimationState("death");
        if (musicPlayer != null)
            musicPlayer.Stop();
        
        if (gameOverSound != null)
            gameOverSound.Play();
        Invoke(nameof(ShowGameOverScreen), animator.GetCurrentAnimatorStateInfo(0).length);
    }

    private void ShowGameOverScreen()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            BeeMovement beeMovement = other.GetComponent<BeeMovement>();
            if (beeMovement != null && !beeMovement.isDead) 
            {
                TriggerDeathAnimation();
            }
        }
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}