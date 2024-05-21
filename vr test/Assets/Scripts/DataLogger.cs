using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataLogger : MonoBehaviour
{
    public bool isLogging = false;
    private List<string> logData = new List<string>();
    private char separator;
    private string filenameBase;
    private string[] header;


    public void StartLogging(char separator, string filenameBase, string[] header)
    {
        if (!isLogging)
        {
            this.separator = separator;
            this.filenameBase = filenameBase;
            this.header = header;
            isLogging = true;
            logData.Clear();
        }
    }

    public void Log(int UId, int trialID, int dumbellID, int dumbellWeight, int gripID, string answer)
    {
        if (isLogging)
        {
            string logEntry = $"{UId}{separator}{trialID}{separator}{dumbellID}{separator}{dumbellWeight}{separator}{gripID}{separator}{answer}";

            logData.Add(logEntry);
        }
    }

    public void StopLogging()
    {
        if (isLogging)
        {
            isLogging = false;
            string filepath = Path.Combine(Application.persistentDataPath , filenameBase + System.DateTime.Now.ToString("ddMMyyyy-HHmmss") + ".csv");
            Debug.Log("File Path: " + filepath);

            using (StreamWriter writer = new StreamWriter(filepath))
            {
                writer.WriteLine(string.Join(separator.ToString(), header));

                foreach (string entry in logData)
                {
                    writer.WriteLine(entry);
                }
            }
        }
    }
}
