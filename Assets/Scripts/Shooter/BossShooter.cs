using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShooter : MonoBehaviour
{
    [SerializeField] private float _speed;
    private float _angle;
    [SerializeField] private float _amplitude;
    public int testCount;
    [SerializeField] private Animator _cameraAnimator;
    [SerializeField] private ShooterPlayer _player;
    [SerializeField] private int _HP = 30;
  
    public void SetBossCamera()
    {
        _cameraAnimator.SetTrigger("Boss");
        _player.limitX *= 1.5f;
        _player.limitY *= 1.5f;
    }
}
 
