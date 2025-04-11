using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test1 : MonoBehaviour
{
    public Enemy enemy;

    public void Start()
    {
        Destroy(enemy);
    }

    public void Update()
    {
        FunctionA();
        FunctionC();
        FunctionB();
        
    }

    public void FunctionA()
    {
        enemy.health = 5;
        Debug.Log(enemy.health);
    }

    private void FunctionB()
    {
        enemy.name = "...";
    }

    private void FunctionC()
    {
        if (enemy != null)
        {
            Debug.Log("Enemy is NOT null");
        }
        else
        {
            Debug.LogWarning("Enemy is null");
        }
        Debug.Log(enemy.health);

    }    
}
