using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePersist : MonoBehaviour
{
    [SerializeField] int startingSceneIndex;


    void Awake()
    {
        int scenePersistCount = FindObjectsOfType<ScenePersist>().Length;

        if (scenePersistCount>1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        startingSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (startingSceneIndex != currentSceneIndex)
        {
            Destroy(gameObject);
        }
    }
}
