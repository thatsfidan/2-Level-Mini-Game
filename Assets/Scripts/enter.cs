using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PlatformEnter : MonoBehaviour
{
    [SerializeField] string playerTag = "Player";
    [SerializeField] Transform platform;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals(playerTag))
        {
            Debug.Log("Enter");
            other.gameObject.transform.SetParent(platform);
            other.GetComponent<BuzzLevel2>().applyGravity = false;
        }
    }
}

