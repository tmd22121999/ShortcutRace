using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveData 
{
    public static void save(){
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath  +"/save.data";
        FileStream stream = new FileStream(path,FileMode.Create);

        Data data = new Data(StaticVar.coin, StaticVar.defaultBrick, StaticVar.map, StaticVar.namePlayer[0]);
        //Data data = new Data(2000, 5, 4, "day la ten");
        formatter.Serialize(stream, data);
        stream.Close(); 
    }
    public static Data load(){
        string path = Application.persistentDataPath  +"/save.data";
        if(File.Exists(path)){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path,FileMode.Open);
            Data data = formatter.Deserialize(stream) as Data;
            setdata(data); 
            stream.Close();
            return data;
        }else{
            return null;
        }
    }
    public static void setdata(Data data){
        StaticVar.coin= data.coin;
        StaticVar.defaultBrick = data.defaultBrick;
        StaticVar.map = data.map;
        StaticVar.namePlayer[0]= data.namePlayer;
    }
}
