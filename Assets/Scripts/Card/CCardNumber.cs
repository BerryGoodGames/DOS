using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCardNumber : CCard
{
    public int Number { 
        get => data.type; 
        set { data.type = value; } 
    }
    public override int AmountPerColor => 2;
    public override CardType CardTypeEnum => CardType.NUMBER;
}
