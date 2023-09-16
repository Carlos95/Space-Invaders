using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidPrivacyPolicy : MonoBehaviour
{

    [SerializeField] private GameObject privacyPolicyGameObject;
    // Start is called before the first frame update
    void Start()
    {
#if UNITY_IOS
        privacyPolicyGameObject.SetActive(false);
#endif
#if UNITY_ANDROID
       privacyPolicyGameObject.SetActive(true);
#endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
