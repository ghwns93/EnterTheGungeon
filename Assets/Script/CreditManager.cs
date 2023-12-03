using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditManager : MonoBehaviour
{
    public Animator animator; // 애니메이터 컴포넌트에 대한 참조
    private float originalSpeed; // 원래의 애니메이션 속도를 저장
    public float speedMultiplier = 2.0f; // 속도가 증가하는 배율
    private bool hasAnimationEnded = false; // 애니메이션 종료 여부를 체크

    void Start()
    {
        // 초기 애니메이션 속도 저장
        originalSpeed = animator.speed;
    }

    void Update()
    {
        // SPACE 키가 눌려 있는지 확인
        if (Input.GetKey(KeyCode.Space))
        {
            // 애니메이션 속도를 2배로 증가
            animator.speed = originalSpeed * speedMultiplier;
        }
        else
        {
            // SPACE 키가 떼어지면 원래 속도로 복귀
            animator.speed = originalSpeed;
        }

        // 애니메이션이 끝났는지 체크
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0) && !hasAnimationEnded)
        {
            hasAnimationEnded = true;
            LoadTitleScene();
        }
    }

    void LoadTitleScene()
    {
        SceneManager.LoadScene("Title"); // 여기에 타이틀 씬의 이름을 넣으세요
    }
}