using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class piano : MonoBehaviour
{
    public AudioClip C_Note;
    public AudioClip D_Note;
    public AudioClip E_Note;
    public AudioClip F_Note;
    public AudioClip G_Note;
    public AudioClip A_Note;
    public AudioClip B_Note;

    public AudioSource aSource;

    public AudioClip C_Note_Hold;

    public AudioClip music;
    public AudioClip music2;

    private void Start()
    {
        this.Music_play();
    }

    public void C_Note_play()
    {
        aSource.clip = C_Note;
        aSource.Play();

    }

    public void C_Note_H()
    {
        aSource.clip = C_Note_Hold;
        aSource.Play();

    }

    public void D_Note_play()
    {
        aSource.clip = D_Note;
        aSource.Play();

    }

    public void E_Note_play()
    {
        aSource.clip = E_Note;
        aSource.Play();

    }

    public void F_Note_play()
    {
        aSource.clip = F_Note;
        aSource.Play();

    }

    public void G_Note_play()
    {
        aSource.clip = G_Note;
        aSource.Play();

    }

    public void A_Note_play()
    {
        aSource.clip = A_Note;
        aSource.Play();

    }

    public void B_Note_play()
    {
        aSource.clip = B_Note;
        aSource.Play();

    }

    public void Music_play()
    {
        aSource.clip = music;
        aSource.Play();
    }

    public void Music2_play()
    {
        aSource.clip = music2;
        aSource.Play();
    }



}
