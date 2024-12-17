using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ColorObjectManager : MonoBehaviour
{
    public string objectColor; // اسم اللون كـ نص (مثل "Red", "Green", "Blue")
    private XRGrabInteractable grabInteractable;
    private Rigidbody rb;
    private Vector3 originalPosition; // لتخزين الموقع الأصلي
    private Quaternion originalRotation; // لتخزين التدوير الأصلي
    public AudioClip successSound; // الصوت التحفيزي عند وضع الكائن في السلة الصحيحة
    public AudioClip errorSound;   // الصوت عند وضع الكائن في السلة الخاطئة
    private AudioSource audioSource;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();

        // تخزين الموقع الأصلي والتدوير
        originalPosition = transform.position;
        originalRotation = transform.rotation;

        // تعطيل الفيزيائية في البداية
        rb.isKinematic = true;
        rb.useGravity = false;

        // ربط الأحداث عند الإمساك والإفلات
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);

        // إضافة AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        // عند الإمساك، تعطيل الحركة الكينماتيكية
        rb.isKinematic = false;
        rb.useGravity = true;
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        // عند الإفلات، تحقق من السلة
        CheckBasket();
    }

    private void CheckBasket()
    {
        // تحقق من السلة التي يتم وضع الكائن فيها
        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.5f); // المسافة التي يمكن أن تلتقط فيها السلة
        bool touchedAnyBasket = false; // هل لمس الكائن أي سلة؟
        bool isCorrectBasket = false; // هل السلة صحيحة؟

        foreach (var collider in colliders)
        {
            if (collider.CompareTag(objectColor + "Basket"))
            {
                // إذا كانت السلة هي السلة الصحيحة، قم بتشغيل الصوت التحفيزي
                Debug.Log($"تم وضع اللون {objectColor} في السلة الصحيحة.");
                PlaySuccessSound();
                isCorrectBasket = true;
                touchedAnyBasket = true;
                break;
            }
            else if (collider.CompareTag("RedBasket") || collider.CompareTag("GreenBasket") ||
                     collider.CompareTag("BlueBasket") || collider.CompareTag("YellowBasket") ||
                     collider.CompareTag("BlackBasket") || collider.CompareTag("WhiteBasket") ||
                     collider.CompareTag("OrangeBasket"))
            {
                // إذا كانت السلة خاطئة، قم بتشغيل الصوت وإرجاع الكائن
                Debug.Log($"تم وضع اللون {objectColor} في السلة الخاطئة.");
                PlayErrorSound();
                ReturnToOriginalPosition();
                touchedAnyBasket = true;
                break;
            }
        }

        // إذا لم يتم لمس أي سلة، لا تفعل شيئًا
        if (!touchedAnyBasket)
        {
            Debug.Log("لم يتم لمس أي سلة. يبقى الكائن في مكانه.");
        }
    }

    private void PlaySuccessSound()
    {
        if (audioSource != null && successSound != null)
        {
            audioSource.PlayOneShot(successSound);
        }
    }

    private void PlayErrorSound()
    {
        if (audioSource != null && errorSound != null)
        {
            audioSource.PlayOneShot(errorSound);
        }
    }

    private void ReturnToOriginalPosition()
    {
        transform.position = originalPosition;
        transform.rotation = originalRotation;
    }

    private void OnDestroy()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }
}
