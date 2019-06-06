using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterPlayerAnimations : MonoBehaviour
{
    [SerializeField] private Animator _animatorEngines;
    [SerializeField] private Animator _animatorPlayer;

    private void Awake()
    {
        _animatorPlayer = GetComponent<Animator>();
    }

    public void isMoving(bool move)
    {
        _animatorEngines.SetBool("isMoving", move);
    }

    public void SetUpgrade(int upgrade)
    {
        _animatorPlayer.SetInteger("Upgrade",upgrade);
    }
}
