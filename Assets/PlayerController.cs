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

    public float Speedy = 0.6f;//ジャンプ加速値
    public float jumpshosoku = 0.15f;
    public float gravity = -0.04f;//重力常にかけるよう

    public bool jumpstate = false;//ジャンプ状態用
    public float Jumpkoudochousei = 0;//小ジャンプに使う変数
    
    public bool Squatstate = false;//屈伸モーション
    public float Squatkouchoku = 0;//屈伸硬直のための変数
    

    Animator animator;//アニメーションの使い方わからんので参考書のうつし

    // Start is called before the first frame update
    void Start()
    {
        this.animator = GetComponent<Animator>();//アニメーション系は参考書の写し
        
       
    }
    // Update is called once per frame
    void Update()
    {
       
       

        


        Vector3 scale = transform.localScale;


        if (!Squatstate)//着地硬直は動けない
        {
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

        }//ここまで着地硬直でうごけない


        if (jumpstate == true)//ジャンプ状態　ジャンプ加速数値と重力を足す
        {
            
            Y += Speedy += gravity;

            if (Speedy>0&&Input.GetKeyUp(KeyCode.Space))//ジャンプ上昇中にボタン話すとジャンプ加速値が０になる
            {
                Speedy = 0f;
            }



            
            if (Y <= -0.1)//着地したらジャンプフラグをとめる＆speedyもとにもどす＆着地とたちモーションで絵柄にずれがでるから故意に着地はめりこませる
            {
                jumpstate = false;
                Speedy = 0.6f;
                Squatstate = true;
                Jumpkoudochousei = 0;
            }
        }

        if(Squatstate)//着地硬直の屈伸モーション
        {
            Squatkouchoku += Time.deltaTime;

            if (Squatkouchoku > 0.2f)
            {
                Squatstate = false;
                Squatkouchoku = 0;
                Y = 0;//硬直とけたらめりこんだ分をもとにもどす
            }
        }



        pos.y = Z + Y;
        transform.position = pos;



        //うまいさんのぱくってアニメーション用領域つくる
        if (!jumpstate&&!Squatstate)
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

            else if (Squatstate)
            {
                animator.Play("Squat");
            }


        }
       

       

    }
}
