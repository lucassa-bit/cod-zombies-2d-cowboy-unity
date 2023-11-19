using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    [SerializeField]
    private long walletAmount = 1000;

    public void discountAmount(long amount) {
        if(walletAmount - amount >= 0)
            this.walletAmount -= amount;
    }

    public long getAmount() { return this.walletAmount; }
}
