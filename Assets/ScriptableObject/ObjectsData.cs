using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ObjectsData : ScriptableObject
{
    public List<ObjectData> objectDatas;
}

[Serializable]
public class ObjectData
{
    [field: SerializeField]
    public string Name { get; private set; }
    
    [field: SerializeField]
    public int ID { get; private set; }
    
    [field: SerializeField]
    public Vector2Int Size { get; private set; }
    
    [field: SerializeField]
    public GameObject Prefab { get; private set; }
    
}