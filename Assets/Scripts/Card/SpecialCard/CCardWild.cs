using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCardWild : CCard
{
    public static int amountPerColor = 1;
    public override int AmountPerColor => amountPerColor;
    public override CardType CardTypeEnum => CardType.WILD;
}
