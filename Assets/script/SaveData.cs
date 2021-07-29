using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.Globalization;

public static class SaveData 
{
    public static void save(){
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath  +"/save.data";
        FileStream stream = new FileStream(path,FileMode.Create);

        Data data = new Data(StaticVar.coin, StaticVar.defaultBrick, StaticVar.map, StaticVar.upgrade1, StaticVar.upgrade2, StaticVar.skinBrick,StaticVar.bonus,StaticVar.skinUnlocked,StaticVar.namePlayer[0],DateTime.Now);
        
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
        Debug.Log(data.map);
        StaticVar.coin= data.coin;
         StaticVar.upgrade1 = data.upgrade1 ;
         StaticVar.coin+= (int)(data.upgrade1/2 * (int)((DateTime.Now - data.lastSaveTime).TotalMinutes)) ;
         StaticVar.upgrade2 = data.upgrade2 ;
         StaticVar.skinBrick = data.skinBrick;
         StaticVar.skinUnlocked = data.skinUnlocked;
         StaticVar.bonus = data.bonus;
        StaticVar.defaultBrick = data.upgrade2 ;
        StaticVar.map = data.map;
        StaticVar.namePlayer[0]= data.namePlayer;
        for(int i=0;i<StaticVar.skinUnlocked.Length;i++){
            if(StaticVar.skinUnlocked[i]<2){
                StaticVar.bonusSkin = i;
                break;
            }
            StaticVar.bonusSkin = -1;
        }
    }
    public static void resetSave(){
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath  +"/save.data";
        FileStream stream = new FileStream(path,FileMode.Create);
        int[] sunlock= {3,1,0,1,1,2};
        Data data = new Data(0, 0, 1, 0, 0, 0,0,sunlock,"name",DateTime.Now);
        
        //Data data = new Data(2000, 5, 4, "day la ten");
        formatter.Serialize(stream, data);
        stream.Close(); 
    }
}
