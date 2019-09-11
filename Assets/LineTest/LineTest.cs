using UnityEngine;
using System.Collections;

public class LineTest : MonoBehaviour {
	//外围线圈
	LineRenderer _LineRender;
	//内围线圈
	LineRenderer _LineRender1;
	public Material _LineMat;
	public Material MeshMat;
	public const int PointCout = 7;
	public Vector3[] PointsPos ;
	
	
	MeshFilter _Mesh;
	
	const float max = 100.0f;
	
	Vector3[] newVertices;
	Vector2[] newUV;
	int[] newTriangles;
	// Use this for initialization
	void Start () {
        GameObject _LineObject = new GameObject ("_LineObject");
        _LineRender = _LineObject.AddComponent<LineRenderer> ();
        _LineRender.material = _LineMat;
        _LineRender.positionCount = PointCout;

        GameObject _LineObject1 = new GameObject ("_LineObject");
        _LineRender1 = _LineObject1.AddComponent<LineRenderer> ();
        _LineRender1.material = _LineMat;
        _LineRender1.positionCount = PointCout;

        PointsPos = new Vector3[]{
			new Vector3(transform.position.x + max/2,  transform.position.y + Mathf.Sqrt(3) * max/2, transform.position.z + 1.0f),
			new Vector3(transform.position.x + max, transform.position.y                     , transform.position.z + 1.0f),
			new Vector3(transform.position.x + max/2,  transform.position.y - Mathf.Sqrt(3) * max/2, transform.position.z + 1.0f),
			new Vector3(transform.position.x - max/2,  transform.position.y - Mathf.Sqrt(3) * max/2, transform.position.z + 1.0f),
			new Vector3(transform.position.x - max, transform.position.y                     , transform.position.z + 1.0f),
			new Vector3(transform.position.x - max/2,  transform.position.y + Mathf.Sqrt(3) * max/2, transform.position.z + 1.0f),
			new Vector3(transform.position.x + max/2,  transform.position.y + Mathf.Sqrt(3) * max/2, transform.position.z + 1.0f),
		};
		
		ChangeMesh ();
		
		GameObject _MeshObj = new GameObject("_MeshObj");
		
		_Mesh = _MeshObj.AddComponent<MeshFilter>();
		_Mesh.mesh = new Mesh();
	
		ApplyMesh ();
		MeshRenderer _render = _MeshObj.AddComponent<MeshRenderer>();
		_render.material = MeshMat;

        for(int i = 0; i < PointCout; i++) {
            _LineRender.SetPosition (i , PointsPos[i]);
        }
    }

	void Update () {
        for(int i = 0; i < PointCout; i++) {
            _LineRender1.SetPosition (i , new Vector3 (PointsPos[i].x * _SliderValue[i % 6] / max , PointsPos[i].y * _SliderValue[i % 6] / max , PointsPos[i].z));
        }
        ChangeMesh ();
        ApplyMesh ();
    }
	float[] _SliderValue = new float[6] {50,50,50,50,50,50};
	void OnGUI()
	{
        for(int i = 0; i < 6; i++) {
            _SliderValue[i] = GUI.HorizontalSlider (new Rect (25 , 25 + i * 30 , 100 , 30) , _SliderValue[i] , 0.0f , max);
        }
    }

	void ChangeMesh ()
	{
		newVertices = new Vector3[]
		{
			PointsPos[1] - new Vector3(max,0,0),
			new Vector3(PointsPos[0].x * _SliderValue[0] / max,  PointsPos[0].y * _SliderValue[0] / max, PointsPos[0].z ),
			new Vector3(PointsPos[1].x * _SliderValue[1] / max,  PointsPos[1].y * _SliderValue[1] / max, PointsPos[1].z ),
			new Vector3(PointsPos[2].x * _SliderValue[2] / max,  PointsPos[2].y * _SliderValue[2] / max, PointsPos[2].z ),
			new Vector3(PointsPos[3].x * _SliderValue[3] / max,  PointsPos[3].y * _SliderValue[3] / max, PointsPos[3].z ),
			new Vector3(PointsPos[4].x * _SliderValue[4] / max,  PointsPos[4].y * _SliderValue[4] / max, PointsPos[4].z ),
			new Vector3(PointsPos[5].x * _SliderValue[5] / max,  PointsPos[5].y * _SliderValue[5] / max, PointsPos[5].z ),
		};
		newUV = new Vector2[]
		{
            new Vector2(0.5f, 0.5f ),
            new Vector2(0.75f, 0.865f),
            new Vector2(1, 0.5f),
            new Vector2(0.75f, 0.135f),
            new Vector2(0.25f, 0.135f),
            new Vector2(0, 0.5f),
            new Vector2(0.25f, 0.865f ),
        };
		newTriangles = new int[]
		{
            0,
            1,
            2,

            0,
            2,
            3,

            0,
            3,
            4,

            0,
            4,
            5,

            0,
            5,
            6,

            0,
            6,
            1
        };
	}

	void ApplyMesh ()
	{
		_Mesh.mesh.vertices = newVertices;
		_Mesh.mesh.uv = newUV;
		_Mesh.mesh.triangles = newTriangles;
	}
}
