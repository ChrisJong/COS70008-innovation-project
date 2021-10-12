using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;

public class LyricsPlayer : MonoBehaviour
{
    public TextMeshProUGUI Lyrics;

    public string replacement_text;

    float[] waitTime = { 2, 1, 1, 1.5f, 2 };


    public void Start()
    {
        StartCoroutine(changeContent());
    }


    public IEnumerator changeContent()
    {
        string[] array = Lyrics.text.Split(' ');
        string[] subarray1 = null;
        string[] subarray2 = null;
        Debug.Log("Length = " + array.Length);
        string word;


        word = array[0];
        subarray1 = array.Skip(1).Take(array.Length - 1).ToArray();
        var lyric = $"<color=red>{word}</color> " + string.Join(" ", subarray1);
        Lyrics.text = lyric;
        Debug.Log(lyric);
        yield return new WaitForSeconds(waitTime[0]);
        for (int i = 1; i < array.Length; i++)
        {
            subarray1 = array.Skip(0).Take(i).ToArray();
            word = array[i];
            subarray2 = array.Skip(i+1).Take(array.Length - i).ToArray();
            lyric = string.Join(" ", subarray1) + $" <color=red>{word}</color> " + string.Join(" ", subarray2);
            Debug.Log(lyric);
            Lyrics.text = lyric;
            yield return new WaitForSeconds(waitTime[i]);
        }
    }

    public void Correct()
    {
        Lyrics.text = replacement_text;
    }

}
