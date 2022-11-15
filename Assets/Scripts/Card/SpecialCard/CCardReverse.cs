using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCardReverse : CCard
{
    public static int amountPerColor = 2;
    public override int AmountPerColor => amountPerColor;
    public override CardType CardTypeEnum => CardType.REVERSE;
}
