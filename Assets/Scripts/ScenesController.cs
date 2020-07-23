using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenesControll : MonoBehaviour
{
    public void openScenes(string nameScene){
	switch(nameScene){
		case "Fase1":

		break;
		case "Menu":
			SceneManager.LoadScene(nameScene)
		break;
		case "Instrucoes":
			SceneManager.LoadScene(nameScene)
		break;
		case "Creditos":
			SceneManager.LoadScene(nameScene)
		break;
		case "Sair":
			finish();
		break;

	}
}
    
}
