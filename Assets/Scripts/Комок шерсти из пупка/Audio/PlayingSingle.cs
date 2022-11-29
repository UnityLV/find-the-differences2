using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LibraryForGames;

[RequireComponent(typeof(AudioSource))]
public class PlayingSingle : MonoBehaviour
{
    [SerializeField] private AudioClip[] _audioClips;
    [SerializeField] private float _startDelay;


    private IEnumerator Start()
    {
        yield return new WaitForSeconds(_startDelay);
        var sourse = GetComponent<AudioSource>();
        sourse.PlayOneShot(_audioClips.RandomElement());
    }
}
