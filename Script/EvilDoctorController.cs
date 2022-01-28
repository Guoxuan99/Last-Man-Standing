using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilDoctorController : MonoBehaviour
{

	public Transform player;
	public float playerDistance;
	public float awareAI = 10f;
	public float ChaseSpeed;
	public float PatrolSpeed;

	

	public Transform[] navPoint;
	public UnityEngine.AI.NavMeshAgent agent;
	private int destPoint = 0;
	public Transform goal;

	private Animator anim;

	public GameObject text_alert;
	void Start()
	{
		anim = GetComponent<Animator>();
		//UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		agent.destination = goal.position;

		agent.autoBraking = true;

		text_alert.SetActive(false);

	}

	void Update()
	{
		playerDistance = Vector3.Distance(player.position, transform.position);

		//if (playerDistance < awareAI)
		//{
		//	LookAtPlayer();
		//}
		if (!player.GetComponent<PlayerMove>().isDead)
		{
			if (playerDistance < awareAI)
			{
				if (playerDistance < awareAI)
				{
					
					Chase();

				}
				else
				{
					anim.SetTrigger("Patrol");	
					GotoNextPoint();
				}	
			}
			else
			{
				
				anim.SetTrigger("Patrol");
			}


			if (agent.remainingDistance < 0.5f)
			{
				anim.SetTrigger("Patrol");
				GotoNextPoint();
			}
		}
		else
		{
			GotoNextPoint();
		}
			
			
			

	}

	//void LookAtPlayer()
	//{
	//	transform.LookAt(player);
	//}


	void GotoNextPoint()
	{	
		text_alert.SetActive(false);
		agent.speed = PatrolSpeed;
		if (navPoint.Length == 0)
			return;
		agent.destination = navPoint[destPoint].position;
		destPoint = (destPoint + 1) % navPoint.Length;
	}


	void Chase()
	{
		text_alert.SetActive(true);
		anim.SetTrigger("Chase");
		
		//transform.Translate(Vector3.forward * ChaseSpeed * Time.deltaTime);
		agent.speed = ChaseSpeed;
		agent.destination = player.position;
	}


}