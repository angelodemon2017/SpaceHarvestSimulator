using UnityEngine;

[CreateAssetMenu(fileName = "Prefabs Config", menuName = "Prefabs Config")]
public class PrefabsConfig : ScriptableObject
{
    public DronView dronView;
    public BaseView baseView;
    public ResourceView resourceView;
}