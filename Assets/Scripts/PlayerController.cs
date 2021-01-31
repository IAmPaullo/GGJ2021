using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float _walkDelay = .8f;
    public float freezeDelay = .8f;
    public float freezeTime = 1f;
    public Transform movePoint;
    Vector2 mvmnt;
    public Animator animator;
    public LayerMask whatStopsMovement;
    public bool canMove = true;
    public Vector2 facingDirection;

    private float walkTimer;
    private bool canWalk;

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

        if (mvmnt.x == 0 && mvmnt.y == 0)
            walkTimer = _walkDelay;

        if(walkTimer <= 0f)
            canWalk = true;
        else
            walkTimer -= Time.deltaTime;

        if (Vector3.Distance(transform.position, movePoint.position) <= 0.3)
        {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                if(!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, whatStopsMovement))
                {
                    animator.SetFloat("Horizontal", mvmnt.x);
                    animator.SetFloat("Vertical", mvmnt.y);
                    facingDirection = new Vector2(mvmnt.x, mvmnt.y).normalized;

                    if (canWalk)
                    {
                        movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                        walkTimer = _walkDelay;
                        canWalk = false;
                    }
                }
            }
            else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, whatStopsMovement))
                {
                    animator.SetFloat("Horizontal", mvmnt.x);
                    animator.SetFloat("Vertical", mvmnt.y);
                    facingDirection = new Vector2(mvmnt.x, mvmnt.y).normalized;
                    
                    if (canWalk)
                    {
                        movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                        walkTimer = _walkDelay;
                        canWalk = false;
                    }
                }
            }
        }

        Debug.DrawRay(transform.position, facingDirection, Color.red);
    }
    public void TakeDamage()
    {
        StartCoroutine(DoAfterTime(freezeDelay, () => 
        {
            animator.SetTrigger("Damage");
            Freeze(freezeTime);
        }));
    }

    public void Freeze(float delay)
    {
        canMove = false;
        StartCoroutine(DoAfterTime(delay, () => 
        {
            canMove = true; 
        }));
    }
    private IEnumerator DoAfterTime(float delay, Action action)
    {
        yield return new WaitForSecondsRealtime(delay);
        action?.Invoke();
    }
}