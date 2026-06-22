using UnityEngine;

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    public int Id;
    public string ItemName;
    public Sprite Icon;
    public GameObject Prefab;
}