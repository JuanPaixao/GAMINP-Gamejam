using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator _animator;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    public void isMoving(bool walk)
    {
        _animator.SetBool("isMoving", walk);
    }
    public void isJumping(bool jump)
    {
        _animator.SetBool("isJumping", jump);
    }
}
