using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPersonItemController : MonoBehaviour {
    public Transform tfScoreParent;

    const string strNumPath = "Prefabs/UI/";
    const int MaxScore = 4;

    void Update()
    {

    }

    void TransformScore(int iScore)
    {
        foreach (Transform tfChild in tfScoreParent)
        {
            Destroy(tfChild.gameObject);
        }

        int iTenThousand = iScore / 10000;
        int iThousand = iScore / 1000%10;
        int iHundred = iScore / 100%100%10;
        int iTen = iScore / 10%1000%100%10;
        int iSingle = iScore % 10000%1000 % 100 % 10;

        if (iTenThousand > 0)
        {
            for (int i = 0; i < MaxScore; i++)
            {
                initNumber(9);
            }
        }
        else
        {
            if (iThousand>0)
            {
                initNumber(iThousand);
            }

            if (iHundred > 0)
            {
                initNumber(iHundred);
            }

            if (iTen > 0)
            {
                initNumber(iTen);
            }

            initNumber(iSingle);
        }
    }

    void initNumber(int num)
    {
        GameObject gNum = Resources.Load(strNumPath + num) as GameObject;

        GameObject.Instantiate(gNum,tfScoreParent);
    }
}
