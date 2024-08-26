using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ターゲットに振り向くスクリプト
/// </summary>
internal class Look : MonoBehaviour
{
    // 自身のTransform
    [SerializeField] private Transform _self;

    // ターゲットのTransform
    Vector3 mousePos, pos;

    private void Update()
    {
        mousePos = Input.mousePosition;
        pos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, mousePos.z));
        //Vector3 worldPos = Camera.main.ViewportToWorldPoint(pos);
        pos.y = _self.transform.position.y;
        pos.z = pos.z + 20.0f;
        pos.z = pos.z * 1.557238f;
        
        // ターゲットの方向に自身を回転させる
        _self.LookAt(pos);
    }
}
