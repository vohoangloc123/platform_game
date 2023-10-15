using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistence
{
    // Start is called before the first frame update
   void LoadData(GameData gameData);
   void SaveData(ref GameData gameData);
}
