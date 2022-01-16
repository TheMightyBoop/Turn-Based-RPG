using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    #region Singleton
    public static LevelLoader instance;

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    #endregion

    public Animator transition;
    public float transitionTime = 1f;

    public void LoadLevel(string levelName)
    {
        StartCoroutine(LoadNamedLevel(levelName));
    }

    IEnumerator LoadNamedLevel(string levelName)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelName);
    }
}
