using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RigidbodyControl : MonoBehaviour
{
    private Rigidbody rb;
    private XRGrabInteractable grabInteractable;
    private bool hasBeenTouched = false; // تتبع إذا تم لمس الفاكهة

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<XRGrabInteractable>();

        // في البداية تكون الفاكهة كينماتيكية وغير متأثرة بالجاذبية
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }

        // ربط الحدث عند الإمساك
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        // إذا كانت الفاكهة لم تلمس من قبل
        if (!hasBeenTouched && rb != null)
        {
            rb.isKinematic = false; // تعطيل الوضع الكينماتيكي
            rb.useGravity = true;   // تفعيل الجاذبية
            hasBeenTouched = true;  // تعيين أن الفاكهة لمست
        }
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        // التأكد من أن الفاكهة تظل متأثرة بالجاذبية وغير كينماتيكية
        if (hasBeenTouched && rb != null)
        {
            rb.isKinematic = false; // تعطيل الكينماتيكية
            rb.useGravity = true;   // تفعيل الجاذبية
        }
    }

    private void OnDestroy()
    {
        // إلغاء ربط الأحداث عند تدمير الكائن
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }
}
