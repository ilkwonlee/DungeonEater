using UnityEngine;
using System.Collections;

public class ScoreBoard : MonoBehaviour {

 	protected const float	SPEED = 100.0f; 			// 상승 속도.

	protected float		m_lifeTime = 3.0f;				// [sec] 출현시간.
	
	protected Vector3	m_position;
	protected float		m_offsetY;

	protected UnityEngine.UI.Text	m_ui_text;

	// ================================================================ //
	//MonoBehaviour에서의 상속.

	void	Awake()
	{
		m_ui_text = this.GetComponent<UnityEngine.UI.Text>();
	}

	void	Start()
	{
		m_offsetY = 0;
	}

	void 	Update()
	{
		this.GetComponent<RectTransform>().localPosition = m_position + new Vector3(0,m_offsetY,0);

		m_offsetY += SPEED*Time.deltaTime;
	
		m_lifeTime -= Time.deltaTime;
		if (m_lifeTime < 0.0f)
			Destroy(gameObject);
	}

	// ================================================================ //

	// 표시 시작 위치를 설정한다.
	public void	SetPosition(Vector3 position)
	{
		position = Camera.main.WorldToScreenPoint(position);

		position.x -= Screen.width/2.0f;
		position.y -= Screen.height/2.0f;

		m_position = position;
	}	

	// 점수를 설정한다.  
	public void SetScore(int score)
	{
		m_ui_text.text = score.ToString();
	}
	
}
