using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour {
    [Header("Limits")]
    [SerializeField]
    private float DamageLimit;
    [SerializeField]
    private float ReloadSpeedLimit;
    [SerializeField]
    private float SpeedLimit;
    [SerializeField]
    private float AddHealthLimit; 

    [Header("Player data")]
    public float HealthMax;
    public float Health { get; private set; }
    public float AddHealth { get; private set; }
    public float BaseSpeed;
    public float Speed { get; private set; }
    public float Damage { get; private set; }
    public float ReloadSpeed { get; private set; }
    public float regen;
    [SerializeField]
    private long WalletAmount = 1000;

    [Header("Components")]
    public SpriteRenderer sprite;

    private float CooldownToHealUp;
    private float TimerToHealUp;

    private void Start() {
        CooldownToHealUp = 5f;
    }

    private void Update() {
        if(Health < Mathf.Ceil(HealthMax * (1 + AddHealth)) && Time.time > TimerToHealUp) {
            Health += regen;
        }
    }

    public void AddNewJuice(JuicesScriptableObject juice) {
        Debug.Log(juice.NameJuice);
        switch(juice.AttributeToChange) {
            case "Speed":
                Speed += Speed < SpeedLimit ? juice.AddValue / 100 : juice.AddValue / 300;
                break;
            case "ReloadSpeed":
                ReloadSpeed += ReloadSpeed < ReloadSpeedLimit ? juice.AddValue / 100 : juice.AddValue / 300;
                break;
            case "Damage":
                Damage += Damage < DamageLimit ? juice.AddValue / 100 : juice.AddValue / 300;
                break;
            case "Health":
                AddHealth += AddHealth < AddHealthLimit ? juice.AddValue / 100 : juice.AddValue / 300;
                break;
            default : break;
        }
    }

    public void DamageOnPlayer(float DamageQuantity) {
        Health -= DamageQuantity;
        TimerToHealUp = Time.time + CooldownToHealUp;

        StartCoroutine(DamagePlayer());

        if(Health <= 0) Destroy(gameObject);
    }


    private IEnumerator DamagePlayer()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }

    public bool DiscountAmount(long amount)
    {
        if (WalletAmount - amount >= 0)
        {
            WalletAmount -= amount;
            return true;
        }
        return false;
    }

    public long GetAmount()
    {
        return WalletAmount;
    }
    public void AddAmount(long value)
    {
        WalletAmount += value;
    }
}
