using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Create Object Information", menuName = "ScriptableObjects/Object Scriptable Object")]
public class Object_Details : ScriptableObject
{
    [SerializeField] public string itemName;
    [SerializeField] public GameObject itemGameObject;
    [SerializeField] public AudioClip soundFile;
    [SerializeField] public string objectDescription;
    [SerializeField] public string keepItem;
    [SerializeField] public string throwItem;
}
