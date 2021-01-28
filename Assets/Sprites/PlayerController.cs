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

    private void Start()
    {
        movePoint.parent = null;
        
    }

    private void Update()
    {

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
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);

                }
            }

            else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, whatStopsMovement))
                {
                    animator.SetFloat("Vertical", mvmnt.y);
                    movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                }
            }

        }

    }
}
