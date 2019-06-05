using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterPlayerAnimations : MonoBehaviour
{
    [SerializeField] private Animator _animatorEngines;


    public void isMoving(bool move)
    {
        _animatorEngines.SetBool("isMoving", move);
    }
}
