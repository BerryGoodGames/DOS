using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPrefab : MonoBehaviour
{
    public static MPrefab Instance { get; private set; }

    [Header("Cards")]
    public GameObject NumberCard;
    public GameObject ReverseCard;
    public GameObject SkipCard;
    public GameObject SwapCard;
    public GameObject PlusOneGlobalCard;
    public GameObject PlusTwoCard;
    public GameObject WildCard;
    public GameObject WildPlusFourCard;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }
}
