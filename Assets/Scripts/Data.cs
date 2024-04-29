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
    [SerializeField] private GameObject[] dataDescriptions;
    
    [SerializeField] private TMP_Text[] xMax;
    [SerializeField] private TMP_Text[] yMax;
    [SerializeField] private TMP_Text[] zMax;
    [SerializeField] private TMP_Text[] xMin;
    [SerializeField] private TMP_Text[] yMin;
    [SerializeField] private TMP_Text[] zMin;
    [SerializeField] private TMP_Text[] xName;
    [SerializeField] private TMP_Text[] yName;
    [SerializeField] private TMP_Text[] zName;
    

    public int runOnce = 0;
    public DataTable iris_dt;
    public DataTable redwine_dt;
    public DataTable movies_dt;
    public DataTable heightweight_dt;
    public DataTable housing_dt;


    [SerializeField] private GameObject dataPointPrefab;
    // Start is called before the first frame update
    void Start()
    {
        // Loading the IRIS dataset
        LoadIrisDataset();
        // Loading the red wine dataset
        LoadWineQualityDataset();
        LoadMovieDataset();
        LoadHousingDataset();
        LoadHeightWeightDataset();
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
    void LoadHeightWeightDataset()
    {
        using (var reader = new StreamReader(Path.Combine(Application.streamingAssetsPath, "HeightWeight.csv")))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            // Do any configuration to `CsvReader` before creating CsvDataReader.
            using (var dr = new CsvDataReader(csv))
            {
                heightweight_dt = new DataTable();
                heightweight_dt.Columns.Add("Gender", typeof(string));
                heightweight_dt.Columns.Add("Height", typeof(int));
                heightweight_dt.Columns.Add("Weight", typeof(int));
                heightweight_dt.Columns.Add("Index", typeof(float));

                heightweight_dt.Load(dr);

                NormalizeColumn(heightweight_dt, "Height");
                NormalizeColumn(heightweight_dt, "Weight");
                NormalizeColumn(heightweight_dt, "Index");
            }
        }
 
        Debug.Log("HeightWeight data loaded");
    }
    
    void LoadHousingDataset()
    {
        using (var reader = new StreamReader(Path.Combine(Application.streamingAssetsPath, "Housing.csv")))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            // Do any configuration to `CsvReader` before creating CsvDataReader.
            using (var dr = new CsvDataReader(csv))
            {
                housing_dt = new DataTable();
                housing_dt.Columns.Add("id", typeof(float));
                housing_dt.Columns.Add("date", typeof(string));
                housing_dt.Columns.Add("price", typeof(float));
                housing_dt.Columns.Add("bedrooms", typeof(float));
                housing_dt.Columns.Add("bathrooms", typeof(float));
                housing_dt.Columns.Add("sqft_living", typeof(int));
                housing_dt.Columns.Add("sqft_lot", typeof(int));
                housing_dt.Columns.Add("floors", typeof(float));
                housing_dt.Columns.Add("waterfront", typeof(string));
                housing_dt.Columns.Add("view", typeof(int));
                housing_dt.Columns.Add("condition", typeof(int));
                housing_dt.Columns.Add("grade", typeof(int));
                housing_dt.Columns.Add("sqft_above", typeof(int));
                housing_dt.Columns.Add("sqft_basement", typeof(int));
                housing_dt.Columns.Add("yr_built", typeof(int));
                housing_dt.Columns.Add("yr_renovated", typeof(int));
                housing_dt.Columns.Add("zipcode", typeof(string));
                housing_dt.Columns.Add("lat", typeof(float));
                housing_dt.Columns.Add("long", typeof(float));
                housing_dt.Columns.Add("sqft_living15", typeof(float));
                housing_dt.Columns.Add("sqft_lot15", typeof(float));

                housing_dt.Load(dr);

                NormalizeColumn(housing_dt, "lat");
                NormalizeColumn(housing_dt, "long");
                NormalizeColumn(housing_dt, "price");
                NormalizeColumn(housing_dt, "condition");
                NormalizeColumn(housing_dt, "sqft_living");
                NormalizeColumn(housing_dt, "grade");
            }
        }
 
        Debug.Log("housing data loaded");
    }
    
    void LoadMovieDataset()
    {
        using (var reader = new StreamReader(Path.Combine(Application.streamingAssetsPath, "Movies.csv")))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            // Do any configuration to `CsvReader` before creating CsvDataReader.
            using (var dr = new CsvDataReader(csv))
            {
                movies_dt = new DataTable();
                movies_dt.Columns.Add("Release", typeof(string));
                movies_dt.Columns.Add("Opening", typeof(int));
                movies_dt.Columns.Add("Total Gross", typeof(int));
                movies_dt.Columns.Add("% of Total", typeof(float));
                movies_dt.Columns.Add("Theaters", typeof(int));
                movies_dt.Columns.Add("Average", typeof(int));
                movies_dt.Columns.Add("Date", typeof(string));
                movies_dt.Columns.Add("Distributor", typeof(string));

                movies_dt.Load(dr);

                NormalizeColumn(movies_dt, "Opening");
                NormalizeColumn(movies_dt, "Total Gross");
                NormalizeColumn(movies_dt, "% of Total");
                NormalizeColumn(movies_dt, "Theaters");
                NormalizeColumn(movies_dt, "Average");
                NormalizeColumnFromDate(movies_dt, "Date");
            }
        }
 
        Debug.Log("movie data loaded");
    }
    
    void LoadIrisDataset()
    {
        using (var reader = new StreamReader(Path.Combine(Application.streamingAssetsPath, "Iris.csv")))
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
 
        Debug.Log("iris data loaded");
    }

    void LoadWineQualityDataset()
    {
        using (var reader = new StreamReader(Path.Combine(Application.streamingAssetsPath, "winequality-red.csv")))
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
        Debug.Log("wine data loaded");
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
    
    void NormalizeColumnFromDate(DataTable dtz, string columnName)
    {
        // Check if the new column already exists, if not, add it
        string newColumnName = columnName + "_normalized";
        if (!dtz.Columns.Contains(newColumnName))
        {
            dtz.Columns.Add(newColumnName, typeof(float));
        }

        DateTime minValue = DateTime.MaxValue;
        DateTime maxValue = DateTime.MinValue;

        // Find the min and max values
        foreach (DataRow row in dtz.Rows)
        {
            DateTime value = DateTime.ParseExact(row[columnName].ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
            if (value < minValue) minValue = value;
            if (value > maxValue) maxValue = value;
        }

        // Normalize the column values
        foreach (DataRow row in dtz.Rows)
        {
            DateTime value = DateTime.ParseExact(row[columnName].ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
            float normalizedValue = (float)(value - minValue).TotalDays / (float)(maxValue - minValue).TotalDays;
            row[newColumnName] = normalizedValue;
        }
    }

    public void InstantiateIrisData1()
    {
        DestroyAllPrefabs();
        EnableHelper(1);
        EnableDescription(1);

        
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
                    prefab.GetComponent<Renderer>().material.color = new Color(1,1,0);
                    break;
                case "Iris-setosa":
                    prefab.GetComponent<Renderer>().material.color = new Color(0,1,1);
                    break;
                case "Iris-virginica":
                    prefab.GetComponent<Renderer>().material.color = new Color(1,0,1);
                    break;
                default:
                    prefab.GetComponent<Renderer>().material.color = new Color(1,1,1);
                    break;
            }

            prefab.GetComponent<FixedScaleScript>().Rescale(0.02f);
        }
        AssignLabelText(xMin[0], xMax[0], iris_dt, "PetalLengthCm", xName[0], "Petal Length(cm)");
        AssignLabelText(xMin[1], xMax[1], iris_dt, "PetalLengthCm", xName[1], "Petal Length(cm)");
        AssignLabelText(yMin[0], yMax[0], iris_dt, "PetalWidthCm", yName[0], "Petal Width(cm)");
        AssignLabelText(yMin[1], yMax[1], iris_dt, "PetalWidthCm", yName[1], "Petal Width(cm)");
        AssignLabelText(yMin[2], yMax[2], iris_dt, "PetalWidthCm", yName[2], "Petal Width(cm)");
        AssignLabelText(yMin[3], yMax[3], iris_dt, "PetalWidthCm", yName[3], "Petal Width(cm)");
        AssignLabelText(zMin[0], zMax[0], iris_dt, "SepalLengthCm", zName[0], "Sepal Length(cm)");
        AssignLabelText(zMin[1], zMax[1], iris_dt, "SepalLengthCm", zName[1], "Sepal Length(cm)");
    }
    
    public void InstantiateHousingData1()
    {
        DestroyAllPrefabs();
        EnableHelper(3);
        EnableDescription(3);

        int maxIterations = 2500;
        int iterations = 0;

        if (housing_dt == null) return;
        foreach (DataRow row in housing_dt.Rows)
        {
            if (iterations >= maxIterations) break;

            GameObject prefab = Instantiate(dataPointPrefab, gameObject.transform);
            prefab.transform.localPosition = new Vector3(Convert.ToSingle(row["sqft_living_normalized"]), Convert.ToSingle(row["price_normalized"]), Convert.ToSingle(row["grade_normalized"]));
            prefab.GetComponent<Renderer>().material.color = new Color(1, 1 - Convert.ToSingle(row["price_normalized"]), 0.0f);
            prefab.GetComponent<DataPointScript>().dataDescriptionText = $"Price(USD): {row["price"]}\nCondition(1-5): {row["condition"]}\nFloors: {row["floors"]}\nLiving Area sq ft: {row["sqft_living"]}\nGrade: {row["grade"]}";

            prefab.GetComponent<FixedScaleScript>().Rescale(0.02f);

            iterations++;
        }

        AssignLabelText(xMin[0], xMax[0], housing_dt, "sqft_living", xName[0], "Living Area(Square Feet)");
        AssignLabelText(xMin[1], xMax[1], housing_dt, "sqft_living", xName[1], "Living Area(Square Feet)");
        AssignLabelText(yMin[0], yMax[0], housing_dt, "price", yName[0], "Price(USD)");
        AssignLabelText(yMin[1], yMax[1], housing_dt, "price", yName[1], "Price(USD)");
        AssignLabelText(yMin[2], yMax[2], housing_dt, "price", yName[2], "Price(USD)");
        AssignLabelText(yMin[3], yMax[3], housing_dt, "price", yName[3], "Price(USD)");
        AssignLabelText(zMin[0], zMax[0], housing_dt, "grade", zName[0], "Grade");
        AssignLabelText(zMin[1], zMax[1], housing_dt, "grade", zName[1], "Grade");
    }
    
    public void InstantiateMovieData1()
    {
        DestroyAllPrefabs();
        EnableHelper(2);
        EnableDescription(2);

        
        if (movies_dt == null) return;
        foreach (DataRow row in movies_dt.Rows)
        {
            GameObject prefab = Instantiate(dataPointPrefab, gameObject.transform);
            prefab.transform.localPosition = new Vector3(Convert.ToSingle(row["Date_normalized"]), Convert.ToSingle(row["Total Gross_normalized"]), Convert.ToSingle(row["Opening_normalized"]));
            prefab.GetComponent<Renderer>().material.color = new Color(1,1-Convert.ToSingle(row["Total Gross_normalized"]),1-Convert.ToSingle(row["Total Gross_normalized"]));
            prefab.GetComponent<DataPointScript>().dataDescriptionText = $"Movie Title: {row["Release"]}\nOpening Gross: {row["Opening"]}\nTotal Gross: {row["Total Gross"]}\nDate: {row["Date"]}\nDistributor: {row["Distributor"]}";

            prefab.GetComponent<FixedScaleScript>().Rescale(0.02f);
        }
        AssignLabelText(xMin[0], xMax[0], movies_dt, "Date_normalized", xName[0],"1983", "2024", "Release Date");
        AssignLabelText(xMin[1], xMax[1], movies_dt, "Date_normalized", xName[1],"1983", "2024", "Release Date");
        AssignLabelText(yMin[0], yMax[0], movies_dt, "Total Gross", yName[0], "Total Gross(USD)");
        AssignLabelText(yMin[1], yMax[1], movies_dt, "Total Gross", yName[1], "Total Gross(USD)");
        AssignLabelText(yMin[2], yMax[2], movies_dt, "Total Gross", yName[2], "Total Gross(USD)");
        AssignLabelText(yMin[3], yMax[3], movies_dt, "Total Gross", yName[3], "Total Gross(USD)");
        AssignLabelText(zMin[0], zMax[0], movies_dt, "Opening", zName[0], "Opening Earnings(USD)");
        AssignLabelText(zMin[1], zMax[1], movies_dt, "Opening", zName[1], "Opening Earnings(USD)");
    }
    
    public void InstantiateHeightWeightData1()
    {
        DestroyAllPrefabs();
        EnableHelper(4);
        EnableDescription(4);
        if (heightweight_dt == null) return;
        foreach (DataRow row in heightweight_dt.Rows)
        {
            GameObject prefab = Instantiate(dataPointPrefab, gameObject.transform);
            prefab.transform.localPosition = new Vector3(Convert.ToSingle(row["Index_normalized"]), Convert.ToSingle(row["Height_normalized"]), Convert.ToSingle(row["Weight_normalized"]));
            //prefab.transform.localPosition = new Vector3(0.5f, Convert.ToSingle(row["Height_normalized"]), Convert.ToSingle(row["Weight_normalized"]));
            //prefab.GetComponent<Renderer>().material.color = new Color(1.0f,0.1f,0.5f);
            prefab.GetComponent<DataPointScript>().dataDescriptionText = $"Gender: {row["Gender"]}\nHeight: {row["Height"]}\nWeight: {row["Weight"]}\nBMI Categorization(1-5): {row["Index"]}";
            prefab.GetComponent<FixedScaleScript>().Rescale(0.02f);
            
            switch (row["Gender"].ToString())
            {
                case "Male":
                    prefab.GetComponent<Renderer>().material.color = new Color(0.1f,0.3f,0.8f);
                    break;
                case "Female":
                    prefab.GetComponent<Renderer>().material.color = new Color(1,0.1f,0.3f);
                    break;
                default:
                    prefab.GetComponent<Renderer>().material.color = new Color(1,1,1);
                    break;
            }
        }
        
        
        AssignLabelText(xMin[0], xMax[0], heightweight_dt, "Index", xName[0],"0\nUnderweight", "5\nExtremely Obese", "BMI Category");
        AssignLabelText(xMin[1], xMax[1], heightweight_dt, "Index", xName[1],"0\nUnderweight", "5\nExtremely Obese", "BMI Category");
        //AssignLabelText(xMin[0], xMax[0], heightweight_dt, "Index", xName[0],"", "", "N/A");
        //AssignLabelText(xMin[1], xMax[1], heightweight_dt, "Index", xName[1],"", "", "N/A");
        AssignLabelText(yMin[0], yMax[0], heightweight_dt, "Height", yName[0], "Height(cm)");
        AssignLabelText(yMin[1], yMax[1], heightweight_dt, "Height", yName[1], "Height(cm)");
        AssignLabelText(yMin[2], yMax[2], heightweight_dt, "Height", yName[2], "Height(cm)");
        AssignLabelText(yMin[3], yMax[3], heightweight_dt, "Height", yName[3], "Height(cm)");
        AssignLabelText(zMin[0], zMax[0], heightweight_dt, "Weight", zName[0], "Weight(Kg)");
        AssignLabelText(zMin[1], zMax[1], heightweight_dt, "Weight", zName[1], "Weight(Kg)");
    }
    
    public void InstantiateWineData2()
    {
        DestroyAllPrefabs();
        EnableHelper(5);
        EnableDescription(5);
        if (redwine_dt == null) return;
        foreach (DataRow row in redwine_dt.Rows)
        {
            GameObject prefab = Instantiate(dataPointPrefab, gameObject.transform);
            prefab.transform.localPosition = new Vector3(Convert.ToSingle(row["residual sugar_normalized"]), Convert.ToSingle(row["sulphates_normalized"]), Convert.ToSingle(row["alcohol_normalized"]));
            prefab.GetComponent<Renderer>().material.color = new Color(1-Convert.ToSingle(row["quality_normalized"]),Convert.ToSingle(row["quality_normalized"]),0.0f);
            prefab.GetComponent<DataPointScript>().dataDescriptionText = $"Residual Sugar: {row["residual sugar"]}\nSulphates: {row["sulphates"]}\nPH: {row["pH"]}\nDensity: {row["density"]}\nAlcohol: {row["alcohol"]}\nQuality: {row["quality"]}";
            prefab.GetComponent<FixedScaleScript>().Rescale(0.02f);
        }
        AssignLabelText(xMin[0], xMax[0], redwine_dt, "residual sugar", xName[0]);
        AssignLabelText(xMin[1], xMax[1], redwine_dt, "residual sugar", xName[1]);
        AssignLabelText(yMin[0], yMax[0], redwine_dt, "sulphates", yName[0]);
        AssignLabelText(yMin[1], yMax[1], redwine_dt, "sulphates", yName[1]);
        AssignLabelText(yMin[2], yMax[2], redwine_dt, "sulphates", yName[2]);
        AssignLabelText(yMin[3], yMax[3], redwine_dt, "sulphates", yName[3]);
        AssignLabelText(zMin[0], zMax[0], redwine_dt, "alcohol", zName[0]);
        AssignLabelText(zMin[1], zMax[1], redwine_dt, "alcohol", zName[1]);
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
    private void EnableDescription(int index)
    {
        foreach (GameObject label in dataDescriptions)
        {
            label.SetActive(false);
        }
        dataDescriptions[index].SetActive(true);
    }

    private void AssignLabelText(TMP_Text minLabel, TMP_Text maxLabel,DataTable dataTable, string columnName, TMP_Text nameLabel, string nameOverride = null)
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

        minLabel.text = minValue.ToString("#,##0.00");
        maxLabel.text = maxValue.ToString("#,##0.00");
        nameLabel.text = nameOverride ?? columnName;
    }
    
    private void AssignLabelText(TMP_Text minLabel, TMP_Text maxLabel,DataTable dataTable, string columnName, TMP_Text nameLabel, string minOverride, string maxOverride, string nameOverride = null)
    {
        minLabel.text = minOverride;
        maxLabel.text = maxOverride;
        nameLabel.text = nameOverride ?? columnName;
    }
}
