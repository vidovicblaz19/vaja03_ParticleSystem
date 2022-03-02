using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wireframe : MonoBehaviour
{
    public float edgeWidth = 0.02f;
    // Start is called before the first frame update
    void Start()
    {
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.widthMultiplier = edgeWidth;
        lineRenderer.positionCount = 16;

        var points = new Vector3[16];
        points[0] = new Vector3( -(this.transform.localScale.x/2), (this.transform.localScale.y/2), -(this.transform.localScale.z/2));
        points[1] = new Vector3(-(this.transform.localScale.x / 2), (this.transform.localScale.y / 2), (this.transform.localScale.z / 2));
        points[2] = new Vector3(-(this.transform.localScale.x / 2), -(this.transform.localScale.y / 2), (this.transform.localScale.z / 2));
        points[3] = new Vector3((this.transform.localScale.x / 2), -(this.transform.localScale.y / 2), (this.transform.localScale.z / 2));
        points[4] = new Vector3((this.transform.localScale.x / 2), (this.transform.localScale.y / 2), (this.transform.localScale.z / 2));
        points[5] = new Vector3((this.transform.localScale.x / 2), (this.transform.localScale.y / 2), -(this.transform.localScale.z / 2));
        points[6] = new Vector3((this.transform.localScale.x / 2), -(this.transform.localScale.y / 2), -(this.transform.localScale.z / 2));
        points[7] = new Vector3(-(this.transform.localScale.x / 2), -(this.transform.localScale.y / 2), -(this.transform.localScale.z / 2));
        points[8] = new Vector3(-(this.transform.localScale.x / 2), (this.transform.localScale.y / 2), -(this.transform.localScale.z / 2));
        points[9] = new Vector3((this.transform.localScale.x / 2), (this.transform.localScale.y / 2), -(this.transform.localScale.z / 2));
        points[10] = new Vector3((this.transform.localScale.x / 2), -(this.transform.localScale.y / 2), -(this.transform.localScale.z / 2));
        points[11] = new Vector3((this.transform.localScale.x / 2), -(this.transform.localScale.y / 2), (this.transform.localScale.z / 2));
        points[12] = new Vector3((this.transform.localScale.x / 2), (this.transform.localScale.y / 2), (this.transform.localScale.z / 2));
        points[13] = new Vector3(-(this.transform.localScale.x / 2), (this.transform.localScale.y / 2), (this.transform.localScale.z / 2));
        points[14] = new Vector3(-(this.transform.localScale.x / 2), -(this.transform.localScale.y / 2), (this.transform.localScale.z / 2));
        points[15] = new Vector3(-(this.transform.localScale.x / 2), -(this.transform.localScale.y / 2), -(this.transform.localScale.z / 2));

        lineRenderer.SetPositions(points);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
