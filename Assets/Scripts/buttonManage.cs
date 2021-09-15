using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonManage : MonoBehaviour
{
    bool City;

    private void Update()
    {
        
    }
    public void PlayCommand()
    {
        City = gameObject.GetComponent<skinManage>().city;

        if (City)
         {
             PlayerPrefs.SetInt("City", 1);
             SceneManager.LoadScene("Game");
        Time.timeScale = 1f;

         }
         else
         {
             PlayerPrefs.SetInt("City", 0);
             SceneManager.LoadScene("HillyGame");
        Time.timeScale = 1f;
         }
         

    }


}
