using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : Singleton<QuestManager>
{
    public int QuestClearValueNum = 0; //����Ʈ Ŭ���� Ƚ��

    public void QuestClear()
    {
        GameObject QPanel = UiUtils.GetUI<QuestPanel>().gameObject;
    
        QPanel.SetActive(false);
        QuestClearValueNum++;

    }

    public int ClearValue()
    {
        return QuestClearValueNum;
    }

}
