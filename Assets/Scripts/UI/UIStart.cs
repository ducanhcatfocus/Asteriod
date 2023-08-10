using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIStart : MonoBehaviour
{
   public void StartButtonOnClick()
   {
        SceneManager.LoadScene(1);
   }
   
   public void ToggleSound()
   {

   }
   public void ToggleMusic()
   {
    
   }

    public void QuitGame()
    {
        Application.Quit();
    }
    


}
