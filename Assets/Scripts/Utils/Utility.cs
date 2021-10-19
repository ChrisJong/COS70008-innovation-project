namespace Utils
{
    using System.Collections.Generic;

    using UnityEngine;
    using UnityEngine.SceneManagement;

    public static class Utility
    {
        private static List<string> _sceneList = new List<string>();

        public static void ChangeScene(string sceneName)
        {
            bool sceneFound = false;

            if (_sceneList.Count == 0)
            {
                for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
                {
                    string path = System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));
                    string scene = path.Substring(path.LastIndexOf('/') + 1);

                    //Debug.Log(scene);

                    if (scene.Length != 0)
                        _sceneList.Add(scene);

                    if (path.Contains(sceneName))
                        sceneFound = true;
                }
            }
            else if (_sceneList.Contains(sceneName))
                sceneFound = true;

            if (!sceneFound)
            {
                Debug.LogError("No Scene With The Name: " + sceneName + " Found!");
                return;
            }

            SceneManager.LoadScene(sceneName);
        }

        public static string GetSceneLetterName()
        {
            string temp = SceneManager.GetActiveScene().name.Split(char.Parse("_"))[0];
            return temp;
        }

        public static void PlayOneShot(AudioClip audioClip)
        {
            if (audioClip != null)
                AudioSource.PlayClipAtPoint(audioClip, Vector3.zero);
            else
                Debug.LogError("No Audio Clip Found!");
        }

        public static void PlayOneShot(AudioClip audioClip, float volume = 1.0f)
        {
            if (audioClip != null)
                AudioSource.PlayClipAtPoint(audioClip, Vector3.zero, volume);
            else
                Debug.LogError("No Audio Clip Found!");
        }

        public static Texture2D ToTexture2D(this RenderTexture renderTexture)
        {
            Texture2D texture2D = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
            RenderTexture oldRT = RenderTexture.active;
            RenderTexture.active = renderTexture;

            texture2D.ReadPixels(new Rect(0.0f, 0.0f, renderTexture.width, renderTexture.height), 0, 0);
            texture2D.Apply();

            RenderTexture.active = oldRT;

            return texture2D;
        }
    }
}