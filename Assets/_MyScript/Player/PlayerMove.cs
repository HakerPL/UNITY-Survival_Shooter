using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour 
{

	public float speed = 6f ;

	//ZMIENNA PRZECHOWUJACA POZYCJE GRACZA
	Vector3 move ;
	//ZMIENNA DO ANIMACJI
	Animator PlayerAnimation ;
	//RIGIDBODY (FIZYKA)
	Rigidbody PlayerRigidbody ;
	//MASKA PODLOGI (CELOWANIE RAY CAST)
	int floorMask ;
	//DLUGOSC PROMIENIA Z KAMERY DO SCENY
	float RayLengthCameraToScene = 100f ;


	//ZMIENNE CZASU DO PRZYSPIESZENIA POSTACI
	float timeActivate ;
	float time ;


	void Awake()
	{
		//TWORZYMY MASKE PODLOGI
		floorMask = LayerMask.GetMask("Floor") ;

		//USTAWIAMY RIGIDBODY
		PlayerRigidbody = GetComponent<Rigidbody>() ;
		//USTAWIAMY ANIMACJE
		PlayerAnimation = GetComponent<Animator>() ;
	}


	// Update is called once per frame
	void FixedUpdate () 
	{
		//POBIERAMY ZMIENNE RUCHU Z INPUT GetAxisRaw ZWRACA -1 , 0 , 1 NIE MA PLYNNEGO PRZEJSCIA
		float h = Input.GetAxisRaw ("Horizontal") ;
		float v = Input.GetAxisRaw ("Vertical") ;

		//FUNKCJA RUCHU GRACZA
		MovePlayer(h , v) ;

		//OBRUT GRACZEM DO MYSZKI
		RotatePlayerFaceToMouse() ;

		//ANIMACJA RYCHY LUB IDLE
		AnimationPlayer( h , v ) ;

	}

	void MovePlayer( float h , float v )
	{
		//USTAWIAMY NOWA POZYCJE
		move.Set( h , 0f , v ) ;

		//NORMALIZED PRZY URZYCIU H I V NARAZ ZAMIAST 1.4 MAMY ZAWSZE 1
		move = move.normalized * speed * Time.deltaTime ;

		//ZMIENIAMY POZYCJE (DO OBECNEJ DODAJEMY VECTOR3 (move))
		PlayerRigidbody.MovePosition( transform.position + move ) ;
	}

	void RotatePlayerFaceToMouse()
	{
		//TWORZYMY PROMIEN Z POZYCJI MYSZKI DO KAMERY
		Ray CamRay = Camera.main.ScreenPointToRay( Input.mousePosition ) ;

		//ZMIENNA PRZECHOWUJE INFORMACJE O TYM GDZIE TRAFILISMY
		RaycastHit floorHit ; 

		//SPRAWDZAMY CZY TRAFILISMY W PODLOGE 
		if( Physics.Raycast( CamRay , out floorHit , RayLengthCameraToScene , floorMask ) )
		{
			//TWORZYMY VECTOR3 OD GRACZA DO POZYCJI MYSZKI NA EKRANIE (DOKLADNIE TO NA PODLODZE [Floor])
			Vector3 PlayerToMouse = floorHit.point - transform.position ;

			//VEKTOR MA BYC NA PODLODZE NIE W POWIETRZU
			PlayerToMouse.y = 0f ;

			//USTAWIAMY ROTACJE NA CEL MYSZKI (OBRACA NA Z AXIS??)
			Quaternion newPlayerRotation = Quaternion.LookRotation ( PlayerToMouse ) ;

			//USTAWIAMY ROTACJE GRACZA
			PlayerRigidbody.MoveRotation ( newPlayerRotation ) ;
		}
	}

	void AnimationPlayer ( float h ,float v )
	{
		//ZMIENNA ROWNA SIE TRUE JAK SIE NIE RUSZAMY
		bool walk = h != 0f || v != 0f ;

		//USTAWIAMY ANIMACJE
		PlayerAnimation.SetBool ( "IsMove" , walk ) ;
	}

	public void SpeedUP( float activeTime )
	{
		StartCoroutine( SpeedUP_Coroutine( activeTime ) ) ;
	}

	IEnumerator SpeedUP_Coroutine( float activeTime )
	{
		//PRZYPISUJEMY CZAS AKTYWACJI
		timeActivate = activeTime ;
		//ZERUJEMY CZAS
		time = 0 ;

		//POBIERAMY STARA SZYBKOSC ORAZ ZWIEKSZAMY OBECNA O 2x
		float oldSpeed = speed ;
		speed *= 2 ;

		//Debug.Log ( "oldSpeed = " + oldSpeed + "  speed = " + speed ) ;

		//SPRAWDZAMY CZY CZAS AKTYWNOSCI JEST WIEKSZY OD OBECNEGO
		while( timeActivate > time )
		{
			//ZWIEKSZAMY OBECNY I WYCHODZIMY Z PETLI
			time += Time.deltaTime ;
			//Debug.Log ( "time = " + time + "  timeActivate = " + timeActivate ) ;
			yield return new WaitForFixedUpdate(); 
		}

		//PRZYPISUJEMY STARA SZYBKOSC
		speed = oldSpeed ;
		//Debug.Log ( "oldSpeed = " + oldSpeed + "  speed = " + speed ) ;
		
		yield return new WaitForEndOfFrame();  
	}
}
