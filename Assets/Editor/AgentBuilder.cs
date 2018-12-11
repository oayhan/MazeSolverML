using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class AgentBuilder
{
    private const string ExecutableName = "mazetest.exe";
    private const string SceneName = "mazetest";
    private const string ScenePath = "Assets/Scenes/mazetest.unity";
    private const string BuildPath = "../python/";

    [MenuItem("ML/BuildForTraining")]
    public static void BuildForTraining()
    {
        Scene scene = SceneManager.GetSceneByName(SceneName);
        GameObject[] gameObjects = scene.GetRootGameObjects();
        GameObject academyObject = gameObjects.FirstOrDefault(g => g.name == "Academy");
        if (academyObject != null)
        {
            Brain brain = academyObject.GetComponentInChildren<Brain>();
            brain.brainType = BrainType.External;
        }
        else
        {
            Debug.LogError("Brain not found on current scene!");
        }

        string[] scenes = new[] { ScenePath };
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneWindows64);
        BuildPipeline.BuildPlayer(scenes, BuildPath + ExecutableName, BuildTarget.StandaloneWindows, BuildOptions.None);
    }

    [MenuItem("ML/SwitchToInternal")]
    public static void SwitchToInternalBrain()
    {
        Scene scene = SceneManager.GetSceneByName(SceneName);
        GameObject[] gameObjects = scene.GetRootGameObjects();
        GameObject academyObject = gameObjects.FirstOrDefault(g => g.name == "Academy");
        if (academyObject != null)
        {
            Brain brain = academyObject.GetComponentInChildren<Brain>();
            brain.brainType = BrainType.Internal;

            //todo: assign selected .bytes object to internal brain
            //GameObject selectedObject = Selection.activeGameObject;
            //if (selectedObject != null)
            //{
                
            //}
        }
        else
        {
            Debug.LogError("Brain not found on current scene!");
        }
    }
}
