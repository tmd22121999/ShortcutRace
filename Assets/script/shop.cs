using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shop : MonoBehaviour
{
    public GameObject[] listSkinUI,datModel;
    public Transform datPreview;
    public Sprite[] skinSprite;
    public Sprite[] buyKind1;
    public Sprite[] buyKind2;
  
    public void updateShop(){
        
        for(int i=0;i<StaticVar.skinUnlocked.Length;i++){
            if(i == StaticVar.skinBrick){
                listSkinUI[i].GetComponent<Button>().enabled = false;
                listSkinUI[i].transform.GetChild(3).gameObject.SetActive(true);
                listSkinUI[i].transform.GetChild(4).gameObject.SetActive(true);
                Destroy(datPreview.GetChild(0).gameObject);
                Instantiate (datModel[i],datPreview);
            }else{
                listSkinUI[i].GetComponent<Button>().enabled = true;
                listSkinUI[i].transform.GetChild(3).gameObject.SetActive(false);
                listSkinUI[i].transform.GetChild(4).gameObject.SetActive(false);
            }
            listSkinUI[i].transform.GetChild(0).gameObject.GetComponent<Image>().sprite  = skinSprite[i];
            if(StaticVar.skinUnlocked[i]<2){
                int k=StaticVar.skinUnlocked[i];
                listSkinUI[i].GetComponent<Image>().sprite = buyKind1[k];
                listSkinUI[i].transform.GetChild(1).gameObject.GetComponent<Image>().sprite = buyKind2[k];
                listSkinUI[i].transform.GetChild(2).gameObject.GetComponent<Text>().text = StaticVar.skinCost[i].ToString();
            }else{
                int k=StaticVar.skinUnlocked[i];
                listSkinUI[i].GetComponent<Image>().sprite = buyKind1[k%2];
                listSkinUI[i].transform.GetChild(1).gameObject.SetActive(false);
                listSkinUI[i].transform.GetChild(2).gameObject.SetActive(false);
            }
        }
    }
}
