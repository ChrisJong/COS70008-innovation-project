namespace Utlis
{
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public static class Utility
    {
        public static void ChangeScene(string sceneName)
        {
            bool sceneFound = false;

            for(int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                string x = SceneUtility.GetScenePathByBuildIndex(i);

                if (x.Contains(sceneName))
                {
                    sceneFound = true;
                    break;
                }
            }

            if (!sceneFound)
            {
                Debug.LogError("No Scene With The Name: " + sceneName + " Found!");
                return;
            }

            SceneManager.LoadScene(sceneName);
        }
    }
}