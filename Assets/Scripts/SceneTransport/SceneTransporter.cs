using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneTransporter : MonoBehaviour
{
    protected static SceneTransporter instance;
    protected bool m_Transitioning;

    public GameObject LoadingUI;

    public static SceneTransporter Instance
    {
        get
        {
            if(instance != null)
            {
                return instance;
            }

            instance = FindObjectOfType<SceneTransporter>();

            if(instance != null)
            {
                return instance;
            }

            //Create();

            return instance;
        }
    }

    public static bool Transitioning
    {
        get
        {
            return Instance.m_Transitioning;
        }
    }

    public static SceneTransporter Create()
    {
        GameObject sceneControllerGameObject = new GameObject("SceneController");
        instance = sceneControllerGameObject.AddComponent<SceneTransporter>();

        return instance;
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if(Instance != this)
        {
            Destroy(gameObject);
            return;
        }


    }

    public static IEnumerator LoadScene(int SceneNum)
    {
        SceneManager.LoadScene(5);

        yield return new WaitForSeconds(3.0f);
        AsyncOperation async = SceneManager.LoadSceneAsync(SceneNum, LoadSceneMode.Single);

        while(!async.isDone)
        {
            yield return null;
        }
    }
}
