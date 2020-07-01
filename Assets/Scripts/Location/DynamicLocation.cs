using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DynamicLocation : EventLocation
{
    GameObject dynamicLocationObject;
    string level;
    private void OnEnable()
    {

        JobHandler.OnJobApply += onNewJob;

    }
    private void OnDisable()
    {
        level = "store";
        JobHandler.OnJobApply -= onNewJob;
    }
    // Start is called before the first frame update
    void Start()
    {
        level = "store";
        SceneManager.LoadSceneAsync(level, LoadSceneMode.Additive);
        if (PlayerDataHolder.Current.PlayerJob == null)
        {
            dynamicLocationObject = null;
            setLocation(FIRE_LOCATION.URBANAREA);
            return;
        }

    }

    void onNewJob(JobNotice jobNotice)
    {

        setLocation(FIRE_LOCATION.WORK);
        string newlevel = jobNotice.scriptable.jobSiteScene;
        StartCoroutine(reloadSceneAsync(level, newlevel));
    }
    IEnumerator reloadSceneAsync(string levelOriginal, string levelname)
    {
        AsyncOperation asyncLoad = SceneManager.UnloadSceneAsync(levelOriginal);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        SceneManager.LoadSceneAsync(levelname, LoadSceneMode.Additive);
        level = levelname;
    }



}
