using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CCard : MonoBehaviour
{
    public abstract int AmountPerColor { get; }
    public CardData data;


    public void Place()
    {
        throw new System.Exception("Place method not implemented");
    }

    public bool CanPlace()
    {
        throw new System.Exception("CanPlace method not implemented");
    }

    public void InitGameObject()
    {
        throw new System.Exception("InitGameObject method not implemented");
    }
}
