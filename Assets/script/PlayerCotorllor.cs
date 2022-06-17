using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlayerCotorllor : MonoBehaviour
{
    [SerializeField]
    private float runSpeed;
    [SerializeField]
    private float playerRotateSpeed;
    [SerializeField]
    private float MaxSpeed;
    [SerializeField]
    public GameObject camraObject;
    [SerializeField]
    public bool isControllor;

    [Header("²y")]
    [SerializeField]
    private float kickRange;
    [SerializeField]
    private LayerMask ballLayer;
    [SerializeField]
    private float kickPower_x;
    [SerializeField]
    private float kickPower_y;
    [SerializeField]
    private GameObject PowerBar;
    [SerializeField]
    private float PowerFloat;

    [SerializeField]
    private GameManger gameManger;

    private Rigidbody rigidbody;
    [HideInInspector]public PhotonView pv;
    // Start is called before the first frame update
    private void Awake()
    {
        rigidbody = this.GetComponent<Rigidbody>();
        pv = GetComponent<PhotonView>();
        gameManger = GameObject.Find("GameManger").GetComponent<GameManger>();

        if (!pv.IsMine)
        {
            Destroy(transform.Find("CamraHere").gameObject);
            Destroy(rigidbody);
        }

        isControllor = false;
        camraObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (pv.IsMine && isControllor&& gameManger.startTime<=0)
        {
            Moveing();
            kickBall();
        } 
    }
    public void Moveing()
    {
        
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        rigidbody.velocity = transform.forward * runSpeed * vertical+ transform.right * runSpeed * horizontal;
        //rigidbody.AddForce(transform.forward*runSpeed* vertical);
        //rigidbody.AddForce(transform.right * runSpeed * horizontal);

        transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X")* playerRotateSpeed);
    }
    public void kickBall()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (PowerFloat < 2)
            {
                PowerFloat += Time.deltaTime;
                PowerBar.transform.localScale += new Vector3(Time.deltaTime, 0, 0);
            }
            
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Vector3 point = new Vector3(transform.position.x, transform.position.x, transform.position.z+0.3f);
            var ball = Physics.OverlapSphere(transform.position, kickRange, ballLayer);
            if (ball.Length > 0)
            {
                ball[0].gameObject.GetComponent<Rigidbody>().AddForce(transform.forward* kickPower_x* PowerFloat);
                ball[0].gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up* kickPower_y* PowerFloat);
            }
            PowerFloat = 0;
            PowerBar.transform.localScale = new Vector3(0, PowerBar.transform.localScale.y, PowerBar.transform.localScale.z);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, kickRange);
    }
}
