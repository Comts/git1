using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameArea : MonoBehaviour
{
    
    private void OnTriggerExit(Collider other)
    {
        other.gameObject.SetActive(false);//상단 체크박스
        //Destroy(other);//컴포넌트 제거 Collider만 제거됨. other.gameObject사용하면 삭제.  프레임 떨어짐.
        // other.enabled = false; 컴포넌트 체크박스 enabled    로직이 복잡해져서 안쓰는게 좋음
    }
}
