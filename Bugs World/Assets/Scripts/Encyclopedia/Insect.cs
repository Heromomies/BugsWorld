using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Insect", menuName = "Scriptable Object/Insect")]
public class Insect : ScriptableObject
{
    public Sprite image;
    public new string name;
    public bool found;
    
}
