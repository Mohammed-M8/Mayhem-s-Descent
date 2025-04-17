using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class ElementalEffect : MonoBehaviour
{
    private bool hasHit = false;
    public int Damage;
    public enum StatusEffect
    {
        Burn,
        Knock,
        Shock,
        Slow,
        Joker
    }

    public StatusEffect effect;
    public float effectDuration = 3f;

    private void OnTriggerEnter(Collider other)
    {
        if (hasHit)
        {
            return;
        }
        if (other.CompareTag("Enemy"))
        {
            hasHit = true;
            EnemyDamage enemyDamage = other.GetComponent<EnemyDamage>();
            enemyDamage.takeDamage(Damage);
            ApplyEffect(other.gameObject);
            Destroy(gameObject);
        }
    }

    void ApplyEffect(GameObject Enemy)
    {
        StatusEffect currentEffect = effect;
        if (currentEffect == StatusEffect.Joker)
        {
            currentEffect = (StatusEffect)Random.Range(0, 3);
        }

        Component effectComponent = null;

        switch (currentEffect)
        {
            case StatusEffect.Burn:
                BurnEffect burnEffect=Enemy.AddComponent<BurnEffect>();
                burnEffect.Burn();
                effectComponent = burnEffect;
                break;

            case StatusEffect.Knock:
                KnockBackEffect knockBackEffect = Enemy.AddComponent<KnockBackEffect>();
                knockBackEffect.applyKnockBack(transform.position);
                effectComponent = knockBackEffect;
                break;

            case StatusEffect.Shock:
                ShockEffect shockEffect = Enemy.AddComponent<ShockEffect>();
                shockEffect.ApplyShock(Enemy);
                effectComponent = shockEffect;
                break;
            

        }
        Destroy(effectComponent,effectDuration);

    }

    private IEnumerator RemoveEffectAfterDuration(GameObject Enemy, Component effectComponent, float duration)
    {
        yield return new WaitForSeconds(duration);

        if (effectComponent != null)
        {
            Destroy(effectComponent);
        }
    }
}
