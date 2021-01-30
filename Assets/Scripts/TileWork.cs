﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileWork : MonoBehaviour
{
    public GridSystem gridSystem;
    private Animator _animator;
    private PlayerController _playerController;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (gridSystem.CanDig(transform.position))
            {
                _playerController.canMove = false;

                _animator.SetTrigger("Dig");
                gridSystem.Dig(((Vector2)transform.position) + _playerController.facingDirection);

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
