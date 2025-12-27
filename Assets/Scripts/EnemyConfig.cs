using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Config")]
public class EnemyConfig : ScriptableObject
{
    public int maxHP;
    public List<Card> moveSet;
}

