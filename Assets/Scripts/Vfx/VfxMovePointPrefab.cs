
using UnityEngine;

public class VfxMovePointPrefab : MonoBehaviour
{
    [SerializeField] ParticleSystem[] particlesArray;

    public void PalyVfx()
    {
        foreach (var item in particlesArray)
        {
            item.Play();
        }
    }

    public void StopVfx()
    {
        foreach (var item in particlesArray)
        {
            item.Stop();
        }
    }
}
