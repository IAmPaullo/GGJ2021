using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform movePoint;
    Vector2 mvmnt;
    public Animator animator;
    public LayerMask whatStopsMovement;
    public bool canMove = true;
    public Vector2 facingDirection;

    private void Start()
    {
        movePoint.parent = null;
    }

    private void Update()
    {
        if (!canMove)
            return;

        if (Input.GetKey(KeyCode.P))
            SceneManager.LoadScene(0);

        mvmnt.x = Input.GetAxisRaw("Horizontal");
        mvmnt.y = Input.GetAxisRaw("Vertical");
       
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        animator.SetFloat("Speed", mvmnt.sqrMagnitude);

        if (Vector3.Distance(transform.position, movePoint.position) <= 0.3)
        {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                if(!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, whatStopsMovement))
                {
                    animator.SetFloat("Horizontal", mvmnt.x);
                    animator.SetFloat("Vertical", mvmnt.y);
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                    facingDirection = new Vector2(mvmnt.x, mvmnt.y).normalized;
                }
            }
            else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, whatStopsMovement))
                {
                    animator.SetFloat("Horizontal", mvmnt.x);
                    animator.SetFloat("Vertical", mvmnt.y);
                    movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                    facingDirection = new Vector2(mvmnt.x, mvmnt.y).normalized;
                }
            }
        }

        Debug.DrawRay(transform.position, facingDirection, Color.red);
    }
}