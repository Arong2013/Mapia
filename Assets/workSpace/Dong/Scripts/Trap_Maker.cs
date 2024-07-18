using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Maker : Actor
{
    bool isMoveable = true;
    public TrapMarking trap_marking; //�̰Ŵ� Ʈ�� ����� ��� ���� ������Ʈ�� �־ Ű���� ������ �� ������ ���� �� ��������

    bool CanSetTrap = false; //������ ��ġ�� �� �ִ� ��ġ�ΰ�?

    public GameObject trap; // Ű���� ���� �� �ش� ������ ��ġ ������ �� ��ġ��
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

        //� ����� �߰��ؾ� ���� �̸� ���ؾ���
        //Ʈ���� ������� �����ٰ����� ���� ������ �����ؿ�

    }

    private void Update()
    {
        SetTrap();


    }

    void SetTrap() //Ʈ�� ��ġ �޼���
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            trap_marking.gameObject.SetActive(true);
            Debug.Log(PV.IsMine);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                trap_marking.transform.position = new Vector3(transform.position.x, transform.position.y + 1, 0);
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                trap_marking.transform.position = new Vector3(transform.position.x, transform.position.y - 1, 0);

            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                trap_marking.transform.position = new Vector3(transform.position.x + 1, transform.position.y, 0);

            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                trap_marking.transform.position = new Vector3(transform.position.x - 1, transform.position.y, 0);

            }

        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (trap_marking.GetBool())
            {
                if (PV.IsMine)
                {
                    PV.RPC(nameof(Maketrap), RpcTarget.All);
                }

                
                //trapList.Add(Instantiate(trap, trap_marking.transform.position, Quaternion.identity));
            }
            trap_marking.gameObject.SetActive(false);

            //trapList.Add(trap, )


            //Ʈ����ġ Ʈ���� 


        }

    }

    [PunRPC]
    public void Maketrap()
    {
        trapList.Add(Instantiate(trap, trap_marking.transform.position, Quaternion.identity));
    }


    public override void Move()
    {
        if (isMoveable)
        {
            CallAct<MovementNode>(new MovementNode(movement));
            PV.RPC(nameof(FlipXRPC), RpcTarget.AllBuffered, moveHorizontal);
        }
    }
}
