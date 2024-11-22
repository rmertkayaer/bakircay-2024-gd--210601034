using UnityEngine;

public class Matcher : MonoBehaviour
{
    public Transform snapPoint;
    private GameObject placedObject = null;
    private Vector3 placedObjectInitialPosition;
    private Quaternion placedObjectInitialRotation;

    private void Start()
    {
        // Platformun Rigidbody bileşenini kinematik yaparak sabit kalmasını sağlayın
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (placedObject == null)
        {
            placedObjectInitialPosition = other.transform.position;
            placedObjectInitialRotation = other.transform.rotation;

            // Nesnenin Rigidbody bileşenini geçici olarak devre dışı bırakın
            Rigidbody otherRb = other.GetComponent<Rigidbody>();
            if (otherRb != null)
            {
                otherRb.isKinematic = true;
            }

            other.transform.position = snapPoint.position;
            other.transform.rotation = snapPoint.rotation;
            placedObject = other.gameObject;
        }
        else if (other.gameObject != placedObject)
        {
            if (other.gameObject.tag == placedObject.tag)
            {
                Destroy(other.gameObject);
                Destroy(placedObject);
                placedObject = null;
            }
            else
            {
                // Nesneleri başlangıç pozisyonlarına geri döndürün
                placedObject.transform.position = placedObjectInitialPosition;
                placedObject.transform.rotation = placedObjectInitialRotation;

                Items otherItem = other.GetComponent<Items>();
                if (otherItem != null)
                {
                    otherItem.ReturnToInitialPosition();
                }

                placedObject = null;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Nesne platformdan ayrıldığında Rigidbody bileşenini tekrar etkinleştirin
        if (other.gameObject == placedObject)
        {
            Rigidbody otherRb = other.GetComponent<Rigidbody>();
            if (otherRb != null)
            {
                otherRb.isKinematic = false;
            }
            placedObject = null;
        }
    }
}