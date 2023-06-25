using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Ithas
{
    public class CsvToJson : MonoBehaviour
    {
        public string csvFilePath;
        public string jsonFilePath;

        private void Start()
        {
            string[] csv = File.ReadAllLines(csvFilePath); //reading from csv file
            List<Dictionary<string, string>> jsonData = new List<Dictionary<string, string>>(); //list to hold json data

            string[] headers = csv[0].Split(','); //header names

            for (int i = 1; i < csv.Length; i++) //loop all lines except header names so i = 1
            {
                string[] values = csv[i].Split(',');
                Dictionary<string, string> dataEntry = new Dictionary<string, string>(); //dictionary for holding data for this entry

                for (int j = 0; j < headers.Length; j++)
                {
                    dataEntry.Add(headers[j], values[j]); //adding to dictionary based on headers
                    //Debug.Log(dataEntry);
                }

                jsonData.Add(dataEntry); //add each data entry to json list
            }
            //Debug.Log(jsonData);
            string json = JsonUtility.ToJson(jsonData, true); //converting the json data to json string
            File.WriteAllText(jsonFilePath, json); //writing json string to a file
        }
    }
}
