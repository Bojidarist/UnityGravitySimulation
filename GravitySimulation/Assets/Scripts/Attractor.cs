using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    /// <summary>
    /// The <see cref="Rigidbody"/> of the attractor
    /// </summary>
    public Rigidbody rb;

    /// <summary>
    /// The gravitational constant
    /// </summary>
    public float G;

    /// <summary>
    /// A <see cref="List{Attractor}"/> containing all attractors in the scene
    /// </summary>
    public static List<Attractor> Attractors;

    /// <summary>
    /// Fixed update is used when working with physics
    /// because it is not depended of fps
    /// </summary>
    private void FixedUpdate()
    {
        for (int i = 0; i < Attractors.Count; i++)
        {
            if (Attractors[i] != this)
            {
                this.Attract(Attractors[i]);
            }
        }
    }

    /// <summary>
    /// This code runs when the object spawns
    /// </summary>
    private void OnEnable()
    {
        if (Attractors == null)
        {
            Attractors = new List<Attractor>();
        }
        Attractors.Add(this);
    }

    /// <summary>
    /// This code runs when the object is removed
    /// </summary>
    private void OnDisable()
    {
        Attractors.Remove(this);
    }

    /// <summary>
    /// Attract a object to the attractor
    /// </summary>
    /// <param name="objToAttract">The object to attract</param>
    public void Attract(Attractor objToAttract)
    {
        // The Rigidbody of the object we need to attract
        Rigidbody rbToAttract = objToAttract.rb;

        // The direction of the force
        Vector3 direction = rb.position - rbToAttract.position;

        // The distance between the objects
        float r = direction.sqrMagnitude;
        if (r == 0f)
        {
            // If the objects are on top of each other do nothing
            return;
        }

        // The magnitude of the force
        float forceMagnitude = G * ((rb.mass * rbToAttract.mass) / (r * r));
        Vector3 force = direction.normalized * forceMagnitude;

        // Add the force to the object we need to attract
        rbToAttract.AddForce(force);
    }
}
