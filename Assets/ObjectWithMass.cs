using UnityEngine;

public class ObjectWithMass : MonoBehaviour
{
    public float mass = 1f;
    private Vector2 velocity = Vector2.zero;
    private Vector2 gravity = new Vector2(0f, -9.8f); // Trọng lực

    void Update()
    {
        // Áp dụng trọng lực
        velocity += gravity * Time.deltaTime;

        // Di chuyển đối tượng dựa trên vận tốc
        transform.position += (Vector3)velocity * Time.deltaTime;
    }
}
