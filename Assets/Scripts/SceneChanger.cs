using System.Collections;
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
        QuantumRunner.ShutdownAll();
        operation.allowSceneActivation = true;
    }
}
