using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace NUnitHelper
{
    public class NUnitTestDataManager
    {

        #region Singleton Impelmentation
        private static NUnitTestDataManager nUnitTestDataManager = null;
        private NUnitTestDataManager()
        {

        }
        
        public static NUnitTestDataManager GetInstance()
        {
            if (nUnitTestDataManager == null)
            {
                nUnitTestDataManager = new NUnitTestDataManager();
            }
            return nUnitTestDataManager;
        }
        #endregion
       
        private System.Collections.Specialized.OrderedDictionary orderedTestData;
        private System.Collections.Specialized.OrderedDictionary orderedTestDataDescription;
        /// <summary>
        /// Loads the test data set into the memory
        /// </summary>
        /// <param name="fileName">takes just the file name of the data xml e.g. TestData.config if kept under bin else should be full path</param>
        /// <returns></returns>
        public bool LoadTestData(string fileName)
        {
            try
            {
                DataSet ds = new DataSet();
                ds.ReadXml(fileName);
                DataTable testData = ds.Tables[0];                
                orderedTestData = new System.Collections.Specialized.OrderedDictionary();
                orderedTestDataDescription = new System.Collections.Specialized.OrderedDictionary();
                foreach(DataRow row in testData.Rows)
                {
                 orderedTestData.Add(row[0],row[1]);
                 orderedTestDataDescription.Add(row[0], row[2]);
                }
                testData.Dispose();
                testData = null;
                return true;
            }
            catch 
            {
                return false;
            }
        }
        /// <summary>
        /// Gets the integer value of the variable name (should exist in the data)
        /// </summary>
        /// <param name="variableName"></param>
        /// <returns></returns>
        public int GetIntValue(string variableName)
        {
            return Convert.ToInt32(orderedTestData[variableName]);
        }
        /// <summary>
        /// Gets the string value of the variable name (should exist in the data)
        /// </summary>
        /// <param name="variableName"></param>
        /// <returns></returns>
        public string GetStringValue(string variableName)
        {
            return Convert.ToString(orderedTestData[variableName]);
        }
        /// <summary>
        /// Returns the Description of the field
        /// </summary>
        /// <param name="variableName"></param>
        /// <returns></returns>
        public string GetVariableDescription(string variableName)
        {
            return Convert.ToString(orderedTestDataDescription[variableName]);
        }
        /// <summary>
        /// Gets the '|' seperated values and parses each as an item list of string
        /// </summary>
        /// <param name="variableName"></param>
        /// <returns></returns>
        public List<string> GetStringListValues(string variableName)
        {
            List<string> stringInputs = new List<string>();
            string[] parsedStrings = orderedTestData[variableName].ToString().Split('|');
            foreach (string parsedString in parsedStrings)
            {
                stringInputs.Add(Convert.ToString(parsedString));
            }
            return stringInputs;
        }
        /// <summary>
        /// Gets the '|' seperated values and parses each as an item list of int
        /// </summary>
        /// <param name="variableName"></param>
        /// <returns></returns>
        public List<int> GetIntListValues(string variableName)
        {
            List<int> integers = new List<int>();
            string[] parsedStrings = orderedTestData[variableName].ToString().Split('|');
            foreach (string parsedString in parsedStrings)
            {
                integers.Add(Convert.ToInt32(parsedString));
            }            
            return integers;
        }
        /// <summary>
        /// Gets the '|' separated list of Descriptions/Test Case names for the variable
        /// </summary>
        /// <param name="variableName"></param>
        /// <returns></returns>
        public List<string> GetVariableListDescriptions(string variableName)
        {
            List<string> descriptions = new List<string>();
            string[] parsedStrings = orderedTestDataDescription[variableName].ToString().Split('|');
            foreach (string parsedString in parsedStrings)
            {
                descriptions.Add(Convert.ToString(parsedString));
            }
            return descriptions;
        }
    }
}
