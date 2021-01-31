using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileWork : MonoBehaviour
{
    private GridSystem gridSystem;
    private Animator _animator;
    private PlayerController _playerController;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerController = GetComponent<PlayerController>();
        gridSystem = FindObjectOfType<GridSystem>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (gridSystem.CanDig(transform.position))
            {
                _playerController.canMove = false;

                _animator.SetTrigger("Dig");
                TileType tileType = gridSystem.Dig(((Vector2)transform.position) + _playerController.facingDirection);

                if (tileType != TileType.SKELETON)
                    StartCoroutine(DoAfterTime(.5f, () => { _playerController.canMove = true; }));
            }
        }
    }

    private IEnumerator DoAfterTime(float delay, Action action)
    {
        yield return new WaitForSecondsRealtime(delay);
        action?.Invoke();
    }
}
