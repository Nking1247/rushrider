using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class playerManager : MonoBehaviour
{

    [SerializeField] GameObject[] playerSkins;
    
    int skinNumber;
    Transform parent;
    private void Awake()
    {
        parent = transform;
       
            skinNumber = PlayerPrefs.GetInt("PlayerSkin");
            Instantiate(playerSkins[skinNumber], new Vector3(0,.2f,1), Quaternion.identity, parent);
       
            
        
        
    }


}
