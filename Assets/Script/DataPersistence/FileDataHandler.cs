// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using System;
// using System.IO;
// public class FileDataHandler : MonoBehaviour
// {

//     private string dataDirPath = "";

//     private string dataFileName="";

//     public FileDataHandler(string dataDirPath, string dataFileName)
//     {
//        this.dataDirPath=dataDirPath;
//        this.dataFileName=dataFileName;
//     }
//     public GameData Load()
//     {
//           string fullPath = Path.Combine(this.dataDirPath, this.dataFileName);
//           GameData loadedData=null;
//           if(File.Exists(fullPath))
//           {
//             try{
//                 //Load the serialized data from the file
//                 string dataToLoad ="";
//                 using(FileStream stream = new FileStream(fullPath, FileMode.Open))
//                 {
//                     using(StreamReader reader = new StreamReader(stream))
//                     {
//                         dataToLoad = reader.ReadToEnd();
//                     }
//                 }
//                 //deserialize the data from Json back into a C# object
//                 loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
//             }
//             catch(Exception e)
//             {
//                 Debug.LogError("Error occured when trying to load data from file: "+fullPath+"\n"+e);
//             }
//           }
//            return loadedData; // Thêm câu lệnh trả về ở đây
//     }
//     public void Save(GameData gameData)
//     {
//         //use Path.Combine to account for different OS's having different path separators
//         string fullPath = Path.Combine(this.dataDirPath, this.dataFileName);
//         try{
//             //create the directory the file will be written to if it doesnt already exist
//             Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
//             //serialize the C# game data object into JSON
//             string dataToStore= JsonUtility.ToJson(gameData,true);
//             using(FileStream stream = new FileStream(fullPath, FileMode.Create))
//             {
//                 using(StreamWriter writer = new StreamWriter(stream))
//                 {
//                     writer.Write(dataToStore);
//                 }
//             }
//         }catch(Exception e)
//         {
//             Debug.LogError("Error occured when trying to save data to File: "+fullPath+ "\n"+e);
//         }
//     }  
// }
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
public class FileDataHandler : MonoBehaviour
{

    private string dataDirPath = "";

    private string dataFileName="";

    private bool useEncryption = false;

    private readonly string encryptionCodeWord = "word";

    public FileDataHandler(string dataDirPath, string dataFileName, bool useEncryption)
    {
       this.dataDirPath=dataDirPath;
       this.dataFileName=dataFileName;
        this.useEncryption=useEncryption;
    }
    public GameData Load()
    {
          string fullPath = Path.Combine(this.dataDirPath, this.dataFileName);
          GameData loadedData=null;
          if(File.Exists(fullPath))
          {
            try{
                //Load the serialized data from the file
                string dataToLoad ="";
                using(FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using(StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }
                //optionally decrypt the data if encryption is enabled
                if(useEncryption)
                {
                    dataToLoad = Encrypt(dataToLoad);
                }
                //deserialize the data from Json back into a C# object
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch(Exception e)
            {
                Debug.LogError("Error occured when trying to load data from file: "+fullPath+"\n"+e);
            }
          }
           return loadedData; // Thêm câu lệnh trả về ở đây
    }
    public void Save(GameData gameData)
    {
        //use Path.Combine to account for different OS's having different path separators
        string fullPath = Path.Combine(this.dataDirPath, this.dataFileName);
        try{
            //create the directory the file will be written to if it doesnt already exist
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            //serialize the C# game data object into JSON
            string dataToStore= JsonUtility.ToJson(gameData,true);
            if(useEncryption)
            {
                dataToStore = Encrypt(dataToStore);
            }
            using(FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using(StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }catch(Exception e)
        {
            Debug.LogError("Error occured when trying to save data to File: "+fullPath+ "\n"+e);
        }
    }  
    private string Encrypt(string data)
    {
        string modifiedData = "";
        for(int i=0;i<data.Length;i++)
        {
            modifiedData += (char)(data[i] ^ encryptionCodeWord[(i % encryptionCodeWord.Length)]);
        }
        return modifiedData;
    }
}
