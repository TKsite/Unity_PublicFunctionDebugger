using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript1 : MonoBehaviour
{
    public enum TestEnum
    {
        test1,
        test2,
        test3
    }

    public int function1(int prm1,float prm2,bool prm3,string prm4)
    {
        return prm1;
    }

    public string function2(Vector2 prm5,Vector3 prm6,Color prm7)
    {
        return "testfunction2";
    }

    public Vector3 function3(TestEnum prm8,GameObject prm9)
    {
        return new Vector3(1, 2, 3);
    }
}
