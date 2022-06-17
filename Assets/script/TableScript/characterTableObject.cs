using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="character",menuName ="characterTable") ]
public class characterTableObject : ScriptableObject
{
    [SerializeField]public  GameObject characterObject;
    [SerializeField]public  GameObject characterPlayer;
}
