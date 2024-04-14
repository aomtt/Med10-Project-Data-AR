using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CsvHelper;
using System.IO;
using System.Globalization;
using System.Data;
using TMPro;

// !!! NOTE !!! Du skal søge efter kolumnens navn (f.eks quality) efterfulgt af '_normalized' da det er disse værdier som befinder sig mellem 0-1 (floats)

// For Stefan:
// How to loop over a specific column and all its rows
/*
foreach (DataRow row in redwine_dt.Rows)
{
    // Loop over rows (values) by using column name
    //Debug.Log(row["fixed acidity_normalized"].ToString());
    //Debug.Log(row["sulphates_normalized"].ToString());
    //Debug.Log(row["alcohol_normalized"].ToString());
    Debug.Log(row["quality_normalized"].ToString());
}
*/


// For Stefan:
// How to access a specific row in a specific column (variable)
// DataTables in C# are accessed firsly by their row, hence this is nesscecary when accessing single values. Ved ikke om vi kommer til at bruge det.
/*
DataRow specificRow = redwine_dt.Rows[27]; // Access the 28th row (index 27)
var value = specificRow["quality_normalized"]; // Then access the specific column in that row
Debug.Log(value.ToString());
*/


public class Data : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown scaledropdown;
    [SerializeField] private TMP_Dropdown meshdropdown;
    [SerializeField] private GameObject[] dataHelperLabels;
    
    [SerializeField] private TMP_Text xMax;
    [SerializeField] private TMP_Text yMax;
    [SerializeField] private TMP_Text zMax;
    [SerializeField] private TMP_Text xMin;
    [SerializeField] private TMP_Text yMin;
    [SerializeField] private TMP_Text zMin;

    public int runOnce = 0;
    public DataTable iris_dt;
    public DataTable redwine_dt;

    [SerializeField] private GameObject dataPointPrefab;
    // Start is called before the first frame update
    void Start()
    {
        // Loading the IRIS dataset
        LoadIrisDataset();
        // Loading the red wine dataset
        LoadWineQualityDataset();
    }

    void Update()
    {
        /*
        if (runOnce == 0)
        {
            foreach (DataRow row in redwine_dt.Rows)
            {
                // Loop over rows (values) by using column name
                //Debug.Log(row["fixed acidity_normalized"].ToString());
                //Debug.Log(row["sulphates_normalized"].ToString());
                //Debug.Log(row["alcohol_normalized"].ToString());
                Debug.Log(row["quality_normalized"].ToString());
            }

            foreach (DataRow row in iris_dt.Rows)
            {
                // Loop over rows (values) by using column name
                //Debug.Log(row["fixed acidity_normalized"].ToString());
                //Debug.Log(row["sulphates_normalized"].ToString());
                //Debug.Log(row["alcohol_normalized"].ToString());
                Debug.Log(row["SepalLengthCm_normalized"].ToString());
            }
        }
        */
    }

    void LoadIrisDataset()
    {
        using (var reader = new StreamReader(Application.dataPath + "/Datasets/Iris.csv"))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            // Do any configuration to `CsvReader` before creating CsvDataReader.
            using (var dr = new CsvDataReader(csv))
            {
                iris_dt = new DataTable();
                iris_dt.Columns.Add("Id", typeof(int));
                iris_dt.Columns.Add("SepalLengthCm", typeof(float));
                iris_dt.Columns.Add("SepalWidthCm", typeof(float));
                iris_dt.Columns.Add("PetalLengthCm", typeof(float));
                iris_dt.Columns.Add("PetalWidthCm", typeof(float));
                iris_dt.Columns.Add("Species", typeof(string));

                iris_dt.Load(dr);

                NormalizeColumn(iris_dt, "SepalLengthCm");
                NormalizeColumn(iris_dt, "SepalWidthCm");
                NormalizeColumn(iris_dt, "PetalLengthCm");
                NormalizeColumn(iris_dt, "PetalWidthCm");
            }
        }
    }

    void LoadWineQualityDataset()
    {
        using (var reader = new StreamReader(Application.dataPath + "/Datasets/winequality-red.csv"))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            // Do any configuration to `CsvReader` before creating CsvDataReader.
            using (var dr = new CsvDataReader(csv))
            {
                redwine_dt = new DataTable();
                redwine_dt.Columns.Add("fixed acidity", typeof(float));
                redwine_dt.Columns.Add("volatile acidity", typeof(float));
                redwine_dt.Columns.Add("citric acid", typeof(float));
                redwine_dt.Columns.Add("residual sugar", typeof(float));
                redwine_dt.Columns.Add("chlorides", typeof(float));
                redwine_dt.Columns.Add("free sulfur dioxide", typeof(float));
                redwine_dt.Columns.Add("total sulfur dioxide", typeof(float));
                redwine_dt.Columns.Add("density", typeof(float));
                redwine_dt.Columns.Add("pH", typeof(float));
                redwine_dt.Columns.Add("sulphates", typeof(float));
                redwine_dt.Columns.Add("alcohol", typeof(float));
                redwine_dt.Columns.Add("quality", typeof(int));

                redwine_dt.Load(dr);
                
                NormalizeColumn(redwine_dt, "fixed acidity");
                NormalizeColumn(redwine_dt, "volatile acidity");
                NormalizeColumn(redwine_dt, "citric acid");
                NormalizeColumn(redwine_dt, "residual sugar");
                NormalizeColumn(redwine_dt, "chlorides");
                NormalizeColumn(redwine_dt, "free sulfur dioxide");
                NormalizeColumn(redwine_dt, "total sulfur dioxide");
                NormalizeColumn(redwine_dt, "density");
                NormalizeColumn(redwine_dt, "pH");
                NormalizeColumn(redwine_dt, "sulphates");
                NormalizeColumn(redwine_dt, "alcohol");
                NormalizeColumn(redwine_dt, "quality");
            }
        }
    }

    void NormalizeColumn(DataTable dtz, string columnName)
    {
        // Check if the new column already exists, if not, add it
        string newColumnName = columnName + "_normalized";
        if (!dtz.Columns.Contains(newColumnName))
        {
            dtz.Columns.Add(newColumnName, typeof(float));
        }

        float minValue = float.MaxValue;
        float maxValue = float.MinValue;

        // Find the min and max values
        foreach (DataRow row in dtz.Rows)
        {
            float value = Convert.ToSingle(row[columnName]);
            if (value < minValue) minValue = value;
            if (value > maxValue) maxValue = value;
        }

        // Normalize the column values
        foreach (DataRow row in dtz.Rows)
        {
            
            float value = Convert.ToSingle(row[columnName]);
            float normalizedValue = (value - minValue) / (maxValue - minValue);
            row[newColumnName] = normalizedValue;
            //Debug.Log(row[newColumnName]);
        }
    }

    public void InstantiateIrisData1()
    {
        DestroyAllPrefabs();
        EnableHelper(0);

        
        if (iris_dt == null) return;
        foreach (DataRow row in iris_dt.Rows)
        {
            GameObject prefab = Instantiate(dataPointPrefab, gameObject.transform);
            prefab.transform.localPosition = new Vector3(Convert.ToSingle(row["PetalLengthCm_normalized"]), Convert.ToSingle(row["PetalWidthCm_normalized"]), Convert.ToSingle(row["SepalLengthCm_normalized"]));
            //prefab.GetComponent<Renderer>().material.color = new Color(1-Convert.ToSingle(row["quality_normalized"]),Convert.ToSingle(row["quality_normalized"]),0.0f);
            prefab.GetComponent<DataPointScript>().dataDescriptionText = $"Sepal Length(cm): {row["SepalLengthCm"]}\nSepal Width(cm): {row["SepalWidthCm"]}\nPetal Length(cm): {row["PetalLengthCm"]}\nPetal Width(cm): {row["PetalWidthCm"]}\nSpecies: {row["Species"]}";
            switch (row["Species"].ToString())
            {
                case "Iris-versicolor":
                    prefab.GetComponent<Renderer>().material.color = new Color(1,0,0);
                    break;
                case "Iris-setosa":
                    prefab.GetComponent<Renderer>().material.color = new Color(0,1,0);
                    break;
                case "Iris-virginica":
                    prefab.GetComponent<Renderer>().material.color = new Color(0,0,1);
                    break;
                default:
                    prefab.GetComponent<Renderer>().material.color = new Color(1,1,1);
                    break;
            }

            prefab.GetComponent<FixedScaleScript>().Rescale(0.02f);
        }
        AssignLabelText(xMin, xMax, iris_dt, "PetalLengthCm");
        AssignLabelText(yMin, yMax, iris_dt, "PetalWidthCm");
        AssignLabelText(zMin, zMax, iris_dt, "SepalLengthCm");
    }
    
    public void InstantiateIrisData2()
    {
        DestroyAllPrefabs();
        EnableHelper(0);

        
        if (iris_dt == null) return;
        foreach (DataRow row in iris_dt.Rows)
        {
            GameObject prefab = Instantiate(dataPointPrefab, gameObject.transform);
            prefab.transform.localPosition = new Vector3(Convert.ToSingle(row["SepalWidthCm_normalized"]), Convert.ToSingle(row["SepalLengthCm_normalized"]), Convert.ToSingle(row["PetalWidthCm_normalized"]));
            //prefab.GetComponent<Renderer>().material.color = new Color(1-Convert.ToSingle(row["quality_normalized"]),Convert.ToSingle(row["quality_normalized"]),0.0f);
            prefab.GetComponent<DataPointScript>().dataDescriptionText = $"Sepal Length(cm): {row["SepalLengthCm"]}\nSepal Width(cm): {row["SepalWidthCm"]}\nPetal Length(cm): {row["PetalLengthCm"]}\nPetal Width(cm): {row["PetalWidthCm"]}\nSpecies: {row["Species"]}";
            switch (row["Species"].ToString())
            {
                case "Iris-versicolor":
                    prefab.GetComponent<Renderer>().material.color = new Color(1,0,0);
                    break;
                case "Iris-setosa":
                    prefab.GetComponent<Renderer>().material.color = new Color(0,1,0);
                    break;
                case "Iris-virginica":
                    prefab.GetComponent<Renderer>().material.color = new Color(0,0,1);
                    break;
                default:
                    prefab.GetComponent<Renderer>().material.color = new Color(1,1,1);
                    break;
            }

            prefab.GetComponent<FixedScaleScript>().Rescale(0.02f);
        }
        AssignLabelText(xMin, xMax, iris_dt, "SepalWidthCm");
        AssignLabelText(yMin, yMax, iris_dt, "SepalLengthCm");
        AssignLabelText(zMin, zMax, iris_dt, "PetalWidthCm");
    }
    
    public void InstantiateIrisData3()
    {
        DestroyAllPrefabs();
        EnableHelper(0);

        
        if (iris_dt == null) return;
        foreach (DataRow row in iris_dt.Rows)
        {
            GameObject prefab = Instantiate(dataPointPrefab, gameObject.transform);
            prefab.transform.localPosition = new Vector3(Convert.ToSingle(row["SepalLengthCm_normalized"]), Convert.ToSingle(row["PetalLengthCm_normalized"]), Convert.ToSingle(row["PetalWidthCm_normalized"]));
            //prefab.GetComponent<Renderer>().material.color = new Color(1-Convert.ToSingle(row["quality_normalized"]),Convert.ToSingle(row["quality_normalized"]),0.0f);
            prefab.GetComponent<DataPointScript>().dataDescriptionText = $"Sepal Length(cm): {row["SepalLengthCm"]}\nSepal Width(cm): {row["SepalWidthCm"]}\nPetal Length(cm): {row["PetalLengthCm"]}\nPetal Width(cm): {row["PetalWidthCm"]}\nSpecies: {row["Species"]}";
            switch (row["Species"].ToString())
            {
                case "Iris-versicolor":
                    prefab.GetComponent<Renderer>().material.color = new Color(1,0,0);
                    break;
                case "Iris-setosa":
                    prefab.GetComponent<Renderer>().material.color = new Color(0,1,0);
                    break;
                case "Iris-virginica":
                    prefab.GetComponent<Renderer>().material.color = new Color(0,0,1);
                    break;
                default:
                    prefab.GetComponent<Renderer>().material.color = new Color(1,1,1);
                    break;
            }

            prefab.GetComponent<FixedScaleScript>().Rescale(0.02f);
        }
        AssignLabelText(xMin, xMax, iris_dt, "SepalLengthCm");
        AssignLabelText(yMin, yMax, iris_dt, "PetalLengthCm");
        AssignLabelText(zMin, zMax, iris_dt, "PetalWidthCm");
    }
    
    public void InstantiateWineData1()
    {
        DestroyAllPrefabs();
        EnableHelper(1);
        if (redwine_dt == null) return;
        foreach (DataRow row in redwine_dt.Rows)
        {
            GameObject prefab = Instantiate(dataPointPrefab, gameObject.transform);
            prefab.transform.localPosition = new Vector3(Convert.ToSingle(row["alcohol_normalized"]), Convert.ToSingle(row["sulphates_normalized"]), Convert.ToSingle(row["pH_normalized"]));
            prefab.GetComponent<Renderer>().material.color = new Color(1-Convert.ToSingle(row["quality_normalized"]),Convert.ToSingle(row["quality_normalized"]),0.0f);
            prefab.GetComponent<DataPointScript>().dataDescriptionText = $"Residual Sugar: {row["residual sugar"]}\nSulphates: {row["sulphates"]}\nPH: {row["pH"]}\nDensity: {row["density"]}\nAlcohol: {row["alcohol"]}\nQuality: {row["quality"]}";
            prefab.GetComponent<FixedScaleScript>().Rescale(0.01f);
        }
        AssignLabelText(xMin, xMax, redwine_dt, "alcohol");
        AssignLabelText(yMin, yMax, redwine_dt, "sulphates");
        AssignLabelText(zMin, zMax, redwine_dt, "pH");
    }
    
    public void InstantiateWineData2()
    {
        DestroyAllPrefabs();
        EnableHelper(1);
        if (redwine_dt == null) return;
        foreach (DataRow row in redwine_dt.Rows)
        {
            GameObject prefab = Instantiate(dataPointPrefab, gameObject.transform);
            prefab.transform.localPosition = new Vector3(Convert.ToSingle(row["residual sugar_normalized"]), Convert.ToSingle(row["sulphates_normalized"]), Convert.ToSingle(row["alcohol_normalized"]));
            prefab.GetComponent<Renderer>().material.color = new Color(1-Convert.ToSingle(row["quality_normalized"]),Convert.ToSingle(row["quality_normalized"]),0.0f);
            prefab.GetComponent<DataPointScript>().dataDescriptionText = $"Residual Sugar: {row["residual sugar"]}\nSulphates: {row["sulphates"]}\nPH: {row["pH"]}\nDensity: {row["density"]}\nAlcohol: {row["alcohol"]}\nQuality: {row["quality"]}";
            prefab.GetComponent<FixedScaleScript>().Rescale(0.01f);
        }
        AssignLabelText(xMin, xMax, redwine_dt, "residual sugar");
        AssignLabelText(yMin, yMax, redwine_dt, "sulphates");
        AssignLabelText(zMin, zMax, redwine_dt, "alcohol");
    }
    
    

    public void DestroyAllPrefabs()
    {
        var prefabList = GameObject.FindGameObjectsWithTag("DataPoint");
        foreach (var datapoint in prefabList){
            Destroy(datapoint);
        }
    }

    public void SetScale()
    {
        float scale = 0.01f;
        int value = scaledropdown.value;
        switch (value)
        {
            case 0:
                scale = 0.0025f;
                break;
            case 1:
                scale = 0.005f;
                break;
            case 2:
                scale = 0.01f;
                break;
            case 3:
                scale = 0.02f;
                break;
            case 4:
                scale = 0.05f;
                break;
                
        }
        var prefabList = GameObject.FindGameObjectsWithTag("DataPoint");
        foreach (var datapoint in prefabList){
            datapoint.GetComponent<FixedScaleScript>().Rescale(scale);
        }
    }

    public void SetLine(bool toggle)
    {
        var prefabList = GameObject.FindGameObjectsWithTag("DataPoint");
        foreach (var datapoint in prefabList){
            datapoint.GetComponent<DataPointScript>().SetLine(toggle);
        }
    }

    public void SetMesh()
    {
        int mesh = meshdropdown.value;
        var prefabList = GameObject.FindGameObjectsWithTag("DataPoint");
        foreach (var datapoint in prefabList){
            datapoint.GetComponent<DataPointScript>().SetMesh(mesh);
        }
    }

    private void EnableHelper(int index)
    {
        foreach (GameObject label in dataHelperLabels)
        {
            label.SetActive(false);
        }
        dataHelperLabels[index].SetActive(true);
    }

    private void AssignLabelText(TMP_Text minLabel, TMP_Text maxLabel,DataTable dataTable, string columnName)
    {
        float minValue = float.MaxValue;
        float maxValue = float.MinValue;
        // Find the min and max values
        foreach (DataRow row in dataTable.Rows)
        {
            float value = Convert.ToSingle(row[columnName]);
            if (value < minValue) minValue = value;
            if (value > maxValue) maxValue = value;
        }

        minLabel.text = columnName+": "+minValue;
        maxLabel.text = columnName+": "+maxValue;
    }
}
