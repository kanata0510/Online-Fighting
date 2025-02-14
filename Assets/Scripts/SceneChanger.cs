using System.Collections;
using Photon.Realtime;
using Quantum;
using UnityEngine;
using UnityEngine.SceneManagement;
using Button = UnityEngine.UI.Button;
using Input = UnityEngine.Input;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Animator animator;
    [SerializeField] private float transitionDurationTime = 1f;

    private bool once = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button.onClick.AddListener(SceneChange);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            SceneChange();
        }
    }

    void SceneChange()
    {
        if (once) return;
        once = true;
        StartCoroutine(SceneChangeCoroutine());
    }

    IEnumerator SceneChangeCoroutine()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("Menu");
        operation.allowSceneActivation = false;
        animator.gameObject.SetActive(true);
        animator.Play("Fade");
        yield return new WaitForSeconds(transitionDurationTime);
        QuantumRunner.Default.NetworkClient.OpLeaveRoom(true);
        QuantumRunner.Default.NetworkClient.State = ClientState.Disconnecting;
        QuantumRunner.Default.NetworkClient.RealtimePeer.Disconnect();
        QuantumRunner.Default.NetworkClient.RealtimePeer.IsSimulationEnabled = false;
        QuantumRunner.Default.Game.RemoveAllPlayers();
        operation.allowSceneActivation = true;
    }
}
