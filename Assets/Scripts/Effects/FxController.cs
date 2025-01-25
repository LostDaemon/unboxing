using UnityEngine;

public class FxController : MonoBehaviour
{
    public GameObject YellowSparksEffect;

    public void ShowYellowSparks(Vector3 position)
    {
        var particles = Instantiate(YellowSparksEffect, position, Quaternion.identity).gameObject.GetComponent<ParticleSystem>();
        PlayUntilComplete(particles);
    }

    private void PlayUntilComplete(ParticleSystem particleSystem)
    {
        StartCoroutine(WaitForParticleSystem(particleSystem));
        System.Collections.IEnumerator WaitForParticleSystem(ParticleSystem ps)
        {
            // Ждем, пока частицы будут активны
            while (particleSystem.IsAlive())
            {
                yield return null;
            }
            Destroy(ps.gameObject);
        }
    }
}
