using UnityEngine;

public class Orbiter : MonoBehaviour
{
    /// <summary>
    /// All generated spheres/moons/planets
    /// </summary>
    public GameObject[] spheres;

    /// <summary>
    /// The amount of spheres/planets
    /// </summary>
    public int sphereCount = 2000;

    /// <summary>
    /// The maximum spawn radius
    /// </summary>
    public int maxRadius = 100;

    /// <summary>
    /// A force multiplier to make the spheres have more interest in the planet
    /// </summary>
    public int forceMultiplier = 100;

    /// <summary>
    /// This executes when the script starts
    /// </summary>
    private void Start()
    {
        spheres = CreateSpheres(sphereCount, maxRadius);
    }

    /// <summary>
    /// This executes before the script starts
    /// </summary>
    private void Awake()
    {
        spheres = new GameObject[sphereCount];
    }

    /// <summary>
    /// Create sample spheres/planets/objects
    /// </summary>
    /// <param name="count">The amount of objects that will be created</param>
    /// <param name="radius">The spawn radius</param>
    /// <returns>Random generated spheres</returns>
    private GameObject[] CreateSpheres(int count, int radius)
    {
        // Create a temporary array to store the spheres
        var spheres = new GameObject[sphereCount];
        // Create a sample sphere that will be duplicated
        var sphereToCopy = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        // Add a Rigidbody to the sample sphere
        Rigidbody rb = sphereToCopy.AddComponent<Rigidbody>();
        // Remove the gravity from the sample sphere (we will use our own calculations)
        rb.useGravity = false;

        // Create spheres with different sizes in the radius
        for (int i = 0; i < count; i++)
        {
            var sp = GameObject.Instantiate(sphereToCopy);
            sp.transform.position = this.transform.position +
                new Vector3(Random.Range(-radius, radius),
                            Random.Range(-10, 10),
                            Random.Range(-radius, radius));
            sp.transform.localScale *= Random.Range(0.5f, 1);
            spheres[i] = sp;
        }
        // Destroy the sample sphere
        GameObject.Destroy(sphereToCopy);

        return spheres;
    }

    /// <summary>
    /// Fixed update is used when working with physics
    /// because it is not depended on fps
    /// </summary>
    private void FixedUpdate()
    {
        // Calculate for all spheres
        for (int i = 0; i < spheres.Length; i++)
        {
            // Calculate the difference in position between the planet and a sphere
            Vector3 difference = this.transform.position - spheres[i].transform.position;
            // Calculate the distance between the planet and a sphere
            float distance = difference.magnitude;
            // Calculate the direction of the gravitational force
            Vector3 gravityDirection = difference.normalized;
            // Calculate the gravitational force between the two objects
            float gravity = 6.7f * (this.transform.localScale.x * spheres[i].transform.localScale.x * forceMultiplier) / (distance * distance);
            // The gravity vector
            Vector3 gravityVector = (gravityDirection * gravity);
            // Add a little push to the sphere so it orbits around the planet instead of going directly to the center
            spheres[i].transform.GetComponent<Rigidbody>().AddForce(spheres[i].transform.forward, ForceMode.Acceleration);
            // Apply the gravitational force of the planet to the sphere
            spheres[i].transform.GetComponent<Rigidbody>().AddForce(gravityVector, ForceMode.Acceleration);
        }
    }
}
