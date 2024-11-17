using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public Material snowMaterial;
    public Material snowyBarkMaterial;

    private MeshRenderer mrTree;
    private Material[] originalMaterials;
    

    private void ChangeTree(WeatherType weatherType)
    {
        if (weatherType == WeatherType.Snow)
        {
            ChangeToSnowCoveredTree();
        }
        else
        {
            ChangeToNormalTree();
        }
    }

    private void ChangeToNormalTree()
    {
        mrTree.materials = originalMaterials;
    }

    private void ChangeToSnowCoveredTree()
    {
        Material[] tempMaterials = mrTree.materials;

        if (tempMaterials.Length > 1)
        {
            for (int i = 0; i < tempMaterials.Length; i++)
            {
                if (i == 0)
                {
                    tempMaterials[i] = snowyBarkMaterial;
                }
                else
                {
                    tempMaterials[i] = snowMaterial;
                }
            }
        }

        mrTree.materials = tempMaterials;
    }

    // Start is called before the first frame update
    void Start()
    {
        mrTree = GetComponent<MeshRenderer>();

        // Save original materials of the tree
        originalMaterials = mrTree.materials;

        WeatherController.instance.OnWeatherChanged.AddListener(ChangeTree);
    }
}
