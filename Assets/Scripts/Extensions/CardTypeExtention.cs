using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public static class CardTypeExtention
{
    private static Dictionary<CardType, Type> TypeDict = new()
    {
        { CardType.NUMBER, typeof(CCardNumber) },
        { CardType.PLUS_TWO, typeof(CCardPlusTwo) },
        { CardType.PLUS_ONE_GLOBAL, typeof(CCardPlusOneGlobal) },
        { CardType.WILD, typeof(CCardWild) },
        { CardType.WILD_PLUS_FOUR, typeof(CCardWildPlusFour) },
        { CardType.REVERSE, typeof(CCardReverse) },
        { CardType.SWAP, typeof(CCardSwap) },
        { CardType.SKIP, typeof(CCardSkip) },
    };

    public static Type GetCardType(this CardType cardType)
    {
        return TypeDict[cardType];
    }
}
