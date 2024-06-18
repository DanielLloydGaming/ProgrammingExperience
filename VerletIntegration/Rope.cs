using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public VerletState currentState;
    public GameObject nodeObject;
    public float mass = 1f;
    public bool isFixed;
}

public class Constraint
{
    public int node1;
    public int node2;
    public float compensation1;
    public float compensation2;
    public float desiredDistance;
    public float minimumDistance;
    public float maximumDistance;
}

public class Rope : MonoBehaviour
{
    public List<Node> nodeList;
    private int softBodyLimit;
    private int rigidBodyLimit;
    [Range(3, 25)]
    public int nodeLimit;

    private void Start()
    {
        nodeList = new List<Node>(nodeLimit);
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < softBodyLimit; i++)
        {

        }

        for (int i = 0; i < rigidBodyLimit; i++)
        {

        }
    }

    //public int AddNode(Vector3 nodePosition, float mass, bool value)
    //{
    //    int corner1 = AddNode(new Vector3(0, 50, 0), 1, true);
    //    int corner2 = AddNode(new Vector3(0, 48, 0), 1, false);
    //    int corner3 = AddNode(new Vector3(0, 46, 0), 1, false);
    //    int corner4 = AddNode(new Vector3(0, 44, 0), 1, false);
    //    AddFixedConstraint(corner1, corner2, 1.0f);
    //}

    public static void AddFixedConstraint(int node1, int node2, float desiredDistance, float compensation1 = 0.5f, float compensation2 = 0.5f)
    {

    }

    public void AddRelaxedConstraint(ref Vector3 node1, ref Vector3 node2, float compensation1 = 0.5f, float compensation2 = 0.5f)
    {
        Vector3 delta = node2 - node1;
        float deltaLength = delta.magnitude;
        if (deltaLength > 0)
        {
            node1 += delta * compensation1;
            node2 -= delta * compensation2;
        }
    }
}
