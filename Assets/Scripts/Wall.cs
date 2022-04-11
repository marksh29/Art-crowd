using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] Rigidbody[] obj;
    [SerializeField] float force, xx, yy, zz;
    void Start()
    {
        
    }   
    public void DropWall()
    {
        for (int i = 0; i < obj.Length; i++)
        {
            obj[i].useGravity = true;
        }
        obj[0].AddRelativeForce(new Vector3(xx, yy, zz) * force, ForceMode.Impulse);
        obj[1].AddRelativeForce(new Vector3(-xx, yy, zz) * force, ForceMode.Impulse);
    }
}
