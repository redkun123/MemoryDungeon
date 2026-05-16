using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Status/New Status")]
public class StatusData : ScriptableObject
{
    public string id;
    public string statusName;
    public string description;
    public Sprite icon;
}
