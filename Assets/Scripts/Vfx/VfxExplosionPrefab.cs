
using UnityEngine;
using DG.Tweening;

public class VfxExplosionPrefab : MonoBehaviour
{
    [SerializeField] ParticleSystem flash;
    [SerializeField] ParticleSystem fire;
    [SerializeField] ParticleSystem smoke;

    private void OnEnable()
    {
        flash.Play();
        fire.Play();
        smoke.Play();

        DOVirtual.DelayedCall(1f, () => Destroy(gameObject));
    }


}
