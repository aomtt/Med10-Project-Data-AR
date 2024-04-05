using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CsvHelper;
using System.IO;
using System.Globalization;
using System.Data;

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
    public int runOnce = 0;
    public DataTable iris_dt;
    public DataTable redwine_dt;
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
            Debug.Log(row[newColumnName]);
        }
    }
}
