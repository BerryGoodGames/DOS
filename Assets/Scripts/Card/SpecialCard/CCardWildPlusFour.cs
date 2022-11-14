using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCardWildPlusFour : CCardWild
{
    public override int AmountPerColor => 1;
    public override CardType cardType => CardType.WILD_PLUS_FOUR;
}
