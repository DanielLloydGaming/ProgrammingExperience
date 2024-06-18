using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BoxSO", menuName = "ScriptableObjects/Box ScriptableObject")]
public class BoxListSO : ScriptableObject
{
    [SerializeField] public List<string> itemsInBoxList = new List<string>();
}
