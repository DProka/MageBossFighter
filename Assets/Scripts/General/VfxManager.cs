
using UnityEngine;

public class VfxManager
{
    public static VfxManager Instance;

    private Transform parentTransform;
    private VfxBase vfxBase;

    public VfxManager(VfxBase _vfxBase)
    {
        Instance = this;

        vfxBase = _vfxBase;
    }

    public void StartExplosion(Vector3 position)
    {
        Object.Instantiate(vfxBase.explosionPrefab, position, Quaternion.identity, parentTransform);
    }
}
