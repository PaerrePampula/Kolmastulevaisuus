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
    // Start is called before the first frame update
    void Start()
    {
        level = "store";
        SceneManager.LoadSceneAsync(level, LoadSceneMode.Additive);
        if (PlayerDataHolder.PlayerJob == null)
        {
            dynamicLocationObject = null;
            return;
        }

    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    void onNewJob(JobNotice jobNotice)
    {
        SceneManager.UnloadSceneAsync(level);
        level = jobNotice.scriptable.jobSiteScene;
        SceneManager.LoadSceneAsync(level, LoadSceneMode.Additive);
    }
}
