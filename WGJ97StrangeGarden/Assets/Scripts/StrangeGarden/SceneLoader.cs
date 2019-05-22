using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "StrangeGarden/SceneLoader", fileName = "NewSceneLoader")]
public class SceneLoader : ScriptableObject
{
    [SerializeField]
    string sceneToLoad;

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
