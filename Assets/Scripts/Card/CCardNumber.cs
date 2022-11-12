using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCardNumber : CCard
{
    public int number;
    public override int AmountPerColor => number == 0 ? 1 : 2;
}
