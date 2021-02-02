
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    ParticleSystem p;
    private void Awake()
    {
        p = this.GetComponent<ParticleSystem>();
    }
    private void Start()
    {
        Destroy(this.gameObject, p.main.duration);
    }
    public float GetDuration()
    {
       return p.main.duration;
    }


    
}
