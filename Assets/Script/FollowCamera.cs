// 유효하게 되면 스페이스키로 줌인/줌아웃을 할 수 있게 된다.      
//#define	ENABLE_ZOOM_IN_DEBUG

using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {

	private const	float	ZOOM_IN_DURATION  = 0.2f;		// [sec] 줌인에 걸리는 시간. 
	private const	float	ZOOM_OUT_DURATION = 1.5f;		// [sec] 줌아웃에 걸리는 시간.  

	public Vector3		m_position_offset;
	public Vector3		m_position_offset_zoom_in;

	public Transform	m_target;
	private bool		m_zoom_in;
	private float		m_zoom_in_timer = 0.0f;

	// Use this for initialization
	void Start ()
	{
		// 줌인이 끝났다＝줌아웃한 상태로 시작.
		m_zoom_in = false;
		m_zoom_in_timer = ZOOM_OUT_DURATION;
	}

	void	Update()
	{
#if ENABLE_ZOOM_IN_DEBUG
		if(Input.GetKeyDown(KeyCode.Space)) {

			if(m_zoom_in) {

				OnEndAttack();

			} else {

				OnAttack();
			}
		}
#endif

	}
	
	// Update is called once per frame
	void	LateUpdate()
	{
		float	rate = 0.0f;

		if(m_zoom_in) {

			// 줌인일 때.    
			rate = m_zoom_in_timer/ZOOM_IN_DURATION;			// 「시간」을「시작부터 종료까지의 비율」로 변환.
			rate = Mathf.Clamp01(rate);							// 0.0 부터 1.0 의 범위에 넣는다.
			rate = Mathf.Sin(rate*Mathf.PI/2.0f);
			rate = Mathf.Pow(rate, 0.5f);

		} else {

			// 줌아웃일 때.    
			rate = m_zoom_in_timer/ZOOM_OUT_DURATION;
			rate = Mathf.Clamp01(rate);
			rate = Mathf.Sin(rate*Mathf.PI/2.0f);
			rate = Mathf.Pow(rate, 0.5f);

			// 타이머의 0.0 ～ 1.0 이 줌비율 0.0 ～ 1.0 이 되도록 한다.   
			rate = 1.0f - rate;
		}

		// 위치 오프셋을 계산. 
		Vector3		offset = Vector3.Lerp(m_position_offset, m_position_offset_zoom_in, rate);
		transform.position = m_target.position + offset;

		// 화각 계산 설정.
		float	fov = Mathf.Lerp(60.0f, 30.0f, rate);
		this.GetComponent<Camera>().fieldOfView = fov;

		m_zoom_in_timer += Time.deltaTime;
	}
	
	// 공격한/공격 받은 때 불러온다.
	public void OnAttack()
	{
#if false
		m_zoom_in = true;
		m_zoom_in_timer = 0.0f;
#else
	// 줌아웃 도중에 줌인이 시작되더라도 괜찮은 코드.
	// 다소 어려울 수 있으나 초반에는 잘 모르더라도 신경 쓰지 않아도 된다.

		if(m_zoom_in) {

			// 줌인 중이었다면 아무 처리도 하지 않는다.

		} else {

			m_zoom_in = true;

			if(m_zoom_in_timer < ZOOM_OUT_DURATION) {

				// 줌아웃 중이었다면 같은 위치에서 줌인으로 바뀌도록       
				// 타이머를 변환한다.

				float	rate;

				// 타이머로 줌 비율을 구한다.
				rate = m_zoom_in_timer/ZOOM_OUT_DURATION;
				rate = Mathf.Clamp01(rate);
				rate = Mathf.Sin(rate*Mathf.PI/2.0f);
				rate = Mathf.Pow(rate, 0.5f);
				rate = 1.0f - rate;

				// 줌 비율에서 줌인일 때 타이머 값으로 역변환한다.
				rate = Mathf.Pow(rate, 1.0f/0.3f);
				rate = Mathf.Asin(rate)/(Mathf.PI/2.0f);
				m_zoom_in_timer = ZOOM_IN_DURATION*rate;


			} else {

				m_zoom_in_timer = 0.0f;
			}
		}
#endif
	}
	
	public void OnEndAttack()
	{
#if false
		m_zoom_in = false;
		m_zoom_in_timer = 0.0f;
#else
	// 줌아웃 도중에 줌인이 시작되더라고 괜찮은 코드.
	// 다소 어려울 수 있으나 초반에는 잘 모르더라도 신경 쓰지 않아도 된다.
		if(!m_zoom_in) {

		} else {

			m_zoom_in = false;

			if(m_zoom_in_timer < ZOOM_IN_DURATION) {

				float	rate;

				rate = m_zoom_in_timer/ZOOM_IN_DURATION;
				rate = Mathf.Clamp01(rate);
				rate = Mathf.Sin(rate*Mathf.PI/2.0f);
				rate = Mathf.Pow(rate, 0.3f);

				rate = 1.0f - rate;
				rate = Mathf.Pow(rate, 1.0f/0.5f);
				rate = Mathf.Asin(rate)/(Mathf.PI/2.0f);
				m_zoom_in_timer = ZOOM_OUT_DURATION*rate;

			} else {

				m_zoom_in_timer = 0.0f;
			}
		}

#endif

	}
}
