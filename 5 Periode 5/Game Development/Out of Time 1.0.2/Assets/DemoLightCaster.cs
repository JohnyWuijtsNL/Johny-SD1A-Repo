using UnityEngine;
using System.Linq;

public class DemoLightCaster : MonoBehaviour
{
    //array of all objects light should collide with
    [SerializeField]
    GameObject[] sceneObjects;

    //variable for the offset of the rays
    [SerializeField]
    Vector2 offset = new Vector2(0.01f, 0.01f);

    //veriable for the visual light rays
    [SerializeField]
    GameObject lightRays;

    //variable for the mesh of the light ray
    Mesh mesh;

    [SerializeField]
    struct angledVerts
    {
        public Vector3 vert;
        public float angle;
        public Vector2 uv;
    }

    private void Start()
    {
        mesh = lightRays.GetComponent<MeshFilter>().mesh;
        sceneObjects.Append(this.gameObject);
    }

    void Update()
    {
        mesh.Clear();

        //get all vertecis of all gameobjects
        Vector3[] objverts = sceneObjects[0].GetComponent<MeshFilter>().mesh.vertices;
        Debug.Log(sceneObjects.Length);
        for (int i = 1; i < sceneObjects.Length; i++)
        {
            objverts.Concat(sceneObjects[i].GetComponent<MeshFilter>().mesh.vertices);
        }

        //resetting variables
        angledVerts[] angledverts = new angledVerts[(objverts.Length * 200)];
        Vector3[] verts = new Vector3[(objverts.Length * 2) + 1];
        Vector2[] uvs = new Vector2[(objverts.Length * 2) + 1];
        verts[0] = lightRays.transform.worldToLocalMatrix.MultiplyPoint3x4(this.transform.position);
        uvs[0] = new Vector2(verts[0].x, verts[0].y);

        //keep track of how many vertecies
        int h = 0;

        //store location of light object
        Vector3 myLoc = this.transform.position;

        //loop through all objects
        for (int i = 0; i < sceneObjects.Length; i++)
        {
            //store all vertices of object
            Vector3[] mesh = sceneObjects[i].GetComponent<MeshFilter>().mesh.vertices;
            //loop through all vertices
            for (int j = 0; j < mesh.Length; j++)
            {
                //store location of vertex
                Vector3 vertLoc = sceneObjects[i].transform.localToWorldMatrix.MultiplyPoint3x4(mesh[j]);

                //determine location difference between light and vertex
                Vector2 locDif = new Vector2(vertLoc.x - myLoc.x, vertLoc.y - myLoc.y);

                //determine angle of rendered texture
                float angle1 = Mathf.Atan2(locDif.y - offset.y, locDif.x - offset.x);
                float angle2 = Mathf.Atan2(locDif.y + offset.y, locDif.x + offset.x);

                //determine hit location of ray between location of light and location of vertex, with an offset
                RaycastHit hit, hit2;
                Physics.Raycast(myLoc, locDif - offset, out hit, 100);
                Physics.Raycast(myLoc, locDif + offset, out hit2, 100);
                Debug.DrawLine(myLoc, hit.point, Color.red);
                Debug.DrawLine(myLoc, hit2.point, Color.green);

                //set texture
                //Debug.Log(mesh.Length + ", " + h * 2);
                angledverts[(h * 2)].vert = lightRays.transform.worldToLocalMatrix.MultiplyPoint3x4(hit.point);

                angledverts[(h * 2)].angle = angle1;
                angledverts[(h * 2)].uv = angledverts[h * 2].vert;
                
                angledverts[(h * 2) + 1].vert = lightRays.transform.worldToLocalMatrix.MultiplyPoint3x4(hit2.point);
                angledverts[(h * 2) + 1].angle = angle2;
                angledverts[(h * 2) + 1].uv = angledverts[(h * 2) + 1].vert;

                h++;
            }
        }


    }
}
