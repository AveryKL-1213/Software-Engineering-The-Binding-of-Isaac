using UnityEngine;
using System.Collections;

public class Buttle : MonoBehaviour {

    // Use this for initialization
    public static Buttle instance;
    Vector2 ori_Pos;//子弹位置
    Animator ani;
    public  bool IsVertical;//垂直眼泪
     
    void Awake()
    {
        instance = this;
        ani = transform.GetComponent<Animator> ( );
    }

	void Start () {
        ori_Pos = transform.GetComponent<RectTransform> ( ).position;//初始位置
	}
	
	void Update () {
        if ( ( Mathf.Abs(transform.position.x - ori_Pos.x) > 5 )&&!IsVertical )
        {
            transform.GetComponent<Rigidbody2D> ( ).gravityScale = 1f;
        }
        if ( ( ori_Pos.y-transform.position.y  ) >= 0.3f&&!IsVertical)
        {
            transform.GetComponent<Rigidbody2D> ( ).drag = 999;//发生碰撞，停止移动
            transform.GetComponent<CircleCollider2D> ( ).enabled = false;//将碰撞体消失
            ani.Play ("buttle_boom_ani");
        }
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        
        if ( c.transform.tag != "Player" && c.transform.tag != "Buttle" &&c.transform.parent.parent.name !="blood")
        {
            transform.GetComponent<Rigidbody2D> ( ).drag = 999;
            transform.GetComponent<CircleCollider2D> ( ).enabled = false;
            if ( ani == null )
            {
                Debug.Log ("null!");
            }
            ani.Play ("buttle_boom_ani");
        }
    }

    //销毁自己
    void destory_self()
    {
        Destroy (transform.gameObject, 1f);
    }


}
