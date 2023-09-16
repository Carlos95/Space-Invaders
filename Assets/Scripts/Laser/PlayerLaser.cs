using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaser : MonoBehaviour
{
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private AudioClip laserClip;
    private GameObject laserInstance;
    Vector3 offset = new Vector2(0, 7);

    void Awake()
    {
        laserInstance = (GameObject)Instantiate(laserPrefab, transform.position, transform.rotation * Quaternion.Euler(0, 0, 0));
        
    }
    // Start is called before the first frame update
    void Start()
    {
        DeactivateLaser();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        laserInstance.transform.position = transform.position + offset;
    }

    void OnDestroy()
    {
        Destroy(laserInstance);
    }

    public void ActivateLaser()
    {
        StartCoroutine(IEActivateLaser());
    }

    public bool IsActive()
    {
        return laserInstance.activeSelf;
    }


    // Method to make laser appear after flashing 3 times and dissappear. Stay some seconds off and then repeat

    IEnumerator IEActivateLaser()
    {
        laserInstance.SetActive(true);
        audioManager.PlayAudio(laserClip, 0.7f);
        yield return new WaitForSeconds(2);
        DeactivateLaser();
    }

    public void DeactivateLaser()
    {
        laserInstance.SetActive(false);
    }
}
