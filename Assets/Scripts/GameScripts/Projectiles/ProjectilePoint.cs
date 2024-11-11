using System.Collections;
using UnityEngine;

public class ProjectilePoint : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float damageDelay;
    [SerializeField] float lifeTime;

    private UnitGeneral target;

    private float damage;
    private float lifeTimer;

    public void Init(float _damage)
    {
        target = GameController.Instance.player;
        damage = _damage;
        lifeTimer = lifeTime;

        StartCoroutine(StartAppearindAnim());
    }

    private void Update()
    {
        lifeTimer -= Time.deltaTime;
        
        if (lifeTimer <= 0)
        {
            StartCoroutine(StartDisappearindAnim());
        }
    }

    private IEnumerator StartAppearindAnim()
    {
        animator.SetBool("Appears", true);

        yield return new WaitForSeconds(damageDelay);

        Vector3 projectilePos = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 targetPos = new Vector3(target.transform.position.x, 0, target.transform.position.z);
        float distance = Vector3.Distance(targetPos, projectilePos);
        
        if (distance < 0.2f)
        {
            target.GetHit(damage);
        }
    }

    private IEnumerator StartDisappearindAnim()
    {
        animator.SetBool("Appears", false);

        yield return new WaitForSeconds(1f);

        Destroy(gameObject);
    }
}
