using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    public GameObject objeto1;

    public void OnAnimationEnd()
    {

        if (objeto1 != null) objeto1.SetActive(true);
    }
}
