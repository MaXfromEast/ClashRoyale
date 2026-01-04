using UnityEngine;

public class ListOfClips : MonoBehaviour
{
    [SerializeField] private AudioClip hit;
    private AudioSource audioSource;

    public void Init()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void HitSound()
    {
        audioSource.PlayOneShot(hit);
    }
}
