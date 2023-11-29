using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Juices", menuName = "ScriptableObjects/JuicesPerk", order = 1)]
public class JuicesScriptableObject : ScriptableObject {
    public string NameJuice;

    public string AttributeToChange;
    public float AddValue;
    public AudioClip Clip;

    public override bool Equals(object obj) {
        return obj is JuicesScriptableObject @object &&
               base.Equals(obj) &&
               name == @object.name &&
               hideFlags == @object.hideFlags &&
               NameJuice == @object.NameJuice &&
               AttributeToChange == @object.AttributeToChange &&
               AddValue == @object.AddValue;
    }

    public override int GetHashCode() {
        return HashCode.Combine(base.GetHashCode(), name, hideFlags, NameJuice, AttributeToChange, AddValue);
    }
}
