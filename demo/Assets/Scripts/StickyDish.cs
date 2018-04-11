using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyDish : MonoBehaviour {
    Rigidbody rBody;

    private void Awake()
    {
        rBody = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision hit)
    {
        if( hit.gameObject.tag == "sticky")
        {
            hit.gameObject.GetComponent<Rigidbody>().useGravity = false;

            hit.gameObject.AddComponent<FixedJoint>().connectedBody = gameObject.GetComponent<Rigidbody>();
            Debug.Log("Sticky Collision");
        }
    }

    public void Disconnect()
    {
        Debug.Log("Destroying");
        Destroy(gameObject.GetComponent<FixedJoint>());
    }

}
