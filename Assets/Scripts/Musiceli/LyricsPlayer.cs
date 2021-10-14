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

    public float[] waitTime;



    public void Start()
    {
        StartCoroutine(changeContent());
    }


    public IEnumerator changeContent()
    {
        string[] array = Lyrics.text.Split(' ');
        string[] subarray1;
        string[] subarray2;
        string word;
        word = array[0];


        string[] array2 = replacement_text.Split(' ');
        int wordlength = array2[array2.Length - 1].Length;
        if (array[array.Length - 1].Length > 19)
        {
            string wordz = string.Join("", array[array.Length - 1].Skip(11).Take(wordlength));
            array[array.Length - 1] = wordz;
            Lyrics.text = string.Join(" ", array);
        }
        subarray1 = array.Skip(1).Take(array.Length - 1).ToArray();
        var lyric = $"<color=red>{word}</color> " + string.Join(" ", subarray1);
        Lyrics.text = lyric;
        yield return new WaitForSeconds(waitTime[0]);
        for (int i = 1; i < array.Length - 1; i++)
        {
            subarray1 = array.Skip(0).Take(i).ToArray();
            word = array[i];
            subarray2 = array.Skip(i+1).Take(array.Length - i).ToArray();
            lyric = string.Join(" ", subarray1) + $" <color=red>{word}</color> " + string.Join(" ", subarray2);
            Lyrics.text = lyric;
            yield return new WaitForSeconds(waitTime[i]);
        }
        subarray1 = array.Skip(0).Take(array.Length - 1).ToArray();
        word = array[array.Length - 1];
        subarray2 = array.Skip(array.Length).Take(array.Length - array.Length - 1).ToArray();
        lyric = string.Join(" ", subarray1) + $" <color=red>{word}</color>" + string.Join(" ", subarray2);
        Lyrics.text = lyric;
    }

    public void Correct()
    {
        Lyrics.text = replacement_text;
    }

}
