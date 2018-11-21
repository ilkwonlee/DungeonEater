using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	public GameCtrl 		m_gameCtrl;			// 게임.
	public GameObject		m_sword;			// 플레이어의 칼.
	public GameObject		m_scoreBorad;		// 점수 표시 오브젝트. 
	private AudioChannels	m_audio;			// 오디오.  
	public AudioClip 		m_swordAttackSE;	// 공격SE.
	public GameObject 		SWORD_ATTACK_OBJ;	// 공격 범위 오브젝트.
	public GameObject GET_EFFECT;

	private bool 		m_equiped = false;  // 검을 장착 중. 
	private Transform 	m_target;  // 공격대상.
	
	// 득점.
	private const int POINT = 500;
	private const int COMBO_BONUS = 200;

	private int 	m_combo = 0;
	
	protected Animator	m_animator;

	// 초기화.
	void	Awake()
	{
		m_animator = GetComponent<Animator>();

		if(m_animator == null) {

			Debug.LogError("Can't find Animator component.");
		}
	}

	void 	Start()
	{
		m_equiped = false;
		m_sword.GetComponent<Renderer>().enabled = false;

		m_audio = FindObjectOfType(typeof(AudioChannels)) as AudioChannels;
		m_combo = 0;
	}
	
	// 스테이지 시작.
	void OnStageStart()
	{
		m_equiped = false;
		m_sword.GetComponent<Renderer>().enabled = false;
	}
	
	// 검을 주웠다.  
	void OnGetSword()
	{
		if (!m_equiped) {
			m_sword.GetComponent<Renderer>().enabled = true;
			m_equiped = true;
			m_animator.SetTrigger("begin_idle_sword");
		} else {
			int point = POINT + COMBO_BONUS * m_combo;

			Hud.get().CreateScoreBoard(this.transform.position, point);
			Hud.get().AddScore(point);
			m_combo++;
		}
	}
	
	void Remove()  
	{
		m_sword.GetComponent<Renderer>().enabled = false;
		m_equiped = false;

		// 레이어 애니메이션을 동시 재생하려면（좌우 팔을 휘두를 수 있도록）
		// 스테이트를 재설정한다(보완하지 않는다).
		m_animator.Play("idle", 0);
		m_animator.Play("idle", 1);

		m_combo = 0;
	}

	
	// 자동 공격한다.
	public void AutoAttack(Transform other)
	{
		if (m_equiped) {
			m_target = other;
			StartCoroutine("SwordAutoAttack");
		}
	}
	
	// 공격 가능한가？
	public bool CanAutoAttack()
	{
		if (m_equiped)
			return true;
		else
			return false;
	}
		
	
	IEnumerator SwordAutoAttack()
	{
		m_gameCtrl.OnAttack();

		// 돌아본다.
		Vector3		target_pos = m_target.transform.position;
		target_pos.y = transform.position.y;
		transform.LookAt(target_pos);
		yield return null;

		// 공격.
		m_animator.SetTrigger("begin_attack");
		yield return new WaitForSeconds(0.3f);

		m_audio.PlayOneShot(m_swordAttackSE,1.0f,0.0f);		
		yield return new WaitForSeconds(0.2f);

		Vector3 projectilePos;
		projectilePos = transform.position + transform.forward * 0.5f;
		Instantiate(SWORD_ATTACK_OBJ,projectilePos,Quaternion.identity);
		yield return null;

		// 공격 효과
		Object geteffect = Instantiate(GET_EFFECT,transform.position,Quaternion.identity);
		Destroy(geteffect,1.0f);

		// 방향을 원래대로 되돌린다.
		Remove();
		m_gameCtrl.OnEndAttack();
	}
}