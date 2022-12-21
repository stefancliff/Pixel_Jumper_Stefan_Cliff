using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip CoinPickUpSFX;
    [SerializeField] int CoinPointValue = 100;

    bool wasCollected = false;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && !wasCollected)
        {
            wasCollected = true;
            AudioSource.PlayClipAtPoint(CoinPickUpSFX, Camera.main.transform.position, 0.35f); // the 0.35f is the volume
            FindObjectOfType<GameSession>().AddToScore(CoinPointValue);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
