using UnityEngine;
using System.Collections;

public class FOVMeshCreator : MonoBehaviour
{
    private Mesh myMesh;
    public float Radius;
    public float angle;

    public float segments = 2;
    private float segmentAngle;

    private Vector3[] verts;
    private Vector3[] normals;
    private int[] triangles;
    private Vector2[] uvs;

    private float actualAngle;

    private float angleFake;

    public Material mat;
    public float range;
    public Vector3 VectorUlti;
    void Start()
    {
        var MeshF = gameObject.AddComponent<MeshFilter>();
        var MeshR = gameObject.AddComponent<MeshRenderer>();
        var MeshMC = gameObject.AddComponent<MeshCollider>();
        MeshMC.convex=true;
        MeshMC.isTrigger=true;
        angleFake = angle;

        MeshR.material = mat;
        MeshR.receiveShadows = false;
        MeshR.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        //go.renderer.material.mainTexture = Resources.Load("glass", typeof(Texture2D));
        //AssetDatabase.CreateAsset(material, "Assets/MyMaterial.mat");

        //MESH
        myMesh = gameObject.GetComponent<MeshFilter>().mesh;

        //BUILD THE MESH
        BuildMesh(range);

        MeshMC.sharedMesh = myMesh;
    }

    public void BuildMesh(float range)
    {
        Radius = range * 0.71f + 5;
        angleFake = angle - (Radius * 2);
        if (angleFake < 15)
            angleFake = 15;
        // Grab the Mesh off the gameObject
        //myMesh = gameObject.GetComponent<MeshFilter>().mesh;
        
        //Clear the mesh
        myMesh.Clear();

        // Calculate actual pythagorean angle
        actualAngle = 90.0f - angleFake;

        // Segment Angle
        segmentAngle = angleFake * 2 / segments;

        // Initialise the array lengths
        verts = new Vector3[(int) segments * 3];
        normals = new Vector3[(int) segments * 3];
        triangles = new int[(int) segments * 3];
        uvs = new Vector2[(int) segments * 3];

        // Initialise the Array to origin Points
        for (int i = 0; i < verts.Length; i++)
        {
            verts[i] = new Vector3(0, 0, 0);
            normals[i] = Vector3.up;
        }

        // Create a dummy angle
        float a = actualAngle;

        // Create the Vertices
        for (int i = 1; i < verts.Length; i += 3)
        {
            verts[i] = new Vector3(Mathf.Cos(Mathf.Deg2Rad * a) * Radius, // x
                0, // y
                Mathf.Sin(Mathf.Deg2Rad * a) * Radius); // z

            a += segmentAngle;

            verts[i + 1] = new Vector3(Mathf.Cos(Mathf.Deg2Rad * a) * Radius, // x
                0, // y
                Mathf.Sin(Mathf.Deg2Rad * a) * Radius); // z          
        }

        // Create Triangle
        for (int i = 0; i < triangles.Length; i += 3)
        {
            triangles[i] = 0;
            triangles[i + 1] = i + 2;
            triangles[i + 2] = i + 1;
        }

        // Generate planar UV Coordinates
        for (int i = 0; i < uvs.Length; i++)
        {
            uvs[i] = new Vector2(verts[i].x, verts[i].z);
        }

        // Put all these back on the mesh
        myMesh.vertices = verts;
        myMesh.normals = normals;
        myMesh.triangles = triangles;
        myMesh.uv = uvs;

    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public bool IsInRange(Vector3 a, float size)
    {
        Vector3 newpos=transform.position;
        newpos.y=a.y;
        transform.position=newpos;
        float distance = Vector3.Distance(a, newpos) - size;

        if (distance > Radius)
            return false;

        if (distance == 0)
            return true;

        return IsInRangeIgnoreDistance(a, size);
    }

    public bool IsInRangeIgnoreDistance(Vector3 a, float size)
    {

        //float biggestAngle = VectorUlti.Angle(transform.parent.InverseTransformDirection(new Vector3(a.x- size,a.y,a.z) - transform.position),transform.forward);
        //float smallestAngle = VectorUlti.Angle(transform.parent.InverseTransformDirection(new Vector3(a.x+ size,a.y,a.z)  - transform.position),transform.forward);
        
        //if (biggestAngle < 90 - angleFake || smallestAngle > 90 + angleFake)
         //   return false;

        return true;
    }
}