using UnityEngine;
using System.Collections;

public class MonsterCtrl : MonoBehaviour {

	private Transform	m_player;
	private GameCtrl	m_gameCtrl;
	
	// 맵 관련.
	private GridMove	m_grid_move;
	public Vector3		m_spawnPosition = new Vector3(100,0,100);
	
	// 점수. 
	public int				m_point = 300;
	
	// 음향.
	private AudioChannels	m_audio;
	public AudioClip 		m_attackSE;
	
	// 동작.
	public enum AI_TYPE {
		TRACER,
		AMBUSH,
		PINCER,
		RANDOM
	};

	private Transform	m_tracer;
	public AI_TYPE		m_aiType = AI_TYPE.TRACER;
	private STATE		m_state;
	private Transform	m_attackTarget;
	
	// 난이도.
	public float		m_baseSpeed = 2.1f;
	public float		m_speedUpPerLevel = 0.3f;
	private const int MAX_LEVEL = 5;
	
	enum STATE {
		NORMAL,
		DEAD,
	};


	protected Animator	m_animator;
	
	// Use this for initialization
	void	Awake()
	{

		m_grid_move = GetComponent<GridMove>();
		m_gameCtrl = FindObjectOfType(typeof(GameCtrl)) as GameCtrl;
		m_gameCtrl.AddObjectToList(gameObject);
		m_audio = FindObjectOfType(typeof(AudioChannels)) as AudioChannels;

		m_animator = GetComponent<Animator>();

		if(m_animator == null) {

			Debug.LogError("Can't find Animator component.");
		}
	}
	
	public void SetSpawnPosition(Vector3 pos)
	{
		m_spawnPosition = pos;
	}
	
	
	public void OnRestart()
	{
		m_state = STATE.NORMAL;
		transform.position = m_spawnPosition;
	}
		
	public void OnGameStart()
	{
		OnStageStart();
	}
	
	public void OnStageStart()
	{
		m_state = STATE.NORMAL;
		transform.position = m_spawnPosition;
		GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
		for (int enemyCnt = 0; enemyCnt < enemys.Length; enemyCnt++) {
			MonsterCtrl mc = enemys[enemyCnt].GetComponent<MonsterCtrl>();
			if (mc != null) {
				if (mc.m_aiType == MonsterCtrl.AI_TYPE.TRACER)
					m_tracer = mc.transform;
			}
		}
		
		m_player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	// AI
	public void OnGrid(Vector3 newPos)
	{
		if (m_state != STATE.NORMAL)
			return;
		switch (m_aiType) {
		case AI_TYPE.TRACER:
			Tracer(newPos);
			break;
		case AI_TYPE.AMBUSH:
			Ambush(newPos);
			break;
		case AI_TYPE.RANDOM:
			RandomAI(newPos);
			break;
		case AI_TYPE.PINCER:
			Pincer(newPos);
			break;
			
		}
	}
	
	//  이동 방향의 후보를 참고하여 이동 가능한 방향을 구한다.
	//  인수:
	//  first   제1 후보.
	//  second  제2 후보.
	//  반환값:
	//  이동 방향/ 이동 불가능하다면 Vector3.zero
	private Vector3 DirectionChoice(Vector3 first, Vector3 second)
	{
		// 제1 후보.
		// 제2 후보.
		// 제2 후보의 역방향.
		// 제1 후보의 역방향.
		//
		// 순서를 점검해 이동 가능한 방향으로 보낸다.

		// 제1 후보.
		if (!m_grid_move.IsReverseDirection(first) && 
		    !m_grid_move.CheckWall(first))
			return first;

		// 제2 후보.
		if (!m_grid_move.IsReverseDirection(second) && 
		    !m_grid_move.CheckWall(second))
			return second;
		
		first *= -1.0f;
		second *= -1.0f;
		// 제2 후보의 역방향.
		if (!m_grid_move.IsReverseDirection(second) && 
		    !m_grid_move.CheckWall(second))
			return second;
		// 제1 후보의 역방향.
		if (!m_grid_move.IsReverseDirection(first) && 
		    !m_grid_move.CheckWall(first))
			return first;
		
		return Vector3.zero;
	}
	
	private void Tracer(Vector3 newPos)
	{
		Vector3 newDirection1st,newDirection2nd;

		// 자신의 위치에서 플레이어의 위치로 향하는 벡터.
		Vector3 diff = m_player.position - newPos;
		
		// X, Z의 절대값이 큰 쪽을 선택한다.
		if (Mathf.Abs(diff.x) > Mathf.Abs(diff.z)) {
			newDirection1st = new Vector3(1,0,0) * Mathf.Sign(diff.x);
			newDirection2nd = new Vector3(0,0,1) * Mathf.Sign(diff.z);
		} else {
			newDirection2nd = new Vector3(1,0,0) * Mathf.Sign(diff.x);
			newDirection1st = new Vector3(0,0,1) * Mathf.Sign(diff.z);			
		}

		// 두 후보 중 이동 가능한 방향을 선택한다.
		Vector3 newDir = DirectionChoice(newDirection1st,newDirection2nd);

		if (newDir == Vector3.zero)
			m_grid_move.SetDirection(-m_grid_move.GetDirection());
		else
			m_grid_move.SetDirection(newDir);
	}
	
	private static float	AMBUSH_DISTANCE = 3.0f;

	private void Ambush(Vector3 newPos)
	{
		Vector3 newDirection1st,newDirection2nd;

		// 플레이어의 전방을 목표 위치로 한다.
		Vector3 diff = m_player.position + m_player.forward*AMBUSH_DISTANCE - newPos;

		if (Mathf.Abs(diff.x) > Mathf.Abs(diff.z)) {
			newDirection1st = new Vector3(1,0,0) * Mathf.Sign(diff.x);
			newDirection2nd = new Vector3(0,0,1) * Mathf.Sign(diff.z);
		} else {
			newDirection2nd = new Vector3(1,0,0) * Mathf.Sign(diff.x);
			newDirection1st = new Vector3(0,0,1) * Mathf.Sign(diff.z);			
		}
		
		Vector3 newDir = DirectionChoice(newDirection1st,newDirection2nd);
		if (newDir == Vector3.zero)
			m_grid_move.SetDirection(-m_grid_move.GetDirection());
		else
			m_grid_move.SetDirection(newDir);
		
	}
	
	private Vector3[] DIRECTION_VEC = new Vector3[4] {
		new Vector3(1,0,0),			// 오른쪽
		new Vector3(-1,0,0),		// 왼쪽
		new Vector3(0,0,1),			// 위쪽
		new Vector3(0,0,-1)			// 아래쪽
	};
	
	private const float DISTANCE_RANDOM_MOVE = 10.0f;
	
	private void RandomAI(Vector3 newPos)
	{
		Vector3 newDirection1st,newDirection2nd;
		Vector3 diff = m_player.position - newPos;
		if (diff.magnitude < DISTANCE_RANDOM_MOVE) {
			int r = Random.Range(0,4);

			// 가로와 세로가 함께 선정되도록.
			newDirection1st = DIRECTION_VEC[r];
			newDirection2nd = DIRECTION_VEC[(r+2)%4];

			if (Random.value > 0.5f)
				newDirection2nd *= -1.0f;

			Vector3 newDir = DirectionChoice(newDirection1st,newDirection2nd);
			if (newDir == Vector3.zero)
				m_grid_move.SetDirection(-m_grid_move.GetDirection());
			else
				m_grid_move.SetDirection(newDir);
		} else
			Tracer(newPos);
	
	}
	
	private void Pincer(Vector3 newPos)
	{
		Vector3 newDirection1st,newDirection2nd;

		Vector3 diff;
		if (m_tracer == null)
			diff = m_player.position - newPos;
		else
			diff = m_player.position * 2 - m_tracer.position - newPos;
		
		
		if (Mathf.Abs(diff.x) > Mathf.Abs(diff.z)) {
			newDirection1st = new Vector3(1,0,0) * Mathf.Sign(diff.x);
			newDirection2nd = new Vector3(0,0,1) * Mathf.Sign(diff.z);
		} else {
			newDirection2nd = new Vector3(1,0,0) * Mathf.Sign(diff.x);
			newDirection1st = new Vector3(0,0,1) * Mathf.Sign(diff.z);			
		}
		
		Vector3 newDir = DirectionChoice(newDirection1st,newDirection2nd);
		if (newDir == Vector3.zero)
			m_grid_move.SetDirection(-m_grid_move.GetDirection());
		else
			m_grid_move.SetDirection(newDir);
		
	}
	
	// 난이도를 배정한다.
	// 인수:
	// difficulty   난이도.
	public void SetDifficulty(int difficulty)
	{
		if (difficulty > MAX_LEVEL)
			difficulty = MAX_LEVEL;
		m_grid_move.SPEED = m_baseSpeed+difficulty*m_speedUpPerLevel;
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (m_state != STATE.NORMAL)
			return;
		
		PlayerController player = other.GetComponent<PlayerController>();
		if (player != null) {
			player.Encount(transform);
			
		}
	}
	
	public void Damage()
	{
		SendMessage("OnDead");
		m_state = STATE.DEAD;

		Hud.get().AddScore(m_point);

		// 몬스터 점수의 표시는 몬스터를 공격한 순간=카메라가 줌인되었을 때.
		// 점수를 줌인되었을 때의 위치에 표시하면, 줌인을 마치고 되돌아왔을 때의 위치와 달라질 것이다.
		// 카메라가 줌인에서 돌아올 때의 위치와 차이가 생기지 않도록 카메라의 정면에 표시한다.
		Hud.get().CreateScoreBoard(Camera.main.transform.TransformPoint(Vector3.forward*1.0f), m_point);
		//Hud.get().CreateScoreBoard(this.transform.position, m_point);

		StartCoroutine("Dead");
	}

	public void OnStageClear()
	{
		StopAllCoroutines();
		SendMessage("OnDead");
		m_state = STATE.DEAD;
	}
	
	IEnumerator Dead()
	{
		yield return new WaitForSeconds(3.0f);
		m_state = STATE.NORMAL;
		SendMessage("OnRebone");
	}
	
	// 공격.
	public void Attack(Transform other)
	{
		m_attackTarget = other;
		StartCoroutine("AttackSub");
	}
	
		
	IEnumerator AttackSub()
	{
		m_gameCtrl.OnAttack();
		transform.LookAt(m_attackTarget.position);
		yield return null;

		m_animator.SetTrigger("begin_attack");
		m_audio.PlayOneShot(m_attackSE,1.0f,0.0f);
		yield return new WaitForSeconds(0.5f);

		m_attackTarget.SendMessage("Damage");
		m_animator.SetTrigger("begin_idle");
		m_gameCtrl.OnEndAttack(); 
	}
	
	// 발소리(무효).
	public void PlayStepSound(AnimationEvent ev)
	{
	}
}