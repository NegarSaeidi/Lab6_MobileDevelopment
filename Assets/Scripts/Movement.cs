using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [Header("Movement")]
    public float horizontalForce;
    public float verticalForce;
    public float groundRadius;
    public LayerMask groundLayerMask;
    public Transform groundOrigin;

    private Rigidbody2D body;


    public bool isGrounded;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        checkIfGrounded();
            
    }

    private void Move()
    {
        if(isGrounded)
        {
            var x = Input.GetAxisRaw("Horizontal");
            var y = Input.GetAxisRaw("Vertical");
            float jump = Input.GetAxisRaw("Jump");
            Vector2 worldTouch = new Vector2();
            //touch
            foreach (var touch in Input.touches)
            {
                worldTouch = Camera.main.ScreenToWorldPoint(touch.position);
            }

            var horizontalMove = x * horizontalForce;
            var verticalMove = jump * verticalForce;
            Debug.Log(verticalMove);

            body.AddForce(new Vector2(horizontalMove, verticalMove));
        }
     


    }
    private void  checkIfGrounded()
    {
        RaycastHit2D hit = Physics2D.CircleCast(groundOrigin.position, groundRadius, Vector2.down,groundRadius,groundLayerMask);
        isGrounded = (hit) ? false:true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundOrigin.position, groundRadius);
    }
}
