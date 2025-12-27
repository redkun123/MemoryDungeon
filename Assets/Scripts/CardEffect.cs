using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Card/Card Effect")]
public abstract class CardEffect : ScriptableObject
{   
    public abstract void Execute(CardContext ctx);
}
