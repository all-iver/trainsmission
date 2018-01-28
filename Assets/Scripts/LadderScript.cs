using UnityEngine;

public class LadderScript : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("PlayerHitsLadder");
        }
    }
}