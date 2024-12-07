using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class HitCollider : MonoBehaviour
{
    [SerializeField] List<string> hittableTags;
    public UnityEvent<HitCollider, HurtCollider> onHitDelivered;

 

    private void OnTriggerEnter(Collider other)
    {
        CheckCollider(other);
    }
    private void OnCollisionEnter(Collision collision)
    {
        CheckCollider(collision.collider);
    }

    private void CheckCollider(Collider other)
    {
        if (hittableTags.Contains(other.tag))
        {
            HurtCollider hurtCollider = other.GetComponent<HurtCollider>();
            if (hurtCollider)
            {
                hurtCollider.NotifyHit(this);
                onHitDelivered.Invoke(this, hurtCollider);

            }
        }
    }
}
