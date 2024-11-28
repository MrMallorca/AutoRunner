using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class HurtCollider : MonoBehaviour
{

    public UnityEvent <HitCollider,HurtCollider> onHitReceived;
   

    

    public void NotifyHit(HitCollider hitcollider)
    {
        onHitReceived.Invoke(hitcollider, this);
    }

}
