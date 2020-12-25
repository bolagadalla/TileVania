using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCoin : MonoBehaviour
{
    [SerializeField] AudioClip coinSFX;
    [SerializeField] int coinWorth;
    void OnTriggerEnter2D(Collider2D other)
    {
        FindObjectOfType<GameSession>().AddToCoins(coinWorth);
        AudioSource.PlayClipAtPoint(coinSFX, Camera.main.transform.position);
        Destroy(gameObject);
    }
}
