using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitboxnorenshu : MonoBehaviour
{
    Vector3 pos; //描画使用
    public float Y2;//ジャンプと奥行き移動用変数
    public float X2;
    public float Z2;

    public Rect HitBox;



    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        HitBox = new Rect(X2, Y2, 5, 5);


        X2 +=0.0003f;
        Y2 = 5f;

        pos.x = X2;
        pos.y = Z2 + Y2;
        transform.position = pos;
    }
}
