using UnityEngine;
using UnityEngine.UI ;
using System.Collections;

public class GameOverManager : MonoBehaviour 
{
	//SKRYPT ZYCIA GRACZA
	public PlayerHealth playerHealth ;

	//ANIMACJA ( KONIEC GRY ITP )
	Animator animationGameOver ;

	//POBIERAMY OBIEKT CANVAS GROUP PO TO ZEBY WLACZYC INTERAKCJE Z PRZYCISKAMI
	CanvasGroup canvasGroup ;



	void Awake()
	{	
		//POBIERAMY COMPONENTY
		animationGameOver = GetComponent<Animator>() ;
		canvasGroup = GetComponent<CanvasGroup>() ;
	}


	// Update is called once per frame
	void Update () 
	{
		//SPRAWDZAMY CZY GRACZ NIE ZYJE
		if( playerHealth.CurrentPlayerHealth() <= 0 )
		{
			//WLACZAMY INTERAKCJE ORAZ BLOKUJEMY RAYCAST
			canvasGroup.interactable = true ;
			canvasGroup.blocksRaycasts = true ;
			//USTAWIAMY TRIGGER KONCA GRY
			animationGameOver.SetTrigger( "GameOver" ) ;
		}
	}

	public void RestartFunction()
	{
		//URUCHAMIAMY ODNOWA LEVEL PODANY JAKO PARAMETR
		Application.LoadLevel (Application.loadedLevel);
	}
}
