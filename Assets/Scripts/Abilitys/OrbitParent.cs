using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// put this script on the object around which other objects orbit
public class OrbitParent : MonoBehaviour
{

    [SerializeField] private Orbiter orbiterPrefab = null; // assign to this a prefab of your object that will orbit
    [SerializeField] private float orbitingSpeed = 1f; // number of circles per second an orbiting object makes
    [SerializeField] private float orbiterCount = 2f;
    [SerializeField] private float timeBetweenCreation = 1f;

    private List<Orbiter> orbiters = new List<Orbiter>();

    private float elapsedTime = 0;

    private void Start()
    {
        CreateOrbiter();
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        float currentAngle = orbitingSpeed * elapsedTime;
        for (int i = 0; i < orbiters.Count; ++i)
        {
            orbiters[i].UpdateOrbiter(currentAngle, i, orbiters.Count);
        }
    }

    public void AddOrbiter()
    {
        Orbiter newOrbiter = Instantiate(orbiterPrefab, transform); // create new orbiting object as child of this object
        orbiters.Add(newOrbiter); // store orbiter so it can be updated
    }

    public void RemoveOrbiter()
    {
        Orbiter lastOrbiter = orbiters[orbiters.Count - 1];
        orbiters.RemoveAt(orbiters.Count - 1);
        Destroy(lastOrbiter.gameObject);
    }

    public void CreateOrbiter()
    {
        StartCoroutine(CreateOrbiterCo());
    }

    protected virtual IEnumerator CreateOrbiterCo()
    {
        for (int i = 0; i < 2; i++)
        {
            AddOrbiter();
            yield return new WaitForSeconds(timeBetweenCreation);
        }
    }

}
