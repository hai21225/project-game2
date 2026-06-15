using UnityEngine;

public class GameManager: MonoBehaviour
{

    [SerializeField] private ShadowCaster2DTileMap _shadowCasterTilemap;

    private void Start()
    {
        _shadowCasterTilemap.Generate();
    }


}