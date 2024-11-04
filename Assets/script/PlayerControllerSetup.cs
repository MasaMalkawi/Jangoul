using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PlayerControllerSetup : MonoBehaviour
{
    void Start()
    {
        // تعيين الـTag إلى "Player"
        gameObject.tag = "Player";

        // الحصول على الـCollider
        Collider col = GetComponent<Collider>();

        // التأكد من أن الـCollider مُعَدّ كـ Trigger
        if (!col.isTrigger)
        {
            col.isTrigger = true;
            Debug.Log($"{gameObject.name}: Collider was set to Trigger.");
        }
    }
}

