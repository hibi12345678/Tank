using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �����L���O�̃T���v��
/// </summary>
public class RankingSample : MonoBehaviour
{

    [SerializeField]
    private Text _nameText = default;

    [SerializeField]
    private Text _scoreText = default;

    [SerializeField]
    private Text _rankingText = default;

    //=================================================================================
    //���[�U��
    //=================================================================================

    /// <summary>
    /// ���[�U�����X�V����
    /// </summary>
    public void UpdateUserName()
    {
        //���[�U�����w�肵�āAUpdateUserTitleDisplayNameRequest�̃C���X�^���X�𐶐�
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = _nameText.text
        };

        //���[�U���̍X�V
        Debug.Log($"���[�U���̍X�V�J�n");
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnUpdateUserNameSuccess, OnUpdateUserNameFailure);
    }

    //���[�U���̍X�V����
    private void OnUpdateUserNameSuccess(UpdateUserTitleDisplayNameResult result)
    {
        //result.DisplayName�ɍX�V������̃��[�U���������Ă�
        Debug.Log($"���[�U���̍X�V���������܂��� : {result.DisplayName}");
    }

    //���[�U���̍X�V���s
    private void OnUpdateUserNameFailure(PlayFabError error)
    {
        Debug.LogError($"���[�U���̍X�V�Ɏ��s���܂���\n{error.GenerateErrorReport()}");
    }

 

    //=================================================================================
    //�X�R�A
    //=================================================================================

    /// <summary>
    /// �X�R�A(���v���)���X�V����
    /// </summary>
    public void UpdatePlayerStatistics()
    {
        //UpdatePlayerStatisticsRequest�̃C���X�^���X�𐶐�
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>{
        new StatisticUpdate{
          StatisticName = "�����L���O�T���v��",   //�����L���O��(���v���)
          Value = int.Parse( _scoreText.text), //�X�R�A(int)
        }
      }
        };

        //���[�U���̍X�V
        Debug.Log($"�X�R�A(���v���)�̍X�V�J�n");
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnUpdatePlayerStatisticsSuccess, OnUpdatePlayerStatisticsFailure);
    }

    //�X�R�A(���v���)�̍X�V����
    private void OnUpdatePlayerStatisticsSuccess(UpdatePlayerStatisticsResult result)
    {
        Debug.Log($"�X�R�A(���v���)�̍X�V���������܂���");
    }

    //�X�R�A(���v���)�̍X�V���s
    private void OnUpdatePlayerStatisticsFailure(PlayFabError error)
    {
        Debug.LogError($"�X�R�A(���v���)�X�V�Ɏ��s���܂���\n{error.GenerateErrorReport()}");
    }


    //=================================================================================
    //�����L���O�擾
    //=================================================================================

    /// <summary>
    /// �����L���O(���[�_�[�{�[�h)���擾
    /// </summary>
    public void GetLeaderboard()
    {
        //GetLeaderboardRequest�̃C���X�^���X�𐶐�
        var request = new GetLeaderboardRequest
        {
            StatisticName = "�����L���O�T���v��", //�����L���O��(���v���)
            StartPosition = 0,                 //���ʈȍ~�̃����L���O���擾���邩
            MaxResultsCount = 3                  //�����L���O�f�[�^�������擾���邩(�ő�100)
        };

        //�����L���O(���[�_�[�{�[�h)���擾
        Debug.Log($"�����L���O(���[�_�[�{�[�h)�̎擾�J�n");
        PlayFabClientAPI.GetLeaderboard(request, OnGetLeaderboardSuccess, OnGetLeaderboardFailure);
    }

    //�����L���O(���[�_�[�{�[�h)�̎擾����
    private void OnGetLeaderboardSuccess(GetLeaderboardResult result)
    {
        Debug.Log($"�����L���O(���[�_�[�{�[�h)�̎擾�ɐ������܂���");

        //result.Leaderboard�Ɋe���ʂ̏��(PlayerLeaderboardEntry)�������Ă���
        _rankingText.text = "";
        foreach (var entry in result.Leaderboard)
        {
            _rankingText.text += $"\n���� : {entry.Position}, �X�R�A : {entry.StatValue}, ���O : {entry.DisplayName}, ID : {entry.PlayFabId}";
        }
    }

    //�����L���O(���[�_�[�{�[�h)�̎擾���s
    private void OnGetLeaderboardFailure(PlayFabError error)
    {
        Debug.LogError($"�����L���O(���[�_�[�{�[�h)�̎擾�Ɏ��s���܂���\n{error.GenerateErrorReport()}");
    }

}


