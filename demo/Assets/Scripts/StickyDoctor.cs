using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyDoctor : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "toolholder")
        {
            other.gameObject.GetComponent<Rigidbody>().useGravity = false;
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            Debug.Log("Doctor Trigger Enter with toolholder");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "toolholder")
        {
            other.gameObject.GetComponent<Rigidbody>().useGravity = true;
            other.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            Debug.Log("Doctor Trigger Exit with toolholder");
        }
    }
}
