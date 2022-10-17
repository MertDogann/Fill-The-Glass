using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishGlass : MonoBehaviour
{
    private CapsuleCollider capsuleCol;
    private Pistol pistol;
    int a = 0;
    public SkinnedMeshRenderer bodySkinnedMeshRenderer;
    private int dressValue = 0;
    [SerializeField] int xScore;
    private Animator anim;
    private Transform finishTransform;
    private void Awake()
    {
        capsuleCol = gameObject.GetComponent<CapsuleCollider>();
        bodySkinnedMeshRenderer = bodySkinnedMeshRenderer.gameObject.GetComponent<SkinnedMeshRenderer>();
        bodySkinnedMeshRenderer.SetBlendShapeWeight(0, dressValue);
        pistol = GameObject.Find("Pistol").GetComponent<Pistol>();
        anim = gameObject.GetComponent<Animator>();
        finishTransform = gameObject.GetComponent<Transform>();

    }

    private void Update()
    {
        bodySkinnedMeshRenderer.SetBlendShapeWeight(0, dressValue);
        if (dressValue>= 100)
        {
            GameManager.Current.multiplicationcore(xScore);
            pistol.finished = true;
            anim.SetBool("FillBool", true);

        }
    }
    private void OnParticleCollision(GameObject other)
    {
        if (dressValue < 100)
        {
            
            dressValue += 10;
            Debug.Log(dressValue);
            
            
        }

        a++;
        if (a > 10)
        {
            capsuleCol.isTrigger = true;

        }
    }


}
