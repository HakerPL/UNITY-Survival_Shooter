using UnityEngine;
using UnityEngine.UI ;
using System.Collections;

public class ScoreManager : MonoBehaviour 
{
	//STATYCZNE PUNKTY
	public static int score ;

	//TEKST KTORY BEDZIEMY ZMIENIAC
	Text textScore ;

	

	void Awake()
	{
		//POBIERAMY TEXT I USTAWIAMY POCZATKOWA WARTOSC PUNKTOW
		textScore = GetComponent<Text>() ;
		score = 0 ;
	}

	// Update is called once per frame
	void Update () 
	{
		textScore.text = "Score : " + score ;
	}
}
