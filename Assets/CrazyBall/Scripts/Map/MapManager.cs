using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public enum MapType
{
    Grass,
    Rock,
    Ice,
    Mire
}

public class MapManager : NetworkBehaviour  {
    public int Length = 100;
    public int Width = 100;

    public int SpecialFloor = 15;

    public GameObject GrassCubePrefab;
    public GameObject RockCubePrefab;
    public GameObject IceCubePrefab;
    public GameObject MireCubePrefab;

    private GameObject FloorPrefab;
    public Transform FloorParent
    {
        get
        {
            return GameObject.Find("Component/FloorParent").transform;
        }
    }

    public static MapManager instance;
    private void Awake()
    {
        instance = this;
    }

    void Start () {

        //CmdTest();
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {

    }
      
     
    public GameObject CmdCreatMap(MapType mt,List<Vector2> listSpecialFloor) {

        switch (mt)
        {
            case MapType.Grass:
                FloorPrefab = GrassCubePrefab;
                break;
            case MapType.Rock:
                FloorPrefab = RockCubePrefab;
                break;
            case MapType.Ice:
                FloorPrefab = IceCubePrefab;
                break;
            case MapType.Mire:
                FloorPrefab = MireCubePrefab;
                break;
            default:
                break;
        }

        CreateFloor();

        switch (mt)
        {
            case MapType.Grass:
                GrassMap(listSpecialFloor);
                break;
            case MapType.Rock:
                RockMap(listSpecialFloor);
                break;
            case MapType.Ice:
                IceMap();
                break;
            case MapType.Mire:
                MireMap();
                break;
            default:
                break;
        }

        return FloorParent.gameObject;

    } 
    
    void CreateFloor()
    {
        for (int i = 0; i <= Length; i++)
        {
            for (int j = 0; j <= Width; j++)
            {
                GameObject gFloor = GameObject.Instantiate(FloorPrefab, FloorParent);
                gFloor.transform.position = new Vector3((i- Length/2)*5, 0,(j- Width/2)*5);
                gFloor.name = (i - Length/2) + "_" + (j - Width / 2);
            }
        }
    }

    void GrassMap(List<Vector2> listSpecialFloor)
    {
        for (int i = 0; i < listSpecialFloor.Count; i++)
        {
            for (int j = 0; j < FloorParent.childCount; j++)
            {
                if (FloorParent.GetChild(j).name== (listSpecialFloor[i].x - 1) + "_" + (listSpecialFloor[i].y- 1)
                    || FloorParent.GetChild(j).name == (listSpecialFloor[i].x - 1) + "_" + (listSpecialFloor[i].y)
                    || FloorParent.GetChild(j).name == (listSpecialFloor[i].x - 1) + "_" + (listSpecialFloor[i].y + 1)
                    || FloorParent.GetChild(j).name == (listSpecialFloor[i].x) + "_" + (listSpecialFloor[i].y - 1)
                    || FloorParent.GetChild(j).name == (listSpecialFloor[i].x) + "_" + (listSpecialFloor[i].y)
                    || FloorParent.GetChild(j).name == (listSpecialFloor[i].x) + "_" + (listSpecialFloor[i].y + 1)
                    || FloorParent.GetChild(j).name == (listSpecialFloor[i].x + 1) + "_" + (listSpecialFloor[i].y - 1)
                    || FloorParent.GetChild(j).name == (listSpecialFloor[i].x + 1) + "_" + (listSpecialFloor[i].y)
                    || FloorParent.GetChild(j).name == (listSpecialFloor[i].x + 1) + "_" + (listSpecialFloor[i].y + 1)
                    )
                {
                    Destroy(FloorParent.GetChild(j).gameObject);
                }
            }
        }

    }

    void RockMap(List<Vector2> listSpecialFloor)
    {
        for (int i = 0; i < listSpecialFloor.Count; i++)
        {
            
            for (int j = 0; j < FloorParent.childCount; j++)
            {
                if (FloorParent.GetChild(j).name == (listSpecialFloor[i].x - 1) + "_" + (listSpecialFloor[i].y - 1)
                    || FloorParent.GetChild(j).name == (listSpecialFloor[i].x - 1) + "_" + (listSpecialFloor[i].y)
                    || FloorParent.GetChild(j).name == (listSpecialFloor[i].x - 1) + "_" + (listSpecialFloor[i].y + 1)
                    || FloorParent.GetChild(j).name == (listSpecialFloor[i].x) + "_" + (listSpecialFloor[i].y - 1)
                    || FloorParent.GetChild(j).name == (listSpecialFloor[i].x) + "_" + (listSpecialFloor[i].y)
                    || FloorParent.GetChild(j).name == (listSpecialFloor[i].x) + "_" + (listSpecialFloor[i].y + 1)
                    || FloorParent.GetChild(j).name == (listSpecialFloor[i].x + 1) + "_" + (listSpecialFloor[i].y - 1)
                    || FloorParent.GetChild(j).name == (listSpecialFloor[i].x + 1) + "_" + (listSpecialFloor[i].y)
                    || FloorParent.GetChild(j).name == (listSpecialFloor[i].x + 1) + "_" + (listSpecialFloor[i].y + 1)
                    )
                {
                    GameObject gSpecialFloor = GameObject.Instantiate(FloorPrefab, FloorParent);
                    gSpecialFloor.transform.position = FloorParent.GetChild(j).position + new Vector3(0f,1f,0f) ;

                }
            }
        }
    }

    void IceMap()
    {

    }

    void MireMap()
    {

    }
}
