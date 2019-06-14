using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector3 pos; //描画使用
    public float Y;//ジャンプと奥行き移動用変数

    public float Z;
  
    public float speed = 0.05f;//横移動スピード
    public float speedz = 0.02f;//縦軸移動すぴーと

    public float speedy = 0.15f;//ジャンプスピード変数
    public float jumpshosoku = 0.15f;
    public float gravity = -0.015f;//重力常にかけるよう

    public bool jumpstate = false;//ジャンプ状態用


    Animator animator;//アニメーションの使い方わからんので参考書のうつし

    // Start is called before the first frame update
    void Start()
    {
        this.animator = GetComponent<Animator>();//アニメーション系は参考書の写し




    }
    // Update is called once per frame
    void Update()
    {

        if (Y < 0) Y = 0;//Yの範囲制限
        if (Y > 5) Y = 5;


        


        Vector3 scale = transform.localScale;

        //上下移動
        if (Input.GetAxisRaw("Horizontal") > 0)

        {
            scale.x = 5; // そのまま（右向き）

            pos.x += speed;//speedを座標にぶっこむ
            

        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            scale.x = -5; // 反転する（左向き）

            pos.x += -speed;//speedをマイナスして座標にぶっこむ
            
        }

                    

        if (Input.GetAxisRaw("Vertical") > 0)
        {
            Z += speedz;//speedzを座標にぶっこむ
           

        }
        else if (Input.GetAxisRaw("Vertical") < 0)
        {
            Z += -speedz;//speedzマイナスして座標にぶっこむ
           
        }


       

        transform.localScale = scale;




        //ジャンプ挙動

        if (Input.GetKeyDown(KeyCode.Space))//スペースおしたらジャンプ   
        {
            jumpstate = true;
            Y += jumpshosoku;
            

        }

        if (jumpstate == true)
        {
            Y += speedy;
            Y += speedy += gravity;
           
        }


        pos.y = Z + Y;
        transform.position = pos;
        if (Y <= 0)//着地したらジャンプフラグをとめる＆speedyもとにもどす
        {
            jumpstate = false;
            speedy = 0.15f;
        }









        //うまいさんのぱくってアニメーション用領域つくる
        if (!jumpstate)
        {
            if (Input.GetAxisRaw("Horizontal") > 0 || Input.GetAxisRaw("Horizontal") < 0 || Input.GetAxisRaw("Vertical") > 0 || Input.GetAxisRaw("Vertical") < 0)
            {
                animator.Play("Walk");
            }
            else
            {
                animator.Play("Stand");

            }

        }

        else
        {
            if (jumpstate)
            {
                animator.Play("Jump");
            }
        }
       

       

    }
}
