using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIDeadzone : MonoBehaviour
{
    public GameObject nearDeadzone;
    public Text distanceLeftTxt;
    [SerializeField] GameObject raft;
    [SerializeField] GameObject deadzone;
    public float currentZ;
    public float deadZoneZ;
    public float distanceLeft;
    void Start()
    {
        deadZoneZ = deadzone.transform.position.z;
    }
    // Update is called once per frame
    void Update()
    {
        currentZ = raft.transform.position.z;
        distanceLeft = currentZ-deadZoneZ-(float)7.6;
        
        if (distanceLeft<=15)
        {
            nearDeadzone.SetActive(true);
            distanceLeftTxt.text = "Near Deadzone!! " + distanceLeft + " left";
        }
    }
}
