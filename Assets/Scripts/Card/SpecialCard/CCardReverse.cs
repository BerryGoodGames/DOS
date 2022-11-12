using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCardReverse : CCard
{
    public override int AmountPerColor => 2;
    public override CardType cardType => CardType.REVERSE;
}
