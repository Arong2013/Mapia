using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrapMaker : Actor
{
    bool isMoveable = true;
    public TrapMarking trap_marking; //�̰Ŵ� Ʈ�� ����� ��� ���� ������Ʈ�� �־ Ű���� ������ �� ������ ���� �� ��������

    bool CanSetTrap = false; //������ ��ġ�� �� �ִ� ��ġ�ΰ�?

    public GameObject trap; // Ű���� ���� �� �ش� ������ ��ġ ������ �� ��ġ��
    public List<GameObject> trapList = new List<GameObject>();

    public override void Awake()
    {
        base.Awake();
    }


    protected override void Start()
    {
        base.Start();

        //� ����� �߰��ؾ� ���� �̸� ���ؾ���
        //Ʈ���� ������� �����ٰ����� ���� ������ �����ؿ�

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

    void SetTrap() //Ʈ�� ��ġ �޼���
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


        //    //Ʈ����ġ Ʈ���� 


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
                  RB.velocity = movement;
        PV.RPC(nameof(FlipXRPC), RpcTarget.AllBuffered, moveHorizontal);
        }
    }
}
