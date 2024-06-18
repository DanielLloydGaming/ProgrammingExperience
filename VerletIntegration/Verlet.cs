using UnityEngine;

public struct VerletState
{
    public Vector2 position; // Position
    public Vector2 previousPosition; // Previous Position
    public Vector2 force; // Force accumulator

    public void Integrate()
    {
        Vector2 currentPosition = position;
        position = 2.0f * position - previousPosition + force * Time.fixedDeltaTime * Time.fixedDeltaTime;
        previousPosition = currentPosition;
        force = Vector2.zero;
    }

    public void AddForce(Vector2 inputForce)
    {
        force += inputForce;
    }
}

public class Verlet : MonoBehaviour
{
    public VerletState state = new VerletState();

    void Start()
    {
        state.position = transform.position;
        state.previousPosition = state.position;
        state.force = Vector2.zero;
    }

    void Update()
    {
        // GetKeyDown is unreliable in FixedUpdate, so do this here.
        if (Input.GetKeyDown(KeyCode.W))
        {
            state.previousPosition.y = state.position.y - 10.0f*Time.fixedDeltaTime;
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            state.AddForce(new Vector2(-7.0f, 0));
        }
        if (Input.GetKey(KeyCode.D))
        {
            state.AddForce(new Vector2(7.0f, 0));
        }

        // Add gravity
        state.AddForce(new Vector2(0, -9.8f));

        // Perform Verlet Integration
        state.Integrate();
        
        // If the object goes below the ground, stop it.
        if (state.position.y < -3.5f)
        {
            state.position.y = -3.5f;
        }

        // Update gameobject using the state data
        transform.position = state.position;
    }
}
