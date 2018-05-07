using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class BlockSaver {


    // Save the grid
    public static void saveGrid(List<Block> Grid)
    {
        // initialize BinaryFormatter and Filestream
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/cfg/grid.sav", FileMode.Create);

        // Make Serializable Grid
        SerializableGrid sGrid = new SerializableGrid(Grid);

        bf.Serialize(stream, sGrid);
        stream.Close();
    }


    public static List<Block> loadGrid()
    {
        if (File.Exists(Application.persistentDataPath + "/cfg/grid.sav"))
        {
            // initialize BinaryFormatter and Filestream
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/cfg/grid.sav", FileMode.Open);

            SerializableGrid sGrid = bf.Deserialize(stream) as SerializableGrid;

            return sGrid.grid;
            
        }

        return null;
    }


}

[System.Serializable]
public class SerializableGrid {
    public List<Block> grid;

    public SerializableGrid(List<Block> newGrid)
    {
        grid = newGrid;
    }
}
