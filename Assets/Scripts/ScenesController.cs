using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesController : MonoBehaviour
{
	public void openScene(string nameScene){
	switch (nameScene){
		
		case "MenuScene":
				SceneManager.LoadScene(1);
			break;

		case "InstrucoesScene":
				SceneManager.LoadScene(2);
			break;

		case "CreditosScene":
				SceneManager.LoadScene(3);
			break;

		case "SoloScene":
				SceneManager.LoadScene(4);
			break;

		case "Sair":
				Application.Quit();
			break;

	}
}
    
}
