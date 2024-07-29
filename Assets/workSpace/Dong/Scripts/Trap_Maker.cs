using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Trap_Maker : Actor
{
    bool isMoveable = true;
    public TrapMarking trap_marking; //이거는 트랩 만드는 사람 하위 오브젝트로 넣어서 키보드 눌렀을 때 켜지고 뗐을 때 꺼지도록

    bool CanSetTrap = false; //함정을 설치할 수 있는 위치인가?

    public GameObject trap; // 키보드 뗐을 때 해당 영역에 설치 가능할 시 설치함
    public List<GameObject> trapList = new List<GameObject>();

    public override void Awake()
    {
        base.Awake();
        statComponents.Add(new MovementStats());
        AddNode<MovementNode>(new MovementNode(this), true);
    }


    protected override void Start()
    {
        base.Start();

        //어떤 기능을 추가해야 할지 미리 정해야함
        //트랩을 어떤식으로 보여줄건지에 대한 정보가 부족해요

    }

    protected override void Update()
    {
        base.Update();
        //SetTrap();

        if(Input.GetKeyDown(KeyCode.Space))
        {
            
            if(trap_marking.gameObject.activeSelf == false)
            {
                trap_marking.gameObject.SetActive(true);
            }
            else
            {
                trap_marking.gameObject.SetActive(false);
            }
            
        }

        if (Input.GetMouseButtonDown(0))
        {
            if(trap_marking.GetBool() && trap_marking.gameObject.activeSelf == true)
            {
                if (PV.IsMine)
                {
                    PV.RPC(nameof(Maketrap), RpcTarget.All, new Vector2(trap_marking.transform.position.x,trap_marking.transform.position.y));
                }
                trap_marking.gameObject.SetActive(false);
            }
        }


    }

    void SetTrap() //트랩 설치 메서드
    {



        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    trap_marking.gameObject.SetActive(true);
        //    Debug.Log(PV.IsMine);
        //}

        //if (Input.GetKey(KeyCode.Space))
        //{
        //    if (Input.GetKeyDown(KeyCode.UpArrow))
        //    {
        //        trap_marking.transform.position = new Vector3(transform.position.x, transform.position.y + 1, 0);
        //    }

        //    if (Input.GetKeyDown(KeyCode.DownArrow))
        //    {
        //        trap_marking.transform.position = new Vector3(transform.position.x, transform.position.y - 1, 0);

        //    }

        //    if (Input.GetKeyDown(KeyCode.RightArrow))
        //    {
        //        trap_marking.transform.position = new Vector3(transform.position.x + 1, transform.position.y, 0);

        //    }

        //    if (Input.GetKeyDown(KeyCode.LeftArrow))
        //    {
        //        trap_marking.transform.position = new Vector3(transform.position.x - 1, transform.position.y, 0);

        //    }

        //}

        //if (Input.GetKeyUp(KeyCode.Space))
        //{
        //    if (trap_marking.GetBool())
        //    {
        //        if (PV.IsMine)
        //        {
        //            PV.RPC(nameof(Maketrap), RpcTarget.All);
        //        }

                
        //        //trapList.Add(Instantiate(trap, trap_marking.transform.position, Quaternion.identity));
        //    }
        //    trap_marking.gameObject.SetActive(false);

        //    //trapList.Add(trap, )


        //    //트랩설치 트랩은 


       // }

    }

    [PunRPC]
    public void Maketrap(Vector2 trapPos)
    {
        trapList.Add(Instantiate(trap,trapPos, Quaternion.identity));
    }


    public override void Move()
    {
        if (isMoveable)
        {
            //print("잘되요");
            CallAct<MovementNode>(new MovementNode(movement));
            PV.RPC(nameof(FlipXRPC), RpcTarget.AllBuffered, moveHorizontal);
        }
    }
}
