using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawMap : MonoBehaviour
{
    public List<Vector3> CorePoints;
    public List<float> gradient;
    public float Radius, width;
    public int step;
    public MeshFilter viewMeshFilter;
	public MeshCollider viewMeshCollider;
	Mesh viewMesh;
    // Start is called before the first frame update
    void Start()
    {
        //tao mesh
        viewMesh = new Mesh ();
		viewMesh.name = "View Mesh";
		viewMeshFilter.mesh = viewMesh;
		viewMeshCollider.sharedMesh=viewMeshFilter.mesh;
    
        //
        List<Vector3> ViewPoints=new List<Vector3>();
        for(int i=0;i<CorePoints.Count;i++){
            ViewPoints.Add(CorePoints[i]);
            Vector3 tmp = CorePoints[i];
            tmp += new Vector3(width,0,0);
            ViewPoints.Add(tmp);
            if(i<CorePoints.Count-1){
                tmp = CorePoints[i];
                float p,q,x0,z0,x1,z1;
                x0=tmp.x;z0=tmp.z;z1=CorePoints[i+1].z;x1=CorePoints[i+1].x;
                p=gradient[i*2];q=gradient[i*2+1];
                Matrix4x4 matrixA = Matrix4x4.identity;
        
                matrixA[0, 0] = Mathf.Pow(x0,3);
                matrixA[0, 1] = Mathf.Pow(x0,2);
                matrixA[0, 2] = x0;
                matrixA[0, 3] = 1;
                matrixA[1, 0] = Mathf.Pow(x1,3);
                matrixA[1, 1] = Mathf.Pow(x1,2);
                matrixA[1, 2] = x1;
                matrixA[1, 3] = 1;
                matrixA[2, 0] = 3*Mathf.Pow(x0,2);
                matrixA[2, 1] = 2*x0;
                matrixA[2, 2] = 1;
                matrixA[2, 3] = 0;
                matrixA[3, 0] = 3*Mathf.Pow(x1,2);
                matrixA[3, 1] = 2*x1;
                matrixA[3, 2] = 1;
                matrixA[3, 3] = 0;
        
                Matrix4x4 matrixB = Matrix4x4.zero;
        
                // setting the final column vector of the matrix to be the constant vector
                matrixB[0, 3] = z0;
                matrixB[1, 3] = z1;
                matrixB[2, 3] = p;
                matrixB[3, 3] = q;
                float a3,a2,a1,a0;
                Matrix4x4 solution = matrixA.inverse * matrixB;
                a3=solution[0,3];
                a2=solution[1,3];
                a1=solution[2,3];
                a0=solution[3,3];
                // the solution should be in the final column vector of the solution matrix
                
            
            
                for(int j=1;j<step;j++){
                    tmp = CorePoints[i] ;
                    tmp += (CorePoints[i+1] - CorePoints[i])/step*j;
                    float z,x;
                    x=tmp.x;
                    z=(a3*Mathf.Pow(x,3)+a2*Mathf.Pow(x,2)+a1*x+a0);
                    tmp=new Vector3(x,0,z);
                    ViewPoints.Add(tmp);
                    tmp += new Vector3(width,0,0);
                    ViewPoints.Add(tmp);
                }
            }
        }
        
		int[] triangles = new int[(ViewPoints.Count-2) * 3*2];
		Vector3[] vertices = new Vector3[ViewPoints.Count];

        for (int i = 0; i < ViewPoints.Count; i++) {
            vertices [i] = transform.InverseTransformPoint(ViewPoints[i]);
			if (i < ViewPoints.Count - 3) {
				triangles [i * 3] = i;
				triangles [i * 3 + 1] = i + 2;
				triangles [i * 3 + 2] = i + 1;
                triangles [(i + ViewPoints.Count - 2) * 3] = i+1;
				triangles [(i + ViewPoints.Count - 2) * 3 +1] = i + 2;
				triangles [(i + ViewPoints.Count - 2) * 3 + 2] = i + 3;
			}
		}
        foreach( var x in ViewPoints) {
            Debug.Log( x);
        }
        viewMesh.Clear ();
        
		viewMesh.vertices = vertices;
		viewMesh.triangles = triangles;
		viewMesh.RecalculateNormals ();
        viewMeshCollider.sharedMesh=viewMeshFilter.mesh;

    }

    // Update is called once per frame
    void Update()
    {
        
        //tao mesh
        viewMesh = new Mesh ();
		viewMesh.name = "View Mesh";
		viewMeshFilter.mesh = viewMesh;
		viewMeshCollider.sharedMesh=viewMeshFilter.mesh;
    
        //
        List<Vector3> ViewPoints=new List<Vector3>();
        for(int i=0;i<CorePoints.Count;i++){
            ViewPoints.Add(CorePoints[i]);
            Vector3 tmp = CorePoints[i];
            tmp += new Vector3(width,0,0);
            ViewPoints.Add(tmp);
            if(i<CorePoints.Count-1){
                tmp = CorePoints[i];
                float p,q,x0,z0,x1,z1;
                x0=tmp.x;z0=tmp.z;z1=CorePoints[i+1].z;x1=CorePoints[i+1].x;
                p=gradient[i*2];q=gradient[i*2+1];
                Matrix4x4 matrixA = Matrix4x4.identity;
        
                matrixA[0, 0] = Mathf.Pow(x0,3);
                matrixA[0, 1] = Mathf.Pow(x0,2);
                matrixA[0, 2] = x0;
                matrixA[0, 3] = 1;
                matrixA[1, 0] = Mathf.Pow(x1,3);
                matrixA[1, 1] = Mathf.Pow(x1,2);
                matrixA[1, 2] = x1;
                matrixA[1, 3] = 1;
                matrixA[2, 0] = 3*Mathf.Pow(x0,2);
                matrixA[2, 1] = 2*x0;
                matrixA[2, 2] = 1;
                matrixA[2, 3] = 0;
                matrixA[3, 0] = 3*Mathf.Pow(x1,2);
                matrixA[3, 1] = 2*x1;
                matrixA[3, 2] = 1;
                matrixA[3, 3] = 0;
        
                Matrix4x4 matrixB = Matrix4x4.zero;
        
                // setting the final column vector of the matrix to be the constant vector
                matrixB[0, 3] = z0;
                matrixB[1, 3] = z1;
                matrixB[2, 3] = p;
                matrixB[3, 3] = q;
                float a3,a2,a1,a0;
                Matrix4x4 solution = matrixA.inverse * matrixB;
                a3=solution[0,3];
                a2=solution[1,3];
                a1=solution[2,3];
                a0=solution[3,3];
                // the solution should be in the final column vector of the solution matrix
                
            
            
                for(int j=1;j<step;j++){
                    tmp = CorePoints[i] ;
                    tmp += (CorePoints[i+1] - CorePoints[i])/step*j;
                    float z,x;
                    x=tmp.x;
                    z=(a3*Mathf.Pow(x,3)+a2*Mathf.Pow(x,2)+a1*x+a0);
                    tmp=new Vector3(x,0,z);
                    ViewPoints.Add(tmp);
                    tmp += new Vector3(width,0,0);
                    ViewPoints.Add(tmp);
                }
            }
        }
        
		int[] triangles = new int[(ViewPoints.Count-2) * 3*2];
		Vector3[] vertices = new Vector3[ViewPoints.Count];

        for (int i = 0; i < ViewPoints.Count; i++) {
            vertices [i] = transform.InverseTransformPoint(ViewPoints[i]);
			if (i < ViewPoints.Count - 3) {
				triangles [i * 3] = i;
				triangles [i * 3 + 1] = i + 2;
				triangles [i * 3 + 2] = i + 1;
                triangles [(i + ViewPoints.Count - 2) * 3] = i+1;
				triangles [(i + ViewPoints.Count - 2) * 3 +1] = i + 2;
				triangles [(i + ViewPoints.Count - 2) * 3 + 2] = i + 3;
			}
		}
        foreach( var x in ViewPoints) {
           // Debug.Log( x);
        }
        viewMesh.Clear ();
        
		viewMesh.vertices = vertices;
		viewMesh.triangles = triangles;
		viewMesh.RecalculateNormals ();
        viewMeshCollider.sharedMesh=viewMeshFilter.mesh;

    }
}
