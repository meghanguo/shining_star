using System.Collections.Generic;
using UnityEngine;
using System.IO;

public struct DataObject
{
    public string hip;
    public float dist;
    public float x0;
    public float y0;
    public float z0;
    public float mag;
    public float absmag;
    public float vx;
    public float vy;
    public float vz;
    public char spect;

    public DataObject(string hip, float dist, float x0, float y0, float z0, float mag, float absmag, float vx, float vy, float vz, char spect)
    {
        this.hip = hip;
        this.dist = dist * 3.262f;
        this.x0 = x0;
        this.y0 = y0;
        this.z0 = z0;
        this.mag = mag;
        this.absmag = absmag;
        this.vx = vx;
        this.vy = vy;
        this.vz = vz;
        this.spect = spect;
    }
}

public class Star : MonoBehaviour
{
    public string csvFileName = "athyg_31_reduced_m10.csv";
    public List<DataObject> allData = new List<DataObject>(); // all data from csv 
    public float maxDistance = 25f; // show stars within 25 light years
    public HashSet<GameObject> allStars = new HashSet<GameObject>(); // all instantiated stars
    public Dictionary<string, GameObject> map = new Dictionary<string, GameObject>();
    public GameObject Sol;
    public GameObject starPrefab;

    void SetStarProperties(GameObject starObject, char spect)
    {
        float size = SpectClassToSize(spect);
        Color color = SpectClassToColor(spect);

        starObject.transform.localScale = new Vector3(size, size, size);
        starObject.GetComponent<Renderer>().material.color = color;
    }

    float SpectClassToSize(char spect)
    {
        switch (spect)
        {
            case 'O':
                return 1.0f;
            case 'B':
                return 0.7f;
            case 'A':
                return 0.4f;
            case 'F':
                return 0.3f;
            case 'G':
                return 0.2f;
            case 'K':
                return 0.15f;
            case 'M':
                return 0.1f;
            default:
                return 1.0f;
        }
    }

    Color SpectClassToColor(char spect)
    {
        switch (spect)
        {
            case 'O':
                return Color.blue;
            case 'B':
                return Color.cyan;
            case 'A':
                return Color.white;
            case 'F':
                return Color.yellow;
            case 'G':
                return Color.yellow;
            case 'K':
                return new Color(1.0f, 0.5f, 1.0f);
            case 'M':
                return Color.red;
            default:
                return Color.white;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        string filePath = Path.Combine(Application.dataPath, csvFileName);
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);

            for (int i = 1; i < lines.Length; i++)
            {
                string[] values = lines[i].Split(',');
                string hip = values[0];
                float dist = float.Parse(values[1]);
                float x0 = float.Parse(values[2]);
                float y0 = float.Parse(values[3]);
                float z0 = float.Parse(values[4]);
                float mag = float.Parse(values[5]);
                float absmag = float.Parse(values[6]);
                float vx = float.Parse(values[7]);
                float vy = float.Parse(values[8]);
                float vz = float.Parse(values[9]);
                char spect = char.Parse(values[10]);

                DataObject temp = new DataObject(hip, dist, x0, y0, z0, mag, absmag, vx, vy, vz, spect);
                allData.Add(temp);
            }
            Debug.Log($"list size: {allData.Count}");

            Sol = Instantiate(starPrefab);
            SetStarProperties(Sol, 'G');
            Sol.transform.position = new Vector3(0f, 1.0f, 0f);

            for (int i = 0; i < allData.Count; i++)
            {
                if ((allData[i].dist <= maxDistance * 3.262) || ((allData[i].hip != "") && (allData[i].dist <= 250f * 3.262))) //TODO: currently only loading stars within 25 light years
                {
                    GameObject starObject = Instantiate(starPrefab);
                    SetStarProperties(starObject, allData[i].spect);
                    starObject.transform.position = new Vector3(allData[i].x0, allData[i].z0, allData[i].y0);
                    starObject.name = "Star" + i;
                    allStars.Add(starObject);
                    if (allData[i].hip != "")
                    {
                        map.Add(allData[i].hip, starObject);
                    }
                    if (allData[i].hip == "11767")
                    {
                        Debug.Log(i);
                    }
                }
                
            }

            Debug.Log($"star size: {allStars.Count}");

            //TODO: currently only drawing 1 constellation 
            //List<string> cent = new List<string> {"71683", "68702", "68702", "66657", "66657", "68002", "68002", "68282", "68282", "67472", "67472", "67464", "67464", "65936", "65936", "65109", "67464", "68933", "67472", "71352", "71352", "73334", "68002", "61932", "61932", "60823", "60823", "59196", "59196", "56480", "56480", "56561"};
            //for (int i = 0; i < cent.Count - 1; i += 2)
            //{
            //    GameObject star1 = map[cent[i]];
            //    GameObject star2 = map[cent[i + 1]];
            //    GameObject line = new GameObject("Line");
            //    LineRenderer lineRenderer = line.AddComponent<LineRenderer>();

            //    lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            //    lineRenderer.widthMultiplier = 0.5f;
            //    lineRenderer.material.color = Color.white;
            //    lineRenderer.positionCount = 2;
            //    lineRenderer.SetPosition(0, star1.transform.position);
            //    lineRenderer.SetPosition(1, star2.transform.position);
            //}
            List<string> uMi = new List<string> { "11767", "85822", "85822", "82080", "82080", "77055", "77055", "79822", "79822", "75097", "75097", "72607", "72607", "77055" };
            for (int i = 0; i < uMi.Count - 1; i += 2)
            {
                GameObject star1 = map[uMi[i]];
                GameObject star2 = map[uMi[i + 1]];
                GameObject line = new GameObject("Line");
                LineRenderer lineRenderer = line.AddComponent<LineRenderer>();

                lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
                lineRenderer.widthMultiplier = 0.2f;
                lineRenderer.material.color = Color.magenta;
                lineRenderer.positionCount = 2;
                lineRenderer.SetPosition(0, star1.transform.position);
                lineRenderer.SetPosition(1, star2.transform.position);
            }


        }
        else
        {
            Debug.LogError($"File not found: {filePath}");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 cameraPosition = Camera.main.transform.position;
        //Collider[] colliders = Physics.OverlapSphere(cameraPosition, 25f, 0);

        //foreach (Collider collider in colliders)
        //{
        //    collider.gameObject.SetActive(true);
        //}
    }

}
