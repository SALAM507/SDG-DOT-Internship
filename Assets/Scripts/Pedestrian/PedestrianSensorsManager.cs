﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum Truck_ 
{
    CargoTruck,
    CargoTruck_2,
    CargoTruck_3,
    Dumper,
    Dumper_2,
    Dumper_3,
    Tanker,
    Tanker_2,
    YelloTanker,
    YellowTanker_2,
    YellowTanker_3
}
public class PedestrianSensorsManager : MonoBehaviour
{
    public GameObject player;
    public Material pedestrianHighlightedMaterial;
    private List<UnityEngine.Material> car_original_material = new List<UnityEngine.Material>();
    static private PedestrianSensorsManager __Current;
    private Dictionary<Truck_, List<SensorsTypes>> activeSensors = new Dictionary<Truck_, List<SensorsTypes>>();

   
    void FixedUpdate()
    {
        foreach(KeyValuePair<Truck_, List<SensorsTypes>> truck in activeSensors)
        {
            if(truck.Value.Count > 0)
            {
                Debug.Log("Sensor(s) is/are obstructed");
                changeCarMaterial();

            }
            else
            {
                changeCarMaterialToOriginal();
            }
            
        }
    }

    static public CarSensorsManager Current{
        get
        {
            if(__Current==null)
            {
                __Current = GameObject.FindObjectOfType<PedestrianSensorsManager>();

            }

            return __Current;
        }
    }

    private void changeCarMaterial()
    {
        Debug.Log("Changing Car Material");
        Debug.Log(car);
        foreach(Transform child in car.transform)
        {
            Debug.Log("Changing Car Material Child");
            car_original_material.Add(child.gameObject.GetComponent<Renderer>().material);
            child.gameObject.GetComponent<Renderer>().material = pedestrianHighlightedMaterial;
            

            break;
        }
    }

    private void changeCarMaterialToOriginal()
    {
        Debug.Log("Changing Car Material to original");
        foreach(Transform child in car.transform)
        {
            child.gameObject.GetComponent<Renderer>().material = car_original_material[0];

            break;
        }
    }

    public void reportDetection(Truck_ t, SensorsTypes sensorType)
    {
        if(!activeSensors.ContainsKey(t))
        {
            activeSensors.Add(t, new List<SensorsTypes>());
        }

        activeSensors[t].Add(sensorType);

    }

    public void unreportDetection(Truck_ t, SensorsTypes sensorType)
    {
        if(!activeSensors.ContainsKey(t))
        {
            return;
        }

        activeSensors[t].Remove(sensorType);
    }

    public bool isReported(Truck_ t, SensorsTypes sensorType)
    {
        if(activeSensors.ContainsKey(t))
        {
            if(activeSensors[t].Contains(sensorType))
            {
                return true;
            }
        }

        return false;
    }
}
