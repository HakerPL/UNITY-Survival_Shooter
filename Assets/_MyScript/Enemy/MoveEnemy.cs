using UnityEngine;
using System.Collections;

public class MoveEnemy : MonoBehaviour 
{
	//POZYCJA GRACZA
	Transform playerPosition ;
	//MAPA DO PORUSZANIA SIE ENEMY DO CELU
	NavMeshAgent navMesh ;
	//REFERENCJA DO ZYCIA GRACZA
	PlayerHealth playerHealth ;
	//REFERENCJA DO ZYCIA ENEMY
	EnemyHealth enemyHealth ;
	
	// Use this for initialization
	void Awake () 
	{
		//SZUKAMY GRACZA I POBIERAMY JEGO WSPOLZEDNE
		playerPosition = GameObject.FindGameObjectWithTag( "Player" ).transform ;
		//PRZYPISUJEMY KOMPONENTY
		playerHealth = playerPosition.GetComponent<PlayerHealth>() ;
		enemyHealth = GetComponent<EnemyHealth>() ;
	
		//POBIERAMY MAPE DO PORUSZANIA SIE
		navMesh = GetComponent<NavMeshAgent> () ;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//JESLI GRACZ I ENEMY ZYJA TO PORUSZAMY PRZECIWNIEKIEM
		if( enemyHealth.CurrentEnemyHealth() > 0 && playerHealth.CurrentPlayerHealth() > 0 )
		{
			//PORUSZAMY ENEMY DO GRACZA
			navMesh.SetDestination( playerPosition.position ) ;
			//Debug.Log( "Enemy Move" ) ;
		}
		else
		{
			//W PRZECIWNYM RAZIE WYLACZAMY NavMesh
			navMesh.enabled = false ;
			//Debug.Log( "Enemy Stop" ) ;
		}
	}
}
