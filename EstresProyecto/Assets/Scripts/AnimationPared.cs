using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class AnimationPared : MonoBehaviour
{
    public PlayableDirector director;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (director != null)
            {
                director.Play();
            }

            // Desactiva este GameObject para que no vuelva a activarse
            gameObject.SetActive(false);
        }
    }
}
