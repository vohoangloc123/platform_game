using UnityEngine;

public class EnemyMoveFromAToB : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D rb;
    private Animator anim;
    private Transform currentPoint;
    public float speed;
    private float left_right;
    public bool isAnimation = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentPoint = pointB.transform;
    }

    void Update()
    {
        Vector2 pointDirection = currentPoint.position - transform.position;
        rb.velocity = pointDirection.normalized * speed;
        left_right= pointDirection.x;
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f)
        {
            if (currentPoint == pointB.transform)
            {
                flip();
                if(isAnimation){
                    anim.SetFloat("run", Mathf.Abs(left_right));
                }
                currentPoint = pointA.transform;
            }
            else if (currentPoint == pointA.transform)
            {
                flip();
                if(isAnimation){
                    anim.SetFloat("run", Mathf.Abs(left_right));
                }
                currentPoint = pointB.transform;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }

    private void flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
