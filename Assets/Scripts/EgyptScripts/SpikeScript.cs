using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpikeScript : MonoBehaviour
{
    public float openDuration = 2f; // Time in seconds the spikes stay open
    public float retractionDuration = 1f; // Time in seconds for each state change

    private bool isOpen = false;
    private Animator spikeAnimator;

    private void Start()
    {
        spikeAnimator = GetComponent<Animator>();
        StartCoroutine(OpenCloseSpikes());
    }

    private IEnumerator OpenCloseSpikes()
    {
        while (true)
        {
            OpenSpikes();
            yield return new WaitForSeconds(openDuration);
            RetractSpikes();
            yield return new WaitForSeconds(retractionDuration);
        }
    }

    private void OpenSpikes()
    {
        isOpen = true;
        spikeAnimator.Play("Spike_animation");
    }

    private void RetractSpikes()
    {
        isOpen = false;
        spikeAnimator.Play("Spike_retract");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isOpen)
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            SceneManager.LoadScene("GameOver");
        }
    }
}
