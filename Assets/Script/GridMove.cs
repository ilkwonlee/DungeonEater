// 그리드 이동.
using UnityEngine;
using System.Collections;

public class GridMove : MonoBehaviour {
	// 일시정지.
	private enum PAUSE_TYPE {
		NONE,
		GAME_PAUSE,
		HITSTOP,
	};
	private PAUSE_TYPE  m_pause = PAUSE_TYPE.NONE;

	// 이동속도.
	public float SPEED = 1.0f;
	
	// 이동방향 벡터.  
	private Vector3		m_direction;
	private Vector3		m_move_vector;
	private Vector3		m_current_grid;
	
	// 충돌 점검.   
	private const float		HITCHECK_HEIGHT = 0.5f;
	private const int		HITCHECK_LAYER_MASK = 1 << 0;
	
	// Use this for initialization
	void Start () {
		m_move_vector = Vector3.zero;
		m_direction = Vector3.forward;
		m_pause = PAUSE_TYPE.NONE;
	}
	
	public void OnRestart()
	{
		m_move_vector = Vector3.zero;
		m_pause = PAUSE_TYPE.NONE;
	}
	
	public void OnGameStart()
	{
		m_move_vector = Vector3.zero;
		m_pause = PAUSE_TYPE.NONE;
	}
	
	public void OnStageStart()
	{
		m_move_vector = Vector3.zero;
		m_pause = PAUSE_TYPE.NONE;
	}
	
	public void OnDead()
	{
		m_pause = PAUSE_TYPE.GAME_PAUSE;
	}
	
	public void OnStageClear()
	{
		m_pause = PAUSE_TYPE.GAME_PAUSE;
	}
	
	public void OnRebone()
	{
		m_pause = PAUSE_TYPE.NONE;
	}

	// Update is called once per frame
	void	Update()
	{
		if(m_pause != PAUSE_TYPE.NONE) {

			m_move_vector = Vector3.zero;

		} else {
	
			// deltaTime이 너무 크면 벽을 침범하게 되므로
			// 클 때에는 여러 차례에 걸쳐서 조금씩 처리한다.
			if (Time.deltaTime <= 0.1f)
				Move(Time.deltaTime);
			else {
				int n = (int)(Time.deltaTime / 0.1f) + 1;
				for (int i = 0; i < n; i++)
					Move(Time.deltaTime / (float)n);
			}
		}
	}
	
	public void Move(float t)
	{
		// 다음에 이동할 위치.
		Vector3 pos = transform.position;
		pos += m_direction * SPEED * t;
		
		
		// 그리드를 통과했는지 점검.   
		bool across = false;		

		// 정수화한 값이 틀린 경우 그리드 경계를 넘었다.
		if ((int)pos.x != (int)transform.position.x)
			across = true;
		if ((int)pos.z != (int)transform.position.z)
			across = true;

		Vector3 near_grid = new Vector3(Mathf.Round(pos.x),pos.y,Mathf.Round(pos.z));
		m_current_grid = near_grid;
		// 정면의 벽에 부딪혔는가.
		Vector3 forward_pos = pos + m_direction*0.5f; // 반Unit까지 Ray를 보낸다.
		if (Mathf.RoundToInt(forward_pos.x) != Mathf.RoundToInt(pos.x) ||
		    Mathf.RoundToInt(forward_pos.z) != Mathf.RoundToInt(pos.z)) {
			Vector3 tpos =pos;
			tpos.y += HITCHECK_HEIGHT;
			bool collided = Physics.Raycast (tpos,m_direction,1.0f,HITCHECK_LAYER_MASK);
			if (collided) {
				pos = near_grid;
				across = true;
			}
		}
		if (across || (pos-near_grid).magnitude < 0.00005f) {
			Vector3 direction_save = m_direction;

			// 메세지를 송신하여 OnGrid() 메서드를 불러온다.
			SendMessage("OnGrid",pos);

			if (Vector3.Dot(direction_save,m_direction )< 0.00005f)
				pos = near_grid + m_direction * 0.001f;  // 조금 작동시켜 놓지 않으면 다시 OnGrid되기 때문에.
		}
		
		m_move_vector = (pos-transform.position)/t;
		transform.position = pos;
	}
	
	public void SetDirection(Vector3 v)
	{
		m_direction = v;
	}
	
	public Vector3 GetDirection()
	{
		return m_direction;
	}
	
	public bool IsReverseDirection(Vector3 v)
	{
		if (Vector3.Dot(v,m_direction) < -0.99999f)
			return true;
		else
			return false;
	}

	public bool CheckWall(Vector3 direction)
	{
		Vector3 tpos =m_current_grid;
		tpos.y += HITCHECK_HEIGHT;
		return Physics.Raycast(tpos,direction,1.0f,HITCHECK_LAYER_MASK);
	}
	
	public bool IsRunning()
	{
		if (m_move_vector.magnitude > 0.01f)
			return true;
		return false;
	}

	public void HitStop(bool enable)
	{
		if (enable)
			m_pause |= PAUSE_TYPE.HITSTOP;
		else
			m_pause &= ~PAUSE_TYPE.HITSTOP;
	}
	
}
