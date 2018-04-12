using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class MyLuisManager : MonoBehaviour
{

    [System.Serializable] //this class represents the LUIS response 
    public class AnalysedQuery
    {
        public TopScoringIntentData topScoringIntent;
        public EntityData[] entities;
        public string query;
    }

    // This class contains the Intent LUIS determines  
    // to be the most likely 
    [System.Serializable]
    public class TopScoringIntentData
    {
        public string intent;
        public float score;
    }

    // This class contains data for an Entity 
    [System.Serializable]
    public class EntityData
    {
        public string entity;
        public string type;
        public int startIndex;
        public int endIndex;
        public float score;
    }

    public static MyLuisManager instance;

    //Substitute the value of luisEndpoint with your own End Point 
    string luisEndpoint = "https://eastasia.api.cognitive.microsoft.com/luis/v2.0/apps/d5e12620-52e9-47ee-9756-ae868756d119?subscription-key=a49960fa27484fd0bbf46489c9f1a749&verbose=true&timezoneOffset=480&q=";

    private void Awake()
    {
        // allows this class instance to behave like a singleton 
        instance = this;
    }

    /// <summary> 
    /// Call LUIS to submit a dictation result. 
    /// </summary> 
    public IEnumerator SubmitRequestToLuis(string dictationResult)
    {
        WWWForm webForm = new WWWForm();
        string queryString;
        queryString = string.Concat(Uri.EscapeDataString(dictationResult));
        using (UnityWebRequest unityWebRequest = UnityWebRequest.Get(luisEndpoint + queryString))
        {
            unityWebRequest.downloadHandler = new DownloadHandlerBuffer();
            yield return unityWebRequest.SendWebRequest();
            long responseCode = unityWebRequest.responseCode;
            try
            {
                using (Stream stream =
                GenerateStreamFromString(unityWebRequest.downloadHandler.text))
                {
                    StreamReader reader = new StreamReader(stream);
                    AnalysedQuery analysedQuery = new AnalysedQuery();
                    analysedQuery =
                    JsonUtility.FromJson<AnalysedQuery>(unityWebRequest.downloadHandler.text);

                    //analyse the elements of the response  
                    AnalyseResponseElements(analysedQuery);
                }
            }
            catch (Exception exception)
            {
                Debug.Log("Luis Request Exception Message: " + exception.Message);
            }
            yield return null;
        }
    }

    public static Stream GenerateStreamFromString(string receivedString)
    {
        MemoryStream stream = new MemoryStream();
        StreamWriter writer = new StreamWriter(stream);
        writer.Write(receivedString);
        writer.Flush();
        stream.Position = 0;
        return stream;
    }

    private void AnalyseResponseElements(AnalysedQuery aQuery)
    {
        string topIntent = aQuery.topScoringIntent.intent;

        // Create a dictionary of entities associated with their type 
        Dictionary<string, string> entityDic = new Dictionary<string, string>();

        foreach (EntityData ed in aQuery.entities)
        {
            entityDic.Add(ed.type, ed.entity);
        }

        // Depending on the topmost recognised intent, read the entities name 
        switch (aQuery.topScoringIntent.intent)
        {
            case "ToolIsReady":
                string toolName = null;
                foreach (var pair in entityDic)
                {
                    if (pair.Key == "Tool")
                    {
                        toolName = pair.Value;
                    }
                }
                Behaviours.instance.ToolReady(toolName);
                break;
        }
    }
}
