using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCardWild : CCard
{
    public override int AmountPerColor => 1;
    public override CardType cardType => CardType.WILD;
}
