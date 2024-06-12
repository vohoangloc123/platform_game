using UnityEngine;

public class PlaySoundOnTrigger : MonoBehaviour
{
    // Tham chiếu tới AudioSource
    public AudioSource audioSource;

    // Hàm này được gọi khi một đối tượng khác vào Trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Kiểm tra nếu đối tượng khác có Tag là "Player"
        if (other.CompareTag("Player"))
        {
            // Phát âm thanh
            if (audioSource != null)
            {
                audioSource.Play();
            }
        }
    }

    // Hàm này được gọi khi một đối tượng khác ra khỏi Trigger
    private void OnTriggerExit2D(Collider2D other)
    {
        // Kiểm tra nếu đối tượng khác có Tag là "Player"
        if (other.CompareTag("Player"))
        {
            // Tắt âm thanh
            if (audioSource != null)
            {
                audioSource.Stop();
            }
        }
    }
}

