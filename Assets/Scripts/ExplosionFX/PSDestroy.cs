using UnityEngine;
using System.Collections;

public class PSDestroy : MonoBehaviour {

	private ParticleSystem ps;
    private void Awake()
    {
		ps = GetComponent<ParticleSystem>();
    }
    // Use this for initialization
    void Start () {
		var main = ps.main;
		Destroy(gameObject, main.duration);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
