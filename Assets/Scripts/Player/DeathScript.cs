using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DeathScript : MonoBehaviour
{
    [Header("Events")]
    public GameEvent onPlayerDeath;

    [SerializeField] private Collider playerCollider;
    [SerializeField] private AudioSource deathSound;
    [SerializeField] private Volume deathEffect;

    private bool isVolumeChanging = false;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.contacts[0].otherCollider.gameObject;

        if (other.CompareTag("EnemyBullet") || other.CompareTag("EnemyBlade"))
        {
            deathSound.Play();

            StartCoroutine(ChangeVolumeWeight(0.5f)); // Start the volume change coroutine

            StartCoroutine(WaitForVolumeChangeAndRaiseEvent()); // Wait for the volume change to end and raise the event
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyBlade"))
        {
            deathSound.Play();

            StartCoroutine(ChangeVolumeWeight(0.5f)); // Start the volume change coroutine

            StartCoroutine(WaitForVolumeChangeAndRaiseEvent()); // Wait for the volume change to end and raise the event
        }
    }

    private IEnumerator ChangeVolumeWeight(float duration)
    {
        isVolumeChanging = true;
        float initialWeight = deathEffect.weight; 
        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            deathEffect.weight = Mathf.Lerp(initialWeight, 1, t); 

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        deathEffect.weight = 1;
        isVolumeChanging = false; 
    }

    private IEnumerator WaitForVolumeChangeAndRaiseEvent()
    {
        while (isVolumeChanging) // Wait until volume change is complete
        {
            yield return null;
        }

        
        deathEffect.weight = 0f; 
        onPlayerDeath.Raise(this, null); // Raise the event after volume change is complete
    }

}
