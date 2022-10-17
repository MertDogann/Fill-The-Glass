using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glass : MonoBehaviour
{
    
    private CapsuleCollider capsuleCol;
    int a = 0;
    public SkinnedMeshRenderer bodySkinnedMeshRenderer;
    private int dressValue = 0;
    private Animator anim;
    private void Awake()
    {
        capsuleCol = gameObject.GetComponent<CapsuleCollider>();
        bodySkinnedMeshRenderer = bodySkinnedMeshRenderer.gameObject.GetComponent<SkinnedMeshRenderer>();
        bodySkinnedMeshRenderer.SetBlendShapeWeight(0, dressValue);
        anim = gameObject.GetComponent<Animator>();
        

    }


    private void Update()
    {
        bodySkinnedMeshRenderer.SetBlendShapeWeight(0, dressValue);
    }

    private void OnParticleCollision(GameObject other)
    {
        if (dressValue < 100)
        {
            GameManager.Current.changescore(10);
            dressValue += 10;
            Debug.Log(dressValue);
        }

        a++;
        if (a > 10)
        {
            capsuleCol.isTrigger = true;

        }
        anim.SetBool("FillBool", true);
        StartCoroutine(ScaleWait());
    }

    IEnumerator ScaleWait()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("FillBool", false);
    }
}
