using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class animManager : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private PlayableDirector particles;
    [SerializeField]
    private GameObject particlesSystem;

    private bool canPause;
    private bool canPlay;
    private bool canResume;

    private void Start()
    {
        canPause = true;
        canPlay = true;
    }

    public void Play()
    {
        if (canPlay)
        {
            canPause = true;
            canResume = false;

            particles.Play();
        }
    }

    public void Pause()
    {
        if (canPause)
        {
            animator.speed = 0;
            canResume = true;
            canPause = false;


            particles.Pause();


        }
    }

    public void Resume()
    {
        if (canResume)
        {
            animator.speed = 1;
            canPause = true;
            canResume = false;

            particles.Play();

        }
    }
}
