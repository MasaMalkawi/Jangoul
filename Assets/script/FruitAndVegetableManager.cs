﻿using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FruitAndVegetableManager : MonoBehaviour
{
    public string objectType; // يمكن أن يكون "فاكهة" أو "خضراوات"
    private XRGrabInteractable grabInteractable;
    private Rigidbody rb;
    private Vector3 originalPosition; // لتخزين الموقع الأصلي
    private Quaternion originalRotation; // لتخزين التدوير الأصلي
    public AudioClip successSound; // الصوت التحفيزي عند وضع الفاكهة أو الخضراوات في السلة الصحيحة
    public AudioClip errorSound;   // الصوت الذي يتم تشغيله عند وضع الكائن في السلة الخاطئة
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

        // ربط الأحداث عند الإمساك
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
        bool isCorrectBasket = false; // هل الكائن وضع في السلة الصحيحة؟

        foreach (var collider in colliders)
        {
            if (collider.CompareTag("FruitBasket") && objectType == "Fruit")
            {
                // وضع الفاكهة في سلة الفواكه
                Debug.Log("تم وضع الفاكهة في سلة الفواكه");
                PlaySuccessSound(); // تشغيل الصوت التحفيزي
                isCorrectBasket = true;
                break;
            }
            else if (collider.CompareTag("VegetableBasket") && objectType == "Vegetable")
            {
                // وضع الخضراوات في سلة الخضراوات
                Debug.Log("تم وضع الخضراوات في سلة الخضراوات");
                PlaySuccessSound(); // تشغيل الصوت التحفيزي
                isCorrectBasket = true;
                break;
            }
        }

        // إذا لم يتم وضع الكائن في السلة الصحيحة، أعده إلى مكانه الأصلي وتشغيل صوت الخطأ
        if (!isCorrectBasket)
        {
            Debug.Log("تم وضع الكائن في السلة الخاطئة. إعادة الفاكهة إلى مكانها الأصلي.");
            PlayErrorSound(); // تشغيل الصوت الذي يشير إلى الخطأ
            ReturnToOriginalPosition();
        }
    }

    private void PlaySuccessSound()
    {
        if (audioSource != null && successSound != null)
        {
            audioSource.PlayOneShot(successSound); // تشغيل الصوت التحفيزي
        }
    }

    private void PlayErrorSound()
    {
        if (audioSource != null && errorSound != null)
        {
            audioSource.PlayOneShot(errorSound); // تشغيل صوت الخطأ
        }
    }

    private void ReturnToOriginalPosition()
    {
        // إعادة الكائن إلى مكانه الأصلي
        transform.position = originalPosition;
        transform.rotation = originalRotation;
    }

    private void OnDestroy()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }
}