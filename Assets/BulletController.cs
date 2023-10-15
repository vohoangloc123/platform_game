using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void FireBullet()
    {
        // Kích hoạt trạng thái bắn đạn
        animator.SetTrigger("BulletFired");
    }
}