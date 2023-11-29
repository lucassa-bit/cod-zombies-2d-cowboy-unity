using System;
using UnityEngine;

public class PlayerAim : MonoBehaviour {
    public Transform BulletPosition;
    [SerializeField]
    private float DistanceAim;
    
    [HideInInspector]
    public event EventHandler<OnShootEventArgs> onShoot;
    public class OnShootEventArgs : EventArgs {
        public Vector3 BulletEndPointPosition;
        public Vector3 BulletShootDirection;
    }

    public Material ShootMaterial;
    public Vector3 ShootDirection { get; private set; }

    private void Start() {
        ShootDirection = Vector3.zero;
    }

    public void SetDirection(Vector3 direction) {
        ShootDirection = direction;
        BulletPosition.position = transform.position + (ShootDirection * DistanceAim);
    }

    private void Update() {
        HandleShooting();
    }

    private void HandleShooting() {
        if (ShootDirection != Vector3.zero) {
            onShoot?.Invoke(this, new OnShootEventArgs {
                BulletEndPointPosition = BulletPosition.position,
                BulletShootDirection = ShootDirection
            });
        }
    }
}
