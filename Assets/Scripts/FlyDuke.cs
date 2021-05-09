using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FlyDuke : MonoBehaviour {
    Vector2 forward_move;
    Animator ani;
    float time = 5;//攻击频率
    bool IsCanMove = true;
    public float HP;//总血量
    public float Hp_Present;//当前血量
   

    void Awake()
    {
        ani = transform.GetComponent<Animator> ( );
        GameObject.Find ("Canvas").transform.Find ("Player").gameObject.SetActive (false);
    }

	// Use this for initialization
	void Start () {
        forward_move = Random.insideUnitCircle.normalized * 0.5f;
        HP = 20;//给苍蝇公爵的总血量赋值
        Hp_Present = 20;//苍蝇公爵当前的血量
    }
	
	// Update is called once per frame
	void Update () {
        if ( IsCanMove )
        {
            Move ( );
        }

        //Boss血条Slider的更新
        GameObject.Find ("Canvas").transform.Find ("All_UI").Find ("Boss_HP").GetComponent<Slider> ( ).value
            = Mathf.Lerp (GameObject.Find ("Canvas").transform.Find ("All_UI").Find ("Boss_HP").GetComponent<Slider> ( ).value, Hp_Present / HP,Time.deltaTime * 3);

    }

    void Move()
    {
        transform.Translate (forward_move * Time.deltaTime * 5);
        Attack_Start ( );
    }

    //反弹
    void OnCollisionStay2D ( Collision2D c )
    {
        if ( forward_move.x > 0 && forward_move.y > 0 )
        {
            //右墙,y不变
            if ( c.transform.name == "wall_r_u" || c.transform.name == "wall_r_d" || c.transform.name == "door_right" )
            {
                forward_move = new Vector2 (-forward_move.x, forward_move.y);
            }
            //上墙，x不变
            else if ( c.transform.name == "wall_u_r" || c.transform.name == "wall_u_l" || c.transform.name == "door_up" )
            {
                forward_move = new Vector2 (forward_move.x, -forward_move.y);
            }
        }

        else if ( forward_move.x < 0 && forward_move.y > 0 )
        {
            //上墙，X不变，Y相反
            if ( c.transform.name == "wall_u_r" || c.transform.name == "wall_u_l" || c.transform.name == "door_up" )
            {
                forward_move = new Vector2 (forward_move.x, -forward_move.y);
            }
            //左墙，Y不变，X相反
            else if ( c.transform.name == "wall_l_u" || c.transform.name == "wall_l_d" || c.transform.name == "door_right" )
            {
                forward_move = new Vector2 (-forward_move.x, forward_move.y);
            }
        }

        if ( forward_move.x < 0 && forward_move.y < 0 )
        {
            //下墙，X不变，Y相反
            if ( c.transform.name == "wall_d_r" || c.transform.name == "wall_d_l" || c.transform.name == "door_down" )
            {
                forward_move = new Vector2 (forward_move.x, -forward_move.y);
            }
            //左墙，Y不变，X相反
            else if ( c.transform.name == "wall_l_u" || c.transform.name == "wall_l_d" || c.transform.name == "door_left" )
            {
                forward_move = new Vector2 (-forward_move.x, forward_move.y);
            }
        }

        else if ( forward_move.x > 0 && forward_move.y < 0 )
        {
            //如果Boss撞到下墙上面，X轴不变，Y相反
            if ( c.transform.name == "wall_d_r" || c.transform.name == "wall_d_l" || c.transform.name == "door_down" )
            {
                forward_move = new Vector2 (forward_move.x, -forward_move.y);
            }
            //如果Boss撞到右墙上面，Y不变，X相反
            else if ( c.transform.name == "wall_r_u" || c.transform.name == "wall_r_d" || c.transform.name == "door_right" )
            {
                forward_move = new Vector2 (-forward_move.x, forward_move.y);
            }
        }
    }

    void Attack_Start()
    {
        time += Time.deltaTime;
        if ( time > 5)//苍蝇公爵的攻击间隔
        {
            ani.SetBool ("IsAttack", true);
            time = 0;
        }
    }

    void Attack_Fin ( )
    {
        ani.SetBool ("IsAttack", false);
    }

    /// <summary>
    /// 生成子苍蝇
    /// </summary>
    void Start_Attack()
    {
        int fly_num = Random.Range (1, 4);
        for ( int i = 0; i < fly_num; i++ )
        {
            Room.instance.enemy_num [ Player_Info.instance.present_room ]++;
            GameObject go = ( GameObject )Instantiate (Resources.Load ("enemy/little_fly"));
            go.transform.parent = GameObject.Find ("Canvas").transform.Find ("All_Enemy");
            go.transform.GetComponent<RectTransform> ( ).localScale = new Vector3 (1, 1, 1);
            go.GetComponent<RectTransform> ( ).anchoredPosition = GameObject.Find ("Canvas").transform.Find ("All_Enemy").Find ("FlyDuke(Clone)").GetComponent<RectTransform> ( ).anchoredPosition - new Vector2 (0, 10);
        }
    }

    public void IsCanShow_Collider()
    {
        if ( transform.GetComponent<CircleCollider2D> ( ).enabled == false )
        {
            transform.GetComponent<CircleCollider2D> ( ).enabled = true;
        }
    }


    /// <summary>
    /// 怪物碰到子弹后检测碰撞
    /// </summary>
    /// <param name="c"></param>
    void OnTriggerEnter2D ( Collider2D c )
    {
        if ( c.transform.tag == "Buttle" )
        {
            Hp_Present--;
            if ( Hp_Present <= 0 )
            {
                transform.GetComponent<CircleCollider2D> ( ).enabled = false;
                ani.SetBool ("IsDeath", true);
            }
        }
    }

    /// <summary>
    /// 怪物死亡后，collider消失
    /// </summary>
    void Disapper_Collider()
    {
        transform.GetComponent<CircleCollider2D> ( ).enabled = false;
    }

    /// <summary>
    /// 动画帧调用，死亡动画完了调用
    /// </summary>
    void DestroySelf()
    {
        Room.instance.enemy_num [ Player_Info.instance.present_room ]--;

        Destroy (transform.gameObject);
    }

    /// <summary>
    /// 动画帧调用，怪物死后停止移动
    /// </summary>
    void IsNotCanMove ( )
    {
        IsCanMove = false;
    }
}
