using UnityEngine;

public class QTButton : MonoBehaviour
{
    public string warpName;
    public GameObject warpPos;
    public bool overrideDistance;
    public Vector3 distanceOverride;

    [Header("Star")]
    public bool starTravel;
    public SceneField starScene;

    [Header("Misc")]
    public bool diabledInScene;
}
