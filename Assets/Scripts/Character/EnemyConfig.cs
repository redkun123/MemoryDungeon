using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Config")]
public class EnemyConfig : ScriptableObject
{
    public string charName;
    public int maxHP;
    public List<Card> moveSet;
    public int starCount;
    public Sprite enemyAvatar;
}

