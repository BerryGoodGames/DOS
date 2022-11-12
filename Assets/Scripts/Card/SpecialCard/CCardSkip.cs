using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCardSkip : CCard
{
    public override int AmountPerColor => 2;
    public override CardType cardType => CardType.SKIP;
}
