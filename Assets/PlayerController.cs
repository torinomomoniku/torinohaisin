using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector3 pos; //描画使用
    public float Y;//ジャンプと奥行き移動用変数
    public float X;
    public float Z;
  
    public float speed = 0.05f;//横移動スピード
    public float speedz = 0.02f;//縦軸移動すぴーと

    

    public float Speedy = 0.3f;//ジャンプ加速値
    public float jumpshosoku = 0.15f;
    public float gravity = -0.02f;//重力常にかけるよう

    public bool Jumpstate = false;//ジャンプ状態用
    public float Jumpkoudochousei = 0;//小ジャンプに使う変数
    
    public bool Squatstate = false;//屈伸モーション
    public float Squatkouchoku = 0;//屈伸硬直のための変数

    public bool MigiDashstate = false;　//左右ダッシュ用
    public bool HidariDashstate = false;
    public float MigiDashjunbi = 0;//ダッシュ用変数
    public float HidariDashjunbi = 0;



    public bool Kickstate = false;//キック用
    public float Kickkouchoku = 0;


    public float Kougekisuberi = 0.06f;//ダッシュ攻撃したときに多少滑らせるための変数


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


        if (!Squatstate&&!Kickstate)//着地＆キック硬直は動けない
        {
            //上下移動
            if (Input.GetAxisRaw("Horizontal") > 0&&!MigiDashstate)//ダッシュ中に通常歩きできないようにする

            {
                
                scale.x = 5; // そのまま（右向き）

                X += speed;//speedを座標にぶっこむ


            }
         
            else if (Input.GetAxisRaw("Horizontal") < 0&&!HidariDashstate)
            {
               
                scale.x = -5; // 反転する（左向き）

                X += -speed;//speedをマイナスして座標にぶっこむ

            }



            if (Input.GetAxisRaw("Vertical") > 0)
            {
                Z += speedz;//speedzを座標にぶっこむ
                MigiDashjunbi = 0;//上下おしたらダッシュ準備解除
                HidariDashjunbi = 0;

            }
            else if (Input.GetAxisRaw("Vertical") < 0)
            {
                Z += -speedz;//speedzマイナスして座標にぶっこむ
                MigiDashjunbi = 0;//上下おしたらダッシュ準備解除
                HidariDashjunbi = 0;
            }




            transform.localScale = scale;




            //ジャンプ挙動
                      
            if (Input.GetKeyDown(KeyCode.Space))//スペースおしたらジャンプ   
            {
                Jumpstate = true;
                Y += jumpshosoku;


            }


            //キック挙動　硬直解除時間用変数はアニメーションのifの中にぶっこんだ
            if (!Jumpstate&&Input.GetKeyDown(KeyCode.Q))
            {
                Kickstate = true;
              
            }


                                 
        }//ここまで着地硬直でうごけない








        //ダッシュ準備と挙動
        if (Input.GetKeyDown(KeyCode.RightArrow))//右一回おすと変数にプラス1
        　
        {
            MigiDashjunbi += 1;
            HidariDashjunbi = 0;//右おしたら左ダッシュ準備解除
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))//左に1回おすと変数プラス１
        {
            HidariDashjunbi += 1;
            MigiDashjunbi = 0;//左おしたら右ダッシュ準備解除
        }


            if (MigiDashstate)//右ダッシュ状態
        {
            X += 0.1f;
            if (Kickstate)//ダッシュ状態でキックしたときに多少すべるのを表現したい
            {
                X -= Kougekisuberi;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MigiDashjunbi = 0;
                MigiDashstate = false;
            }
        }
        else if (HidariDashstate)//左ダッシュ状態
        {
            X -= 0.1f;
            if (Kickstate)//ダッシュ状態でキックしたときに多少すべるのを表現したい
            {
                X += Kougekisuberi;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                HidariDashjunbi = 0;
                HidariDashstate = false;
            }
        }
                       
        if (MigiDashjunbi > 1.9)//ダッシュ準備状態の変数管理
        {
            MigiDashstate = true;
        }
        else if (HidariDashjunbi > 1.9)
        {
            HidariDashstate = true;
        }
        

     






        if (Jumpstate == true)//ジャンプ状態　ジャンプ加速数値と重力を足す
        {
            
            Y += Speedy += gravity;

            if (Speedy>0&&Input.GetKeyUp(KeyCode.Space))//ジャンプ上昇中にボタン話すとジャンプ加速値が０になる
            {
                Speedy = 0f;
            }



            
            if (Y <= -0.1)//着地したらジャンプフラグをとめる＆speedyもとにもどす＆着地とたちモーションで絵柄にずれがでるから故意に着地はめりこませる
            {
                Jumpstate = false;
                Speedy = 0.3f;
                Squatstate = true;
                Jumpkoudochousei = 0;
            }
        }

        if(Squatstate)//着地硬直の屈伸モーション
        {
            Squatkouchoku += Time.deltaTime;

            MigiDashstate = false;//着地でダッシュ解除
            MigiDashjunbi = 0;
            HidariDashstate = false;
            HidariDashjunbi = 0;

            if (Squatkouchoku > 0.2f)
            {
                Squatstate = false;
                Squatkouchoku = 0;
                Y = 0;//硬直とけたらめりこんだ分をもとにもどす

                
            }
        }



        if (Kickkouchoku >0.2)//キック硬直→解除用 ダッシュも解除＆ダッシュキックはちょっとすべらせたいのでここにいれた
        {
            Kickstate = false;
            Kickkouchoku = 0;
            MigiDashjunbi = 0;//ダッシュ解除
            HidariDashjunbi = 0;
            MigiDashstate = false;
            HidariDashstate = false;
        }













        pos.x = X;
        pos.y = Z + Y;
        transform.position = pos;



       






        //うまいさんのぱくってアニメーション用領域つくる
        if (!Jumpstate&&!Squatstate)
        {
            if (Kickstate)　//キックモーションと硬直解除時間用の変数
            {
                animator.Play("Kick");
                Kickkouchoku += Time.deltaTime;
            }



            else if (MigiDashstate||HidariDashstate)
            {
                animator.Play("Dash");
            }

            else if (Input.GetAxisRaw("Horizontal") > 0 || Input.GetAxisRaw("Horizontal") < 0 || Input.GetAxisRaw("Vertical") > 0 || Input.GetAxisRaw("Vertical") < 0)
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
            if (Jumpstate)
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
