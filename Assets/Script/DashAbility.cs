using UnityEngine;

public class DashAbility : MonoBehaviour
{
    public DashState dashState;
    public float dashDuration = 0.2f; // The duration of the dash in seconds
    public float dashCooldown = 1f; // The cooldown time between dashes

    public Rigidbody2D rb; // Change from Rigidbody to Rigidbody2D
    private Vector2 savedVelocity; // Change from Vector3 to Vector2
    private float dashTimer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Change from GetComponent<Rigidbody>() to GetComponent<Rigidbody2D>()
    }

    private void Update()
    {
        CheckInput();
        UpdateDashState();
    }

    private void CheckInput()
    {
        if (dashState == DashState.Ready && Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartDash();
        }
    }

    private void UpdateDashState()
    {
        switch (dashState)
        {
            case DashState.Dashing:
                dashTimer += Time.fixedDeltaTime; // You can use Time.deltaTime for Rigidbody2D as well
                if (dashTimer >= dashDuration)
                {
                    StopDash();
                }
                break;
            case DashState.Cooldown:
                dashTimer -= Time.fixedDeltaTime; // You can use Time.deltaTime for Rigidbody2D as well
                if (dashTimer <= 0)
                {
                    dashTimer = 0;
                    dashState = DashState.Ready;
                }
                break;
        }
    }

    private void StartDash()
    {
        dashState = DashState.Dashing;
        savedVelocity = rb.velocity;
        rb.velocity = new Vector2(rb.velocity.x * 3f, rb.velocity.y); // Change from Vector3 to Vector2
    }

    private void StopDash()
    {
        dashTimer = 0;
        rb.velocity = savedVelocity;
        dashState = DashState.Cooldown;
        Invoke(nameof(ResetDash), dashCooldown);
    }

    private void ResetDash()
    {
        dashState = DashState.Ready;
    }
}

public enum DashState
{
    Ready,
    Dashing,
    Cooldown
}
