using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageUIElement : MonoBehaviour
{
    [SerializeField]
    private Animator PlayerAnim;
    [SerializeField]
    private Animator CoworkerAnim;
    [SerializeField]
    private Transform CoworkerPos;
    private Transform Coworker;
    // Start is called before the first frame update
    public void PlayerActive(bool b)
    {
        PlayerAnim.gameObject.SetActive(b);
    }
    public void CoworkerActive(bool b)
    {
        CoworkerAnim.gameObject.SetActive(b);
    }
    private int mID;
    public void Init(int id,
                     Animator anim)
    {
        mID = id;
        CoworkerAnim = Instantiate(anim, CoworkerPos);
        CoworkerAnim.gameObject.SetActive(false);

    }
}
