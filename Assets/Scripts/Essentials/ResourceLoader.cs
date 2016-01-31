using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System;

public class ResourceLoader : MonoBehaviour
{
    private static ResourceLoader instance;

    public static ResourceLoader _instance
    {
        get { return instance ?? (instance = (new GameObject("ResourceLoader").AddComponent<ResourceLoader>())); }
    }

    // Keeps track of the loaded assets
    public Dictionary<string, object> loadedAssets = new Dictionary<string, object>();
    // Initial finds
    public Dictionary<string, string> foundPath = new Dictionary<string, string>();

    // Since we're a singleton we do this extra check
    private bool hasInstantiated = false;

    private void Awake()
    {
        // If we haven't initialized, do so
        if (!hasInstantiated)
            Initialize();
    }

    private void Initialize()
    {
        if (Application.isEditor)
        {
            // Check/Create the correct folder & text file
            CheckForFolder();

            // Look for all files in the folder and get the path
            CheckFolderRecursively();

            // Save all found paths in the text asset
            StreamWriter file = new StreamWriter(Application.dataPath + "/Resources/ESSENTIALS/ResourceLoader_Paths.txt", false);

            foreach (KeyValuePair<string, string> value in foundPath)
                file.WriteLine("Name:" + value.Key + ", Path:" + value.Value + ".");

            file.Close();

            // Make sure we reset the dictionairy
            foundPath.Clear();
        }

        // Load file & get the correct lines
        TextAsset text = Resources.Load("ESSENTIALS/ResourceLoader_Paths") as TextAsset;
        string[] lines = text.text.Split('\r', '\n');

        // Loop through the lines, and add these to the dict
        foreach (string line in lines)
            if (line != "")
                AddFileToDict(line.Split(':', ',')[1], line.Split(':', '.')[2]);

        hasInstantiated = true;
    }

    private void CheckForFolder()
    {
        // If the directory does not exist, we create it
        if (!Directory.Exists("Assets/Resources/ESSENTIALS"))
            Directory.CreateDirectory("Assets/Resources/ESSENTIALS");
    }

    private void CheckFolderRecursively(string folderName = "")
    {
        // Find all the assets in the folder, and add these to the list
        FileInfo[] assetInfo = new DirectoryInfo("Assets/Resources/" + folderName).GetFiles("*.*");

        // Loop through all found assets, and add these to the list
        foreach (FileInfo file in assetInfo)
            // Make sure we don't get duplicate files
            if (file.Extension != ".meta")
                // Get the file data and add this to the list
                AddFileToDict(Path.GetFileNameWithoutExtension(file.Name), folderName);

        // Get all the folders from the current folder
        DirectoryInfo[] fileInfo = new DirectoryInfo("Assets/Resources/" + folderName).GetDirectories();

        // Loop through all folders in the current folder, and call these as well
        foreach (DirectoryInfo file in fileInfo)
            CheckFolderRecursively(folderName + file.Name + "/");
    }

    private void AddFileToDict(string foundName, string foundPathLoc)
    {
        // Add the files to the dictionairy
        foundPath.Add(foundName, foundPathLoc);
    }

    public T GetAsset<T>(string name)
    {
        // Make sure we've already instantiated
        if (!hasInstantiated)
            Initialize();

        // Check if assets exist
        if (!loadedAssets.ContainsKey(name))
            LoadAsset<T>(name);

        // Return the asset
        return (T)loadedAssets[name];
    }

    private void LoadAsset<T>(string name)
    {
        // Load asset
        object newObject = Resources.Load(foundPath[name] + name, typeof(T));

        // Add it to the dict if it's not null
        if (newObject != null)
            loadedAssets.Add(name, newObject);
    }
}