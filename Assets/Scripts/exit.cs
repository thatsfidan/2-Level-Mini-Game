using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PlatformExit : MonoBehaviour
{
    [SerializeField] string playerTag = "Player";
    [SerializeField] Transform platform;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals(playerTag))
        {
            Debug.Log($"Exit");
            other.gameObject.transform.SetParent(null);
            other.GetComponent<BuzzLevel2>().applyGravity = true;

        }
    }
}

