using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matcher2 : MonoBehaviour
{
    public List<GameObject> PlacedObject = new List<GameObject>();
    public GameObject PointA;

    private void OnTriggerEnter(Collider other)
    {
        if (PlacedObject.Count == 0)
        {
            other.gameObject.transform.position = PointA.transform.position;
            other.gameObject.transform.rotation = PointA.transform.rotation;
            PlacedObject.Add(other.gameObject);
        }
        else if (other.gameObject.tag == PlacedObject[0].tag)
        {
            Debug.Log("Aynı nesne");
            Destroy(other.gameObject);
            Destroy(PlacedObject[0].gameObject);
            PlacedObject.Clear();
        }
        else//deneme
        {
            other.GetComponent<Rigidbody>().velocity = new Vector3(0, 1, 1) * 120 * Time.deltaTime;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (PlacedObject.Contains(other.gameObject))
        {
            PlacedObject.Remove(other.gameObject);
        }
    }
}