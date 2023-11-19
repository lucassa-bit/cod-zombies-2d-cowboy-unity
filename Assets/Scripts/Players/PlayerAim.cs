using System;
using UnityEngine;

public class PlayerAim : MonoBehaviour {
    [SerializeField]
    private Transform bulletPosition;
    [SerializeField]
    private float distanceAim;
    
    [HideInInspector]
    public event EventHandler<OnShootEventArgs> onShoot;
    public class OnShootEventArgs : EventArgs {
        public Vector3 BulletEndPointPosition;
        public Vector3 BulletShootDirection;
    }

    public Vector3 ShootDirection { get; private set; }

    private void Start() {
        ShootDirection = Vector3.zero;
    }

    public void SetDirection(Vector3 direction) {
        ShootDirection = direction;
        bulletPosition.position = transform.position + (ShootDirection * distanceAim);
    }

    private void Update() {
        HandleShooting();
    }

    private void HandleShooting() {
        if (ShootDirection != Vector3.zero) {
            onShoot?.Invoke(this, new OnShootEventArgs {
                BulletEndPointPosition = bulletPosition.position,
                BulletShootDirection = ShootDirection
            });
        }
    }
}
