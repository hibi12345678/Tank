using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �^�[�Q�b�g�ɐU������X�N���v�g
/// </summary>
internal class Look : MonoBehaviour
{
    // ���g��Transform
    [SerializeField] private Transform _self;

    // �^�[�Q�b�g��Transform
    Vector3 mousePos, pos;

    private void Update()
    {
        mousePos = Input.mousePosition;
        pos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, mousePos.z));
        //Vector3 worldPos = Camera.main.ViewportToWorldPoint(pos);
        pos.y = _self.transform.position.y;
        pos.z = pos.z + 20.0f;
        pos.z = pos.z * 1.557238f;
        
        // �^�[�Q�b�g�̕����Ɏ��g����]������
        _self.LookAt(pos);
    }
}
