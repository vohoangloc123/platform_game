using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public float jumpForce = 10f; // Lực nhảy lên khi va chạm với trampoline
    public AudioSource audioSource;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (playerRb != null)
        {
            // Áp dụng lực nhảy theo hướng lên trên
            audioSource.Play();
            playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}

