using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class MovementScript : MonoBehaviour
{
    private bool _facingRight = true;
    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.velocity = new Vector2(Random.Range(1.5f, 2.5f), 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _rigidbody.velocity = -_rigidbody.velocity;
    }

    private void Update()
    {
        Vector2 speed = _rigidbody.velocity;

        if (speed.x > 0 && _facingRight == false)
        {
            transform.localScale = new Vector2(1, 1);
            _facingRight = true;
        }
        else if (speed.x < 0 && _facingRight == true)
        {
            transform.localScale = new Vector2(-1, 1);
            _facingRight = false;
        }
    }
}
