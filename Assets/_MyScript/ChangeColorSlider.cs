using UnityEngine;
using UnityEngine.UI ;
using System.Collections;

public class ChangeColorSlider : MonoBehaviour 
{
	//KOLORY
	public Color red ;
	public Color green ;


	//TABLICA OBIEKTOW Image OD SLIDERA
	Image[] img ;
	//SLIDER
	Slider sld ;


	void Awake () 
	{	
		//POBIERAMY SLIDER
		sld = GetComponent<Slider>() ;
		//POBIERAMY WSZYSTKIE DZIEIC KTORE POSIADAJA COMPONENT Image
		img = sld.GetComponentsInChildren<Image>() ;
	}

	// Update is called once per frame
	void Update () 
	{
		//ZMIENIAMY KOLOR SKALI NA CZERWONY JESLI MAMY MNIEJ NIZ 35% ZYCIA
		//W TYM PRZYPADKU WIEM ZE OBRAZEK SKALI TO 2 (CZYT 1) OBIEKT DZIECKO
		if( ( sld.value / sld.maxValue ) <= 0.35 )
			img[1].color = red ;
		else
			img[1].color = green ;
	}
}
