using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using Cinemachine;

public class PlayerScript : MonoBehaviourPunCallbacks, IPunObservable
{
    public Rigidbody2D RB;
    public Animator AN;
    public SpriteRenderer SR;
    public PhotonView PV;
    public Text NickNameText;
    public Image HealthImage;

    public int itemChoice = 0; //아이템 고를때 사용

    bool isGround;
    Vector3 curPos;



    void Awake()
    {
        // 닉네임
        NickNameText.text = PV.IsMine ? PhotonNetwork.NickName : PV.Owner.NickName;
        NickNameText.color = PV.IsMine ? Color.green : Color.red;

        if (PV.IsMine)
        {
            // 2D 카메라
            var CM = GameObject.Find("CMCamera").GetComponent<CinemachineVirtualCamera>();
            CM.Follow = transform;
            CM.LookAt = transform;
        }
    }


    void Update()
    {
        if (PV.IsMine)
        {
            // ← → 이동
            float axis = Input.GetAxisRaw("Horizontal");
            RB.velocity = new Vector2(4 * axis, RB.velocity.y);

            if (axis != 0)
            {
                AN.SetBool("walk", true);
                PV.RPC("FlipXRPC", RpcTarget.AllBuffered, axis); // 재접속시 filpX를 동기화해주기 위해서 AllBuffered
            }
            else AN.SetBool("walk", false);


            // ↑ 점프, 바닥체크
            isGround = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(0, -0.5f), 0.07f, 1 << LayerMask.NameToLayer("Ground"));
            AN.SetBool("jump", !isGround);
            if (Input.GetKeyDown(KeyCode.UpArrow) && isGround) PV.RPC("JumpRPC", RpcTarget.All);


            // 스페이스 총알 발사
            if (Input.GetKeyDown(KeyCode.Space))
            {
                PhotonNetwork.Instantiate("Bullet", transform.position + new Vector3(SR.flipX ? -0.4f : 0.4f, -0.11f, 0), Quaternion.identity)
                    .GetComponent<PhotonView>().RPC("DirRPC", RpcTarget.All, SR.flipX ? -1 : 1);
                AN.SetTrigger("shot");
            }

            //인벤토리 열기 및 닫기
            if (Input.GetKeyDown(KeyCode.I))
            {
                if(UIManager.Instance.inventory.activeSelf == false)
                {
                    UIManager.Instance.inventory.SetActive(true);
                }
                else
                {
                    UIManager.Instance.inventory.SetActive(false);
                }
                
            }

            //아이템 고르기
            if(Input.GetKeyDown(KeyCode.Q))
            {
                if (itemChoice > 0) itemChoice--;
                else itemChoice = 2;

                UIManager.Instance.SetNowItem(itemChoice);
            }
            if(Input.GetKeyDown(KeyCode.E))
            {
                if (itemChoice < 2) itemChoice++;
                else itemChoice = 0;

                UIManager.Instance.SetNowItem(itemChoice);
            }

            if(Input.GetKeyDown(KeyCode.Tab))
            {
                UIManager.Instance.inventory.GetComponent<Inventory>().UseItem(itemChoice);
            }



        }
        // IsMine이 아닌 것들은 부드럽게 위치 동기화
        else if ((transform.position - curPos).sqrMagnitude >= 100) transform.position = curPos;
        else transform.position = Vector3.Lerp(transform.position, curPos, Time.deltaTime * 10);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Item"))
        {
            if(collision.GetComponent<Item_Script>().item != null)
            {
                //Debug.Log(collision.GetComponent<Item_Script>().item.Data.Name);
                
                UIManager.Instance.inventory.GetComponent<Inventory>().GetItem(collision.GetComponent<Item_Script>().item);
                UIManager.Instance.SetNowItem(itemChoice);
            }
            else
            {
                Debug.Log("nullnull");
            }
            
            //UIManager.Instance.Inventory.GetComponent<Inventory>().setitem(collision.GetComponent<ItemData>());
            Destroy(collision.gameObject);
        }
    }


    [PunRPC]
    void FlipXRPC(float axis) => SR.flipX = axis == -1;

    [PunRPC]
    void JumpRPC()
    {
        RB.velocity = Vector2.zero;
        RB.AddForce(Vector2.up * 700);
    }

    public void Hit()
    {
        HealthImage.fillAmount -= 0.1f;
        if (HealthImage.fillAmount <= 0)
        {
            GameObject.Find("Canvas").transform.Find("RespawnPanel").gameObject.SetActive(true);
            PV.RPC("DestroyRPC", RpcTarget.AllBuffered); // AllBuffered로 해야 제대로 사라져 복제버그가 안 생긴다
        }
    }

    [PunRPC]
    void DestroyRPC() => Destroy(gameObject);


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(HealthImage.fillAmount);
        }
        else
        {
            curPos = (Vector3)stream.ReceiveNext();
            HealthImage.fillAmount = (float)stream.ReceiveNext();
        }
    }

}
