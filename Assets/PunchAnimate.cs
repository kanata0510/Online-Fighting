using UnityEngine;

public class PunchAnimate : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private static readonly int Right = Animator.StringToHash("Right");

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _animator.SetTrigger(Right);
        }
    }
}
