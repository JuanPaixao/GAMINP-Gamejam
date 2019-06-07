using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMovement : MonoBehaviour
{

    [SerializeField] private float _upgradeSpeed;
    private void Update()
    {
        this.transform.Translate(new Vector2(0, _upgradeSpeed * Time.deltaTime));
    }
}
