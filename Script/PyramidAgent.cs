using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using System.Collections;
using System.Collections.Generic;

public class PyramidAgent : Agent
{
    public GameObject area;
    GameLogic m_GameLogic;
    //PyramidArea m_MyArea;
    Rigidbody m_AgentRb;
    PyramidSwitch m_SwitchLogic;
    //public GameObject areaSwitch;
    public bool useVectorObs;
    public GameObject text_5sec;
    string message;

    public override void Initialize()
    {
        m_AgentRb = GetComponent<Rigidbody>();
        //Initialize the Game Logic
        m_GameLogic = area.GetComponent<GameLogic>();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Collect observations to be learned by the ml agent
        if (useVectorObs)
        {
            sensor.AddObservation(false);
            sensor.AddObservation(transform.InverseTransformDirection(m_AgentRb.velocity));
        }
    }

    // Agent will move based on the act para passed into this method by mlagent
    public void MoveAgent(ActionSegment<int> act)
    {
        var dirToGo = Vector3.zero;
        var rotateDir = Vector3.zero;

        var action = act[0];
        switch (action)
        {
            case 1:
                dirToGo = transform.forward * 0.5f;
                break;
            case 2:
                dirToGo = transform.forward * -0.5f;
                break;
            case 3:
                rotateDir = transform.up * 0.5f;
                break;
            case 4:
                rotateDir = transform.up * -0.5f;
                break;
        }
        transform.Rotate(rotateDir, Time.deltaTime * 200f);
        m_AgentRb.AddForce(dirToGo * 2f, ForceMode.VelocityChange);
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        // Add penalty when agent took too long to find the goal
        AddReward(-1f / MaxStep);
        MoveAgent(actionBuffers.DiscreteActions);
    }

    // For player to control agent when there is no trained model plugged in
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActionsOut = actionsOut.DiscreteActions;
        if (Input.GetKey(KeyCode.D))
        {
            discreteActionsOut[0] = 3;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            discreteActionsOut[0] = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            discreteActionsOut[0] = 4;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            discreteActionsOut[0] = 2;
        }
    }

    public override void OnEpisodeBegin()
    {
        var enumerable = Enumerable.Range(0, 9).OrderBy(x => Guid.NewGuid()).Take(9);
        var items = enumerable.ToArray();

        // Initialize the agent coordinate and rotation
        m_AgentRb.velocity = Vector3.zero;
        transform.rotation = Quaternion.Euler(new Vector3(0f, Random.Range(0, 360)));
    }

    void OnCollisionEnter(Collision collision)
    {
        // triggers when agent collide with the goal
        if (collision.gameObject.CompareTag("goal"))
        {
            // Destroy Syringe when agent collide with syringe
            //m_GameLogic.ReduceSyringeCount();
            Destroy(collision.gameObject);

            Debug.Log(gameObject + "Agent pressed switch");
            message = "Your dog had picked up a vaccine.";
            if (m_GameLogic.GetRemainingSyringeCount() == 0)
            {
                message = "You had found all vaccine. Find the key to escape!";
            }
            StartCoroutine("WaitForSec", message);
        }
    }

    IEnumerator WaitForSec(string message)
    {
        text_5sec.SetActive(true);
        text_5sec.GetComponent<TMPro.TextMeshProUGUI>().text = message; 
        Debug.Log("waiting");
        yield return new WaitForSeconds(3);
        text_5sec.SetActive(false);
    }
}
